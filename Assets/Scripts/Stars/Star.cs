using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Stars
{
    public class Star : MonoBehaviour
    {
        private Coroutine _coroutine;

        public event Action<Star> Released;

        public void DoMove(float delay, Vector3 targetPosition, float duraction)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(CountTimeWaitMoving(delay, targetPosition, duraction));
        }

        private IEnumerator CountTimeWaitMoving(float delay, Vector3 targetPosition, float duraction)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

            yield return waitForSeconds;

            transform.DOMove(targetPosition, duraction).onComplete = ReportReleased;
        }

        private void ReportReleased()
        {
            Released?.Invoke(this);
        }
    }
}
