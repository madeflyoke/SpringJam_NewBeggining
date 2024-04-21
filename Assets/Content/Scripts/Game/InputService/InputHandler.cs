using System;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Scripts.Game.InputService
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private InputConfig _inputConfig;
        private Vector2 _axis = Vector2.zero;
        private ButtonPair[] _mouseButton;
        private ButtonPair[] _keyboardButtons;
        private bool _enable;
        public Vector2 GetAxisRaw() => _axis;

        private Dictionary<KeysEventType, List<Action>> _inputActionTable =
            new Dictionary<KeysEventType, List<Action>>();

        public void Enable()
        {
            _enable = true;
            _axis = Vector2.zero;
            UnityEngine.Input.ResetInputAxes();
        }

        public void Disable()
        {
            _enable = false;
            _axis = Vector2.zero;
        }

        private void Awake()
        {
            _keyboardButtons = _inputConfig.KeyboardButtonPairs.ToArray();
        }

        private void Update()
        {
            if (_enable == false) return;
            HandleAxis();
            HandleButtonInput();
        }

        private void HandleAxis()
        {
            if (_enable == false) return;
            _axis.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            _axis.y = UnityEngine.Input.GetAxisRaw("Vertical");
        }
        

        private void HandleButtonInput()
        {
            void CheckKeyInput(ButtonPair[] buttonPairs)
            {
                foreach (var buttonPair in buttonPairs)
                {
                    switch (buttonPair.InteractionType)
                    {
                        case InputInteractionType.DOWN:
                        {
                            if (HandleKeyDown(buttonPair.KeyType))
                                CastAction(buttonPair.EventType);
                            break;
                        }

                        case InputInteractionType.UP:
                        {
                            if (HandleKeyUpS(buttonPair.KeyType))
                                CastAction(buttonPair.EventType);
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            if (_mouseButton != null)
                CheckKeyInput(_mouseButton);
            if (_keyboardButtons != null)
                CheckKeyInput(_keyboardButtons);
        }


        private bool HandleKeyDown(KeyCode key) => UnityEngine.Input.GetKeyDown(key);
        private bool HandleKeyUpS(KeyCode key) => UnityEngine.Input.GetKeyUp(key);


        private void CastAction(KeysEventType type)
        {
            if (_inputActionTable.ContainsKey(type))
                _inputActionTable[type].ForEach(action => action?.Invoke());
        }

        public void ClearEventHandlerOn(KeysEventType type)
        {
            if (_inputActionTable.ContainsKey(type))
                _inputActionTable.Remove(type);

        }

        public void SubscribeOnInputEvent(KeysEventType type, Action action)
        {
            if (_inputActionTable.ContainsKey(type))
            {
                if (!_inputActionTable[type].Contains(action))
                    _inputActionTable[type].Add(action);
            }

            else
            {
                _inputActionTable.Add(type, new List<Action>());
                _inputActionTable[type].Add(action);
            }
        }

        public void UnsubscribeFromInputEvent(KeysEventType type, Action action)
        {
            if (!_inputActionTable.ContainsKey(type)) return;

            if (_inputActionTable[type].Contains(action))
                _inputActionTable[type].Remove(action);
        }
    }

    public enum KeysEventType
    {
        Jump,
        InteractDown,
        InteractUp,
        SelectFirst,
        SelectSecond,
        ChangeTeamStatus
    }
}