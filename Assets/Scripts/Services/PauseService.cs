using System;
using UnityEngine;


namespace Services
{
    public class PauseService : MonoBehaviour
    {
        private float _minValueTime = 0f;
        private float _maxValueTime = 1f;

        public event Action FocusNotDetected;
        public event Action PauseEnabled;
        public event Action PauseDisabled;

        public void Init()
        {
            if (IsPause() == true)
            {
                DisablePause();
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus == false && IsPause() == false)
            {
                FocusNotDetected?.Invoke();
            }
        }

        public void EnablePause()
        {
            �hangeTime(_minValueTime);

            ReportEnabledPause();
        }

        public void DisablePause()
        {
            �hangeTime(_maxValueTime);

            ReportDisablePause();
        }

        public bool IsPause()
        {
            return Time.timeScale < _maxValueTime;
        }

        private void �hangeTime(float valueTime)
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
