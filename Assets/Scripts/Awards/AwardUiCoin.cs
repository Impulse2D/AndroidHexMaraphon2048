using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Awards
{
    public class AwardUiCoin : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;
        [SerializeField] private float _duraction = 1f;

        private Coroutine _coroutine;

        public event Action<AwardUiCoin> Released;

        public void DoMove(Vector3 targetPosition)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(CountTimeWaitMoving(targetPosition));
        }

        public void SetParrent(Transform parrent)
        {
            transform.SetParent(parrent);
        }

        private IEnumerator CountTimeWaitMoving(Vector3 targetPosition)
        {
            WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(_delay);

            yield return waitForSeconds;

            transform.DOMove(targetPosition, _duraction).SetUpdate(true).onComplete = ReportReleased;
        }

        private void ReportReleased()
        {
            Released?.Invoke(this);
        }
    }
}
