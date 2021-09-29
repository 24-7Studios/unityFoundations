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
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5d3149f4-2a4b-4a54-af55-91d2f96b3be4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change"",
                    ""type"": ""Button"",
                    ""id"": ""c3b2bfbf-9a55-4025-a929-6c2af0258380"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""140ded67-27fb-439c-bf63-5a9df26ed819"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire_1"",
                    ""type"": ""Button"",
                    ""id"": ""92026b81-ea9e-4cb7-868d-cd5fc69714f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire_2/Zoom1"",
                    ""type"": ""Button"",
                    ""id"": ""3154bfe1-127f-4536-81a0-6f034a659eff"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""480fb3eb-c577-4d51-bb7f-8d65a2d4ec66"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85694f22-a308-4d5f-9808-84a181edd587"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Change"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7126d87-a11d-4aa9-8eca-6dcefb78cbc9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Fire_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29a78e4f-3e55-48b6-9f04-746864e1ffa6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""Fire_2/Zoom1"",
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
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Change = m_Player.FindAction("Change", throwIfNotFound: true);
        m_Player_interact = m_Player.FindAction("interact", throwIfNotFound: true);
        m_Player_Fire_1 = m_Player.FindAction("Fire_1", throwIfNotFound: true);
        m_Player_Fire_2Zoom1 = m_Player.FindAction("Fire_2/Zoom1", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Change;
    private readonly InputAction m_Player_interact;
    private readonly InputAction m_Player_Fire_1;
    private readonly InputAction m_Player_Fire_2Zoom1;
    public struct PlayerActions
    {
        private @Inputmaster m_Wrapper;
        public PlayerActions(@Inputmaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @looking => m_Wrapper.m_Player_looking;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Change => m_Wrapper.m_Player_Change;
        public InputAction @interact => m_Wrapper.m_Player_interact;
        public InputAction @Fire_1 => m_Wrapper.m_Player_Fire_1;
        public InputAction @Fire_2Zoom1 => m_Wrapper.m_Player_Fire_2Zoom1;
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
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Change.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChange;
                @Change.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChange;
                @Change.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChange;
                @interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Fire_1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_2Zoom1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
                @Fire_2Zoom1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
                @Fire_2Zoom1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
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
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Change.started += instance.OnChange;
                @Change.performed += instance.OnChange;
                @Change.canceled += instance.OnChange;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
                @Fire_1.started += instance.OnFire_1;
                @Fire_1.performed += instance.OnFire_1;
                @Fire_1.canceled += instance.OnFire_1;
                @Fire_2Zoom1.started += instance.OnFire_2Zoom1;
                @Fire_2Zoom1.performed += instance.OnFire_2Zoom1;
                @Fire_2Zoom1.canceled += instance.OnFire_2Zoom1;
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
        void OnMovement(InputAction.CallbackContext context);
        void OnChange(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnFire_1(InputAction.CallbackContext context);
        void OnFire_2Zoom1(InputAction.CallbackContext context);
    }
}
