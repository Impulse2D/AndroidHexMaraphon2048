using Awards;
using Cell;
using Hexes;
using Liderboard;
using SpawnerHexa;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SaverData
{
    public class SaverDataGame : MonoBehaviour
    {
        private const string QuantyAwardCoins2048 = nameof(QuantyAwardCoins2048);
        private const string QuantyAwardCoins4096 = nameof(QuantyAwardCoins4096);
        private const string QuantyAwardCoins8192 = nameof(QuantyAwardCoins8192);
        private const string QuantyAwardCoins16384 = nameof(QuantyAwardCoins16384);
        private const string QuantyAwardCoins32768 = nameof(QuantyAwardCoins32768);
        private const string QuantyAwardCoins65536 = nameof(QuantyAwardCoins65536);

        private const string HexaCellPrefs = nameof(HexaCellPrefs);
        private const string IdHexPrefs = nameof(IdHexPrefs);
        private const string IdPlaneHexPrefs = nameof(IdPlaneHexPrefs);
        private const string PositionPlaneHexPrefs = nameof(PositionPlaneHexPrefs);
        private const string Record = nameof(Record);

        private const string CurrentQuantyScore = nameof(CurrentQuantyScore);
        private const string QuantityStars = nameof(QuantityStars);

        [SerializeField] private List<HexaCell> _cells;

        private HexaSpawner _hexaSpawner;
        private LiderboardSaver _liderboardSaver;
        private AwardsCounter _awardsCounter;

        private void OnDisable()
        {
            YandexGame.onHideWindowGame -= SaveBeforeCloseWindow;
        }

        public void Construct(HexaSpawner hexaSpawner, LiderboardSaver liderboardSaver, AwardsCounter awardsCounter)
        {
            _hexaSpawner = hexaSpawner;
            _liderboardSaver = liderboardSaver;
            _awardsCounter = awardsCounter;

            YandexGame.onHideWindowGame += SaveBeforeCloseWindow;
        }

        public void Init()
        {
            LoadDataHexCells();
        }

        public void SavePlaneHexes()
        {
            for (int i = 0; i < _hexaSpawner.PlaneFromPosition.Count; i++)
            {
                if (PlayerPrefs.HasKey(PositionPlaneHexPrefs + i) == true)
                {
                    PlayerPrefs.DeleteKey(PositionPlaneHexPrefs + i);
                }

                if (PlayerPrefs.HasKey(IdPlaneHexPrefs + i) == true)
                {
                    PlayerPrefs.DeleteKey(IdPlaneHexPrefs + i);
                }
            } 

            for (int i = 0; i < _hexaSpawner.PlaneHexes.Count; i++)
            {
                for (int y = 0; y < _hexaSpawner.PlaneHexes.Count; y++)
                {
                    if (_hexaSpawner.PlaneFromPosition[y] == _hexaSpawner.PlaneHexes[i].SpawnPosition)
                    {
                        PlayerPrefs.SetInt(PositionPlaneHexPrefs + y, y);
                        PlayerPrefs.SetInt(IdPlaneHexPrefs + y, _hexaSpawner.PlaneHexes[i].ID);
                    }
                }

                PlayerPrefs.Save();
            }
        }

        public void LoadPlaseHexesData()
        {
            for (int i = 0; i < _hexaSpawner.PlaneFromPosition.Count; i++)
            {
                if (PlayerPrefs.HasKey(PositionPlaneHexPrefs + i) && PlayerPrefs.HasKey(IdPlaneHexPrefs + i))
                {
                    Hexa newHexa = _hexaSpawner.Create(_hexaSpawner.PlaneFromPosition[PlayerPrefs.GetInt(PositionPlaneHexPrefs + i)],
                        _hexaSpawner.HexaScriptableObjects[PlayerPrefs.GetInt(IdPlaneHexPrefs + i)],
                        _hexaSpawner.PlaneFromPosition[PlayerPrefs.GetInt(PositionPlaneHexPrefs + i)].y);

                    _hexaSpawner.AddPlaneHexes(newHexa);
                }
            }
        }

        public void SavePrefsDataCells()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (PlayerPrefs.HasKey(HexaCellPrefs + i) == true)
                {
                    PlayerPrefs.DeleteKey(HexaCellPrefs + i);
                }

                if (PlayerPrefs.HasKey(IdHexPrefs + i) == true)
                {
                    PlayerPrefs.DeleteKey(IdHexPrefs + i);
                }
            }

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].IsBusy == true && _hexaSpawner.IsGameOver == false)
                {
                    /* if (YandexGame.savesData._hexaCells.ContainsKey(HexaCellYaGames + i))
                     {
                         YandexGame.savesData._hexaCells[HexaCellYaGames + i] = i;
                     }
                     else
                     {
                         YandexGame.savesData._hexaCells.Add(HexaCellYaGames + i, i);
                     }

                     if (YandexGame.savesData._idHexes.ContainsKey(IdHexYaGames + i))
                     {
                         YandexGame.savesData._idHexes[IdHexYaGames + i] = _cells[i].CurrentHexa.ID;
                     }
                     else
                     {
                         YandexGame.savesData._idHexes.Add(IdHexYaGames + i, _cells[i].CurrentHexa.ID);
                     } */

                    PlayerPrefs.SetInt(HexaCellPrefs + i, i);
                    PlayerPrefs.SetInt(IdHexPrefs + i, _cells[i].CurrentHexa.ID);

                    PlayerPrefs.Save();
                }
            }
        }

        public void ResetPrefsData()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                ResetSavePrefsData(HexaCellPrefs + i);
                ResetSavePrefsData(IdHexPrefs + i);
            }

            for (int i = 0; i < _hexaSpawner.PlaneFromPosition.Count; i++)
            {
                ResetSavePrefsData(IdPlaneHexPrefs + i);
                ResetSavePrefsData(PositionPlaneHexPrefs + i);
            }

            PlayerPrefs.DeleteKey(QuantyAwardCoins2048);
            PlayerPrefs.DeleteKey(QuantyAwardCoins4096);
            PlayerPrefs.DeleteKey(QuantyAwardCoins8192);
            PlayerPrefs.DeleteKey(QuantyAwardCoins16384);
            PlayerPrefs.DeleteKey(QuantyAwardCoins32768);
            PlayerPrefs.DeleteKey(QuantyAwardCoins65536);

            ResetSavePrefsData(CurrentQuantyScore);
            ResetSavePrefsData(QuantityStars);
            _awardsCounter.ResetCounterAwardsCoins();
        }

        public void ResetRecordAndAdwarsAndroid()
        {
            PlayerPrefs.DeleteKey(QuantyAwardCoins2048);
            PlayerPrefs.DeleteKey(QuantyAwardCoins4096);
            PlayerPrefs.DeleteKey(QuantyAwardCoins8192);
            PlayerPrefs.DeleteKey(QuantyAwardCoins16384);
            PlayerPrefs.DeleteKey(QuantyAwardCoins32768);
            PlayerPrefs.DeleteKey(QuantyAwardCoins65536);

            ResetSavePrefsData(Record);
        }

        //ÄëÿÊíîïêè
        public void ResetDataYandexGame()
        {
            YandexGame.savesData.award2048 = 0;
            YandexGame.savesData.award4096 = 0;
            YandexGame.savesData.award8192 = 0;
            YandexGame.savesData.award16384 = 0;
            YandexGame.savesData.award32768 = 0;
            YandexGame.savesData.award65536 = 0;

            YandexGame.savesData.record = 0;
            YandexGame.SaveProgress();

            _liderboardSaver.ResetResultLiderboard();
        }

        private void ResetSavePrefsData(string nameSave)
        {
            if(PlayerPrefs.HasKey(nameSave) == true)
            {
                PlayerPrefs.DeleteKey(nameSave);
            }
        }

        private void SaveBeforeCloseWindow()
        {
            PlayerPrefs.Save();
        }

        private void LoadDataHexCells()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (PlayerPrefs.HasKey(HexaCellPrefs + i) && PlayerPrefs.HasKey(IdHexPrefs + i))
                {
                    _hexaSpawner.CreateHexOnCell(_cells[PlayerPrefs.GetInt(HexaCellPrefs + i)].transform.position,
                    _hexaSpawner.HexaScriptableObjects[PlayerPrefs.GetInt(IdHexPrefs + i)], _cells[i]);
                }
            }

            /*  for (int i = 0; i < _cells.Count; i++)
             {
                 if (YandexGame.savesData._hexaCells.ContainsKey(HexaCellYaGames + i) && YandexGame.savesData._idHexes.ContainsKey(IdHexYaGames + i))
                 {
                     _hexaSpawner.CreateHexOnCell(_cells[YandexGame.savesData._hexaCells[HexaCellYaGames + i]].transform.position,
                         _hexaSpawner.HexaScriptableObjects[YandexGame.savesData._idHexes[IdHexYaGames + i]], _cells[i]);
                 }
             } */
        }
    }
}