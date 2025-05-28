using System;
using Cell;
using TMPro;
using DG.Tweening;
using UnityEngine;

namespace Hexes
{
    [RequireComponent(typeof(MeshRenderer))]

    public class Hexa : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private HexaCell _currentHexaCell;
        [SerializeField] private float _delayAnimationMoving = 0.5f;
        [SerializeField] private int _counterCollisionsRaycastsWithHexes;
        [SerializeField] private float _offsetMoving;
        [SerializeField] private LayerMask _layerMaskHexa;
        [SerializeField] private LayerMask _layerMaskCell;
        [SerializeField] private TextMeshProUGUI _textNumber;
        [SerializeField] private GameObject _objectRaycastCenter;
        [SerializeField] private GameObject _objectRaycast1;
        [SerializeField] private GameObject _objectRaycast2;
        [SerializeField] private GameObject _objectRaycast3;
        [SerializeField] private GameObject _objectRaycast4;
        [SerializeField] private GameObject _objectRaycast5;
        [SerializeField] private GameObject _objectRaycast6;
        [SerializeField] private int _rangeLiteralRaycasts = 10;
        [SerializeField] private int _rangeCentralRaycasts = 100;

        private MeshRenderer _meshRenderer;

        private int _minQuantyCollisionsRaycastsWithHexes;
        [SerializeField] private Vector3 _spawnPosition;
        private Vector3 _positionObjectLower;

        private bool _isLocatedCell;
        private bool _isDropped;
        private Hexa _raycastSameHexa;
        private bool _isDoMove;

        public event Action<Hexa> ReportedQuantyCollisionsRaycastsWithHexes;
        public event Action<Hexa> ReportedEnabledCollisionSameHexa;

        public bool IsDoMove => _isDoMove;
        public bool IsLocatedCell => _isLocatedCell;
        public Vector3 PositionObjectLower => _positionObjectLower;
        public HexaCell CurrentHexaCell => _currentHexaCell;
        public Vector3 SpawnPosition => _spawnPosition;
        public Hexa RaycastSameHexa => _raycastSameHexa;

        public int CounterCollisionsRaycastsWithHexes => _counterCollisionsRaycastsWithHexes;

        public int ID => _id;

