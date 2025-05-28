using System;
using System.Collections.Generic;
using Cell;
using Hexes;
using OtherHexSO;
using SaverData;
using SoundsGame;
using SpawnerAwardChest;
using Spawners;
using UnityEngine;

namespace SpawnerHexa
{
    public class HexaSpawner : Spawner<HexaPool>
    {
        private const string IdPlaneHexPrefs = nameof(IdPlaneHexPrefs);
        private const string PositionPlaneHexPrefs = nameof(PositionPlaneHexPrefs);

        [SerializeField] private List<Hexa> _planeHexes;
        [SerializeField] private float _hightPositionHexa = 23.99f;
        [SerializeField] private List<HexaCell> _cells;
        [SerializeField] private Vector3 _firstSpawnPosition;
        [SerializeField] private Vector3 _secondSpawnPosition;
        [SerializeField] private Vector3 _thirdSpawnPosition;

        [SerializeField] private HexaScriptableObject _hexaSO2;
        [SerializeField] private HexaScriptableObject _hexaSO4;
        [SerializeField] private HexaScriptableObject _hexaSO8;
        [SerializeField] private HexaScriptableObject _hexaSO16;
        [SerializeField] private HexaScriptableObject _hexaSO32;
        [SerializeField] private HexaScriptableObject _hexaSO64;

        [SerializeField] private List<HexaScriptableObject> _hexaScriptableObjects;

        private List<HexaCell> _tempCellsGameOver;
        private List<HexaScriptableObject> _hexesFromPlaneSO;
        private List<Vector3> _planeFromPosition;

        private Vector3 _rotateHex = new Vector3(0f, 90f, 0f);
        private SaverDataGame _saverDataGame;
        private HexaCollisionSoundPlayer _hexaCollisionSoundPlayer;
        private AwardChestCoinSpawner _awardChestCoinSpawner;
        private bool _isGameOver;

        public event Action<Hexa> SameHexes—ollided;
        public event Action<Vector3> HexaAfterSameHexesCreated;
        public event Action GameOverDetected;
        public event Action<int> CreatingNewHexaDetected;

        public bool IsGameOver => _isGameOver;
        public List<HexaScriptableObject> HexaScriptableObjects => _hexaScriptableObjects;
        public List<Hexa> PlaneHexes => _planeHexes;
        public List<Vector3> PlaneFromPosition => _planeFromPosition;

        public void Construct(SaverDataGame prefsSaver,
            HexaCollisionSoundPlayer hexaCollisionSoundPlayer,
            AwardChestCoinSpawner awardChestCoinSpawner)
        {
            _saverDataGame = prefsSaver;
            _hexaCollisionSoundPlayer = hexaCollisionSoundPlayer;
            _awardChestCoinSpawner = awardChestCoinSpawner;
        }

        public void Init()
        {
            _planeHexes = new List<Hexa>();
            _isGameOver = false;

            _hexesFromPlaneSO = new List<HexaScriptableObject>()
            {
                _hexaSO2,
                _hexaSO4,
                _hexaSO8,
                _hexaSO16,
                _hexaSO32,
                _hexaSO64
            };

            _planeFromPosition = new List<Vector3>()
            {
                _firstSpawnPosition,
                _secondSpawnPosition,
                _thirdSpawnPosition,
            };

            CreatePlaneHaxes();
        }

        public Hexa Create(Vector3 position, HexaScriptableObject hexaSO, float hightPositionHexa)
        {
            Hexa hexa = ObjectsPool.GetObject(position, Quaternion.Euler(_rotateHex));

            hexa.ReportedQuantyCollisionsRaycastsWithHexes += TryGameOverLastHex;
            hexa.ReportedEnabledCollisionSameHexa += CreateHexAfterSameHexes—ollided;

            hexa.SetFrontSize(hexaSO.FrontSize);
            hexa.SetMaterial(hexaSO.Material);
            hexa.SetTextNumber(hexaSO.NumberHexa);
            hexa.SetId(hexaSO.ID);
            hexa.transform.position = new Vector3(hexa.transform.position.x,
                hightPositionHexa, hexa.transform.position.z);
            hexa.SetSpawnPosition(hexa.transform.position);

            return hexa;
        }

        public void CreatePlaneHaxes()
        {
            int minIndex = 0;
            int maxIndexHexesFromPlaneSO = _hexesFromPlaneSO.Count;

            if (PlayerPrefs.HasKey(PositionPlaneHexPrefs + 1) &&
                PlayerPrefs.HasKey(IdPlaneHexPrefs + 1))
            {
                _saverDataGame.LoadPlaseHexesData();
            }
            else
            {
                for (int i = 0; i < _planeFromPosition.Count; i++)
                {
                    int randomHexSO = UnityEngine.Random.Range(minIndex, maxIndexHexesFromPlaneSO);
                    Hexa newHexa = Create(_planeFromPosition[i], _hexesFromPlaneSO[randomHexSO], _planeFromPosition[i].y);

                    AddPlaneHexes(newHexa);
                }
            }
        }

