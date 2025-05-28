using System;
using SpawnerHexa;
using UnityEngine;
using UnityEngine.UI;

namespace Generations
{
    public class StarsGenerator : MonoBehaviour
    {
        [SerializeField] private float _duraction = 0.5f;
        [SerializeField] private int _quantyCoins = 3;
        [SerializeField] private float _summandDelayCreatingStars = 0.1f;

        private StarsSpawner _starsSpawner;
        private HexaSpawner _hexaSpawner;
        private Image _imagePoint;
        private float _delayCoroutineStar;

        public event Action StarsCreated;

        private void OnDisable()
        {
            _hexaSpawner.HexaAfterSameHexesCreated -= CreateStars;
        }

        public void Construct(StarsSpawner starsSpawner, Image imagePoint, HexaSpawner hexaSpawner)
        {
            _starsSpawner = starsSpawner;
            _imagePoint = imagePoint;
            _hexaSpawner = hexaSpawner;

            _hexaSpawner.HexaAfterSameHexesCreated += CreateStars;
        }

        private void CreateStars(Vector3 spawnPosition)
        {
            StarsCreated?.Invoke();

            _delayCoroutineStar = 0.1f;

            Vector3 spawnPos = new Vector3(spawnPosition.x, 23.99f, spawnPosition.z);

            for (int i = 0; i < _quantyCoins; i++)
            {
                _starsSpawner.GetCreate(spawnPos).
                                DoMove(_delayCoroutineStar, _imagePoint.transform.position, _duraction);

                _delayCoroutineStar += _summandDelayCreatingStars;
            }
        }
    }
}
