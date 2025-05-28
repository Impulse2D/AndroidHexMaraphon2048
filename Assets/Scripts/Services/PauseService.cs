using System;
using UnityEngine;
using YG;

namespace Services
{
    public class PauseService : MonoBehaviour
    {
        private float _minValueTime = 0f;
        private float _maxValueTime = 1f;

        public event Action FocusNotDetected;
        public event Action FocusOnPauseNotDetected;
        public event Action PauseEnabled;
        public event Action PauseDisabled;

        private void OnDisable()
        {
            YandexGame.onHideWindowGame -= TryENablePauseNotFocuse;
        }

        public void Init()
        {
            if (IsPause() == true)
            {
                DisablePause();
            }

            YandexGame.onHideWindowGame += TryENablePauseNotFocuse;
        }

        private void TryENablePauseNotFocuse()
        {
            if(IsPause() == false)
            {
                FocusNotDetected?.Invoke();
            }
        }

        public void EnablePause()
        {
            ÑhangeTime(_minValueTime);

            ReportEnabledPause();
        }

        public void DisablePause()
        {
            ÑhangeTime(_maxValueTime);

            ReportDisablePause();
        }

        public bool IsPause()
        {
            return Time.timeScale < _maxValueTime;
        }

        private void ÑhangeTime(float valueTime)
        {
            Time.timeScale = valueTime;
        }

        private void ReportEnabledPause()
        {
            PauseEnabled?.Invoke();
        }

        private void ReportDisablePause()
        {
            PauseDisabled?.Invoke();
        }
    }
}
