using PlayerInputReader;
using UnityEngine;
using UnityEngine.EventSystems;

namespace СontrolBlocker
{
    public class GameСontrolBlocker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private InputReader _inputReader;

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputReader.EnableBlockCancelingClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputReader.EnableBlockCancelingClick();
        }
    }
}
