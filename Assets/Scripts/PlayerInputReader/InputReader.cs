using System;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputReader
{
    public class InputReader : MonoBehaviour
    {
        private InputActions _inputActions;
        private bool _isMoving;
        private bool _isCanceledClickBlocked;

        private PauseService _pausedService;
        public event Action MovingDetected;
        public event Action MovingNotDetected;
        public event Action<Vector3> ClickDetected;
        public event Action Click;

        public bool IsMoving => _isMoving;
        public bool IsCanceledClickBlocked => _isCanceledClickBlocked;
        public InputActions InputActions => _inputActions;

        public void Construct(PauseService pauseService)
        {
            _pausedService = pauseService;
        }

        public void Init()
        {
            _inputActions = new InputActions();

            _inputActions.Player.Moving.started += EnableMovingEvent;
            _inputActions.Player.Moving.canceled += DisableMovingEvent;
            _inputActions.Player.Click.started += ReportClickPosition;

            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Moving.started -= EnableMovingEvent;
            _inputActions.Player.Moving.canceled -= DisableMovingEvent;
            _inputActions.Player.Click.started -= ReportClickPosition;

            _inputActions.Disable();
        }

        public void DisableMoving()
        {
            _isMoving = false;
        }

        public void EnableBlockCancelingClick()
        {
            _isCanceledClickBlocked = true;
        }

        private void DisableBlockCancelingClick()
        {
            _isCanceledClickBlocked = false;
        }

        private void EnableMoving()
        {
            _isMoving = true;
        }

        private void EnableMovingEvent(InputAction.CallbackContext context)
        {
            if(_pausedService.IsPause() == false)
            {
                EnableMoving();

                if (_isCanceledClickBlocked == true)
                {
                    DisableBlockCancelingClick();
                }

                MovingDetected?.Invoke();
            }
        }

        private void DisableMovingEvent(InputAction.CallbackContext context)
        {
            DisableMoving();

            MovingNotDetected?.Invoke();
        }

        private void ReportClickPosition(InputAction.CallbackContext context)
        {
            if(_pausedService.IsPause() == false)
            {
                Click?.Invoke();
            }
        }
    }
}

