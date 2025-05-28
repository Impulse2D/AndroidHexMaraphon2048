using System;
using Cell;
using PlayerInputReader;
using SaverData;
using SpawnerHexa;
using UnityEngine;

namespace Hexes
{
    public class HexagonsMover : MonoBehaviour
    {
        private const string StartLearnStatus = nameof(StartLearnStatus);
        private const string StartLearnFirstStatus = nameof(StartLearnFirstStatus);

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _heightDropped = 23.99f;
        [SerializeField] private float _heightTaking = 23.99f;

        private InputReader _inputReader;
        private Camera _camera;
        private Hexa _hexa;
        private HexaSpawner _hexaSpawner;
        private SaverDataGame _saverDataGame;

        private bool _isTaked;
        private Vector3 _positionInput;
        private Vector3 _hexaPosition;

        public event Action HexaGoingDetected;

        private void OnDisable()
        {
            _inputReader.Click -= TakeHex;
            _inputReader.MovingNotDetected -= LetGoHex;
        }

        public void Construct(InputReader inputReader,
            Camera camera,
            HexaSpawner hexaSpawner,
            SaverDataGame saverDataGame)
        {
            _inputReader = inputReader;
            _camera = camera;
            _hexaSpawner = hexaSpawner;
            _saverDataGame = saverDataGame;

            _inputReader.Click += TakeHex;
            _inputReader.MovingNotDetected += LetGoHex;
        }

        private void FixedUpdate()
        {
            if (_isTaked == true && _hexa != null)
            {
                MoveHexa();
            }
        }

        private void MoveHexa()
        {
            _positionInput = _inputReader.InputActions.Player.ClickPosition.ReadValue<Vector2>();
            _hexaPosition = _camera.ScreenToWorldPoint(new Vector3(_positionInput.x, _positionInput.y, _heightTaking));
            _hexa.SetPositionMoving(_hexaPosition);
        }

        private void TakeHex()
        {
            TryReportedGoingHexa();

            _positionInput = _inputReader.InputActions.Player.ClickPosition.ReadValue<Vector2>();

            Ray ray = _camera.ScreenPointToRay(_positionInput);

            if (Physics.Raycast(ray, out RaycastHit hit, _layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out Hexa hexa))
                {
                    if (hexa.IsLocatedCell == false)
                    {
                        _hexa = hexa;
                        _isTaked = true;
                        hexa.EnableIsDropped();
                    }
                }
            }
        }

        private void LetGoHex()
        {
            if (_hexa != null)
            {
                _isTaked = false;

                if (_hexa.CurrentHexaCell != null)
                {
                    if (_hexa.CurrentHexaCell.IsBusy == false)
                    {
                        HexaCell hexaCell = _hexa.CurrentHexaCell;

                        _hexa.CurrentHexaCell.EnableIsBusy();
                        _hexa.CurrentHexaCell.SetHexa(_hexa);
                        _hexaSpawner.RemovePlaneHexa(_hexa);
                        _hexa.EnableIsLocatedCell();
                        _hexaSpawner.CreateOnePlaneHaxe(_hexa.SpawnPosition);

                        _hexa.SetPositionDropped(new Vector3(_hexa.PositionObjectLower.x, _heightDropped, _hexa.PositionObjectLower.z));

                        _hexa.TryEnableRaysLateral();
                    }
                }
                else
                {
                    _hexa.SetPositionDropped(new Vector3(_hexa.PositionObjectLower.x, _hexa.PositionObjectLower.y, _hexa.PositionObjectLower.z));
                }

                _hexa.DisableIsDropped();
                _hexa = null;

                _saverDataGame.SavePrefsDataCells();
            }
        }

        private void TryReportedGoingHexa()
        {
            if(PlayerPrefs.GetString(StartLearnStatus) == StartLearnFirstStatus)
            {
                HexaGoingDetected?.Invoke();
            }
        }
    }
}
