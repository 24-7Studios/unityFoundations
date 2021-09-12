// GENERATED AUTOMATICALLY FROM 'Assets/inputmaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputmaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputmaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""inputmaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""0a64611d-a017-4b6a-9559-e91d92d36a5f"",
            ""actions"": [
                {
                    ""name"": ""looking"",
                    ""type"": ""Value"",
                    ""id"": ""e761f024-addf-4cf5-a38a-f0a15a112488"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""451f71ac-93f8-4e63-8c8c-f8ef0c2f30cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""grab"",
                    ""type"": ""Button"",
                    ""id"": ""812610ee-03ca-4a3b-bb97-1b2d90e1fe95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5d3149f4-2a4b-4a54-af55-91d2f96b3be4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""320c1fcb-4c1d-4805-9b7c-c1680fbe7773"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""440cbc69-c6e4-4367-8691-3067d362a7a6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""vertical"",
                    ""id"": ""466f37b0-7162-44f4-94a6-373a1fa742cd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""aed6e840-8963-4304-be26-17edfeb46fc2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""0c99cc6c-37d9-49f4-8a0d-d7a47a981529"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""53e5680b-ae9f-4385-9f32-038027a4fbe9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""6f8382c6-cb9b-40df-8687-1798ca86f43d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f85e59ce-1b03-44f4-8370-5593c2b787b0"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""looking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""mouse + keybaord"",
            ""bindingGroup"": ""mouse + keybaord"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_looking = m_Player.FindAction("looking", throwIfNotFound: true);
        m_Player_jump = m_Player.FindAction("jump", throwIfNotFound: true);
        m_Player_grab = m_Player.FindAction("grab", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_looking;
    private readonly InputAction m_Player_jump;
    private readonly InputAction m_Player_grab;
    private readonly InputAction m_Player_Movement;
    public struct PlayerActions
    {
        private @Inputmaster m_Wrapper;
        public PlayerActions(@Inputmaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @looking => m_Wrapper.m_Player_looking;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @grab => m_Wrapper.m_Player_grab;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @looking.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLooking;
                @looking.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLooking;
                @looking.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLooking;
                @jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @grab.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                @grab.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                @grab.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrab;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @looking.started += instance.OnLooking;
                @looking.performed += instance.OnLooking;
                @looking.canceled += instance.OnLooking;
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
                @grab.started += instance.OnGrab;
                @grab.performed += instance.OnGrab;
                @grab.canceled += instance.OnGrab;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_mousekeybaordSchemeIndex = -1;
    public InputControlScheme mousekeybaordScheme
    {
        get
        {
            if (m_mousekeybaordSchemeIndex == -1) m_mousekeybaordSchemeIndex = asset.FindControlSchemeIndex("mouse + keybaord");
            return asset.controlSchemes[m_mousekeybaordSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnLooking(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