        public void RemovePlaneHexa(Hexa hexa)
        {
            _planeHexes.Remove(hexa);
        }

        public void CreateOnePlaneHaxe(Vector3 position)
        {
            int minIndex = 0;
            int maxIndexHexesFromPlaneSO = _hexesFromPlaneSO.Count;

            int randomHexSO = UnityEngine.Random.Range(minIndex, maxIndexHexesFromPlaneSO);

            Hexa newHexa = Create(position, _hexesFromPlaneSO[randomHexSO], position.y);

            AddPlaneHexes(newHexa);

            _saverDataGame.SavePlaneHexes();
        }

        public void CreateHexOnCell(Vector3 position, HexaScriptableObject soHexa, HexaCell hexaCell)
        {
            Hexa newHexa = Create(position, soHexa, _hightPositionHexa);

            newHexa.SetHexaCell(hexaCell);
            newHexa.CurrentHexaCell.SetHexa(newHexa);
            newHexa.EnableIsLocatedCell();
            newHexa.TryEnableRaysLateral();
        }

        public void AddPlaneHexes(Hexa hexa)
        {
            _planeHexes.Add(hexa);
        }

        private void CreateHexAfterSameHexes—ollided(Hexa hexa)
        {
            hexa.ReportedEnabledCollisionSameHexa -= CreateHexAfterSameHexes—ollided;

            _hexaCollisionSoundPlayer.PlaySound();

            if (hexa.RaycastSameHexa != null)
            {
                int minIdHexa = 9;
                int maxIdSoHexa = 15;
                int idCurrentHexa = hexa.RaycastSameHexa.ID;
                Vector3 positionCellCurrentHexa = hexa.RaycastSameHexa.CurrentHexaCell.transform.position;
                HexaCell currentHexaCell = hexa.RaycastSameHexa.CurrentHexaCell;

                if (idCurrentHexa < maxIdSoHexa)
                {
                    CreateHexOnCell(positionCellCurrentHexa, _hexaScriptableObjects[idCurrentHexa + 1], currentHexaCell);

                    if (idCurrentHexa + 1 > minIdHexa)
                    {
                        CreatingNewHexaDetected?.Invoke(idCurrentHexa + 1);

                        _awardChestCoinSpawner.Create(positionCellCurrentHexa);
                    }
                }
                else
                {
                    currentHexaCell.DisableIsBusy();
                }

                HexaAfterSameHexesCreated?.Invoke(positionCellCurrentHexa);
            }
            SameHexes—ollided?.Invoke(hexa);
        }

        private void TryGameOverLastHex(Hexa hexa)
        {
            int minCountTempCells = 0;
            int maxCountTempCellsGameOver = 1;
            int minCounttempCellsGameOver = 0;
            int countSameId = 0;

            hexa.ReportedQuantyCollisionsRaycastsWithHexes -= TryGameOverLastHex;

            _tempCellsGameOver = new List<HexaCell>();

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].IsBusy == false)
                {
                    _tempCellsGameOver.Add(_cells[i]);
                }
            }

            if (_tempCellsGameOver.Count == maxCountTempCellsGameOver)
            {
                _tempCellsGameOver[0].TryEnableRaysLateral();

                if (_tempCellsGameOver[0].IdHexes.Count > minCounttempCellsGameOver)
                {
                    for (int i = 0; i < _tempCellsGameOver[0].IdHexes.Count; i++)
                    {
                        for (int y = 0; y < _planeHexes.Count; y++)
                        {
                            if (_tempCellsGameOver[0].IdHexes[i] == _planeHexes[y].ID)
                            {
                                Debug.Log(_tempCellsGameOver[0].IdHexes[i]);

                                ++countSameId;
                            }
                        }
                    }
                }

                if (countSameId == 0)
                {
                    EnableGameOver();

                    GameOverDetected?.Invoke();

                    Debug.Log(countSameId + "" + " ÓÌÂˆ Ë„˚, ÌË˜Â„Ó ÌÂ Ì‡È‰ÂÌÓ");
                }
                else
                {
                    Debug.Log(countSameId + "" + "œÓ‰ÓÎÊ‡ÂÏ Ë„Û");
                }

                if (_tempCellsGameOver.Count > 0)
                {
                    _tempCellsGameOver[0].ClearIdHexes();
                }
            }
            else if (_tempCellsGameOver.Count == minCountTempCells)
            {
                Debug.Log(" ÓÌÂˆ Ë„˚");

                EnableGameOver();

                GameOverDetected?.Invoke();
            }
        }

        private void EnableGameOver()
        {
            _saverDataGame.ResetPrefsData();

            PlayerPrefs.Save();

            _tempCellsGameOver.Clear();

            _isGameOver = true;
        }
    }
}