        private void OnEnable()
        {
            ResetCounterCollisionsRaycastsWithHexes();
            _isDoMove = false;
            _minQuantyCollisionsRaycastsWithHexes = 0;
            _raycastSameHexa = null;

            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnDisable()
        {
            ResetCounterCollisionsRaycastsWithHexes();

            if (_raycastSameHexa != null)
            {
                _raycastSameHexa.ReportEnabledCollisionSameHexa();

                _raycastSameHexa = null;
            }

            ResetId();
            TryResetCurrentHexaCell();
            DisableIsLocatedCell();
            DisableIsDropped();
        }

        private void FixedUpdate()
        {
            if (_isDropped == true)
            {
                EnableRayCenterUp(_objectRaycastCenter.transform.position, -_objectRaycastCenter.transform.up,
                    _rangeCentralRaycasts);
            }
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public void TryEnableRaysLateral()
        {
            if (IsCellLocated() == true)
            {
                EnableRayLateral(_objectRaycast1.transform.position, _objectRaycast1.transform.forward, _rangeLiteralRaycasts);
                EnableRayLateral(_objectRaycast2.transform.position, -_objectRaycast2.transform.forward, _rangeLiteralRaycasts);
                EnableRayLateral(_objectRaycast3.transform.position, _objectRaycast3.transform.forward, _rangeLiteralRaycasts);
                EnableRayLateral(_objectRaycast4.transform.position, -_objectRaycast4.transform.forward, _rangeLiteralRaycasts);
                EnableRayLateral(_objectRaycast5.transform.position, _objectRaycast5.transform.forward, _rangeLiteralRaycasts);
                EnableRayLateral(_objectRaycast6.transform.position, -_objectRaycast6.transform.forward, _rangeLiteralRaycasts);
            }

            if (_counterCollisionsRaycastsWithHexes == _minQuantyCollisionsRaycastsWithHexes)
            {
                ReportedQuantyCollisionsRaycastsWithHexes?.Invoke(this);
            }
        }

        public void SetSpawnPosition(Vector3 position)
        {
            _spawnPosition = position;
        }

        public void SetPositionMoving(Vector3 position)
        {
            transform.position = new Vector3(position.x + _offsetMoving, position.y, position.z);
        }

        public void SetPositionDropped(Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
        }
        public void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        public void SetTextNumber(string textNumber)
        {
            _textNumber.text = textNumber;
        }

        public void SetFrontSize(float frotnSize)
        {
            _textNumber.fontSize = frotnSize;
        }

        public void EnableIsLocatedCell()
        {
            _isLocatedCell = true;
        }

        public void EnableIsDropped()
        {
            _isDropped = true;
        }

        public void DisableIsDropped()
        {
            if (_currentHexaCell != null)
            {
                _currentHexaCell.DisableSelectableMaterial();
            }

            _isDropped = false;
        }

        public void SetPositionGameObjectLower(Vector3 positionObjectLower)
        {
            _positionObjectLower = positionObjectLower;
        }

        public void SetHexaCell(HexaCell hexaCell)
        {
            _currentHexaCell = hexaCell;
        }

        private void EnableRayLateral(Vector3 startPosition, Vector3 direction, int rangeLiteralRaycasts)
        {
            RaycastHit hit;

            if (Physics.Raycast(EnableRay(startPosition, direction, rangeLiteralRaycasts), out hit, rangeLiteralRaycasts, _layerMaskHexa))
            {
                if (hit.collider.TryGetComponent(out Hexa hexa))
                {
                    if (hexa != this && hexa.ID == _id && hexa.IsLocatedCell == true && IsDoMove == false)
                    {
                        _counterCollisionsRaycastsWithHexes++;

                        _isDoMove = true;

                        transform.DOMove(hexa.transform.position, _delayAnimationMoving).onComplete = () =>
                         {
                             _raycastSameHexa = hexa;
                             _currentHexaCell.DisableIsBusy();
                             _isDoMove = false;
                             ReportEnabledCollisionSameHexa();
                         };
                    }
                }
            }
        }

        private void ReportEnabledCollisionSameHexa()
        {
            ReportedEnabledCollisionSameHexa?.Invoke(this);
        }

        private void EnableRayCenterUp(Vector3 startPosition, Vector3 direction, int rangeLiteralRaycasts)
        {
            RaycastHit hit;

            if (Physics.Raycast(EnableRay(startPosition, direction, rangeLiteralRaycasts), out hit, _layerMaskCell))
            {
                if (hit.collider.gameObject.TryGetComponent(out HexaCell hexaCell))
                {
                    if (hexaCell.IsBusy == false)
                    {
                        if (_currentHexaCell != null && _currentHexaCell != hexaCell)
                        {
                            _currentHexaCell.DisableSelectableMaterial();
                        }

                        SetHexaCell(hexaCell);

                        _currentHexaCell.EnableSelectableMaterial();

                        SetPositionGameObjectLower(hexaCell.gameObject.transform.position);
                    }
                }
                else
                {
                    if (_currentHexaCell != null)
                    {
                        _currentHexaCell.DisableSelectableMaterial();
                    }

                    SetPositionGameObjectLower(_spawnPosition);
                    TryResetCurrentHexaCell();
                }
            }
        }

        private Ray EnableRay(Vector3 startPosition, Vector3 direction, int rangeLiteralRaycasts)
        {
            Ray ray = new Ray(startPosition, direction * rangeLiteralRaycasts);
            Debug.DrawRay(startPosition, direction * rangeLiteralRaycasts, Color.yellow);

            return ray;
        }

        private void DisableIsLocatedCell()
        {
            _isLocatedCell = false;
        }

        private bool IsCellLocated()
        {
            return _isLocatedCell;
        }

        private void TryResetCurrentHexaCell()
        {
            if (_currentHexaCell != null)
            {
                _currentHexaCell = null;
            }
        }

        private void ResetId()
        {
            int minValueId = 0;

            _id = minValueId;
        }

        private void ResetCounterCollisionsRaycastsWithHexes()
        {
            _counterCollisionsRaycastsWithHexes = _minQuantyCollisionsRaycastsWithHexes;
        }
    }
}
