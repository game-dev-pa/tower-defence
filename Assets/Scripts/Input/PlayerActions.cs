// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public class @PlayerActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""509f6633-cf8f-4204-929f-5d0ded18d59d"",
            ""actions"": [
                {
                    ""name"": ""PlaceTurret"",
                    ""type"": ""Button"",
                    ""id"": ""51a1abc4-f618-49c0-a72b-facbaeae88b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5d4718b8-f2f9-4c1f-928d-64fb53811c1b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceTurret"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_PlaceTurret = m_Gameplay.FindAction("PlaceTurret", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_PlaceTurret;
        public struct GameplayActions
        {
            private @PlayerActions m_Wrapper;
            public GameplayActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @PlaceTurret => m_Wrapper.m_Gameplay_PlaceTurret;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @PlaceTurret.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceTurret;
                    @PlaceTurret.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceTurret;
                    @PlaceTurret.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceTurret;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PlaceTurret.started += instance.OnPlaceTurret;
                    @PlaceTurret.performed += instance.OnPlaceTurret;
                    @PlaceTurret.canceled += instance.OnPlaceTurret;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnPlaceTurret(InputAction.CallbackContext context);
        }
    }
}
