//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/inputmaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Inputmaster : IInputActionCollection2, IDisposable
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
                    ""type"": ""PassThrough"",
                    ""id"": ""e761f024-addf-4cf5-a38a-f0a15a112488"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""451f71ac-93f8-4e63-8c8c-f8ef0c2f30cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5d3149f4-2a4b-4a54-af55-91d2f96b3be4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Change"",
                    ""type"": ""Button"",
                    ""id"": ""c3b2bfbf-9a55-4025-a929-6c2af0258380"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""drop"",
                    ""type"": ""Button"",
                    ""id"": ""590c09c7-69b0-43ee-980e-f3b6be6ce383"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""140ded67-27fb-439c-bf63-5a9df26ed819"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1,pressPoint=0.2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire_1"",
                    ""type"": ""Button"",
                    ""id"": ""92026b81-ea9e-4cb7-868d-cd5fc69714f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire_2/Zoom1"",
                    ""type"": ""Button"",
                    ""id"": ""3154bfe1-127f-4536-81a0-6f034a659eff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""reload"",
                    ""type"": ""Button"",
                    ""id"": ""1b3b9af8-9eed-4408-8268-bcfca367b39a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""reload2"",
                    ""type"": ""Button"",
                    ""id"": ""7d2cb123-8ff9-4447-a713-d6cb739a8409"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""melee"",
                    ""type"": ""Button"",
                    ""id"": ""9798b9a0-aa45-4f96-af2c-575c45aa7e21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""kill"",
                    ""type"": ""Button"",
                    ""id"": ""d629762e-523f-4e2c-aa5b-74d15c2ba3a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""dual"",
                    ""type"": ""Button"",
                    ""id"": ""4662051a-3861-4f81-b855-2c12cc82a876"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""toggleLight"",
                    ""type"": ""Button"",
                    ""id"": ""cd57bac0-48cc-49bf-aefb-36ec6ca2190c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
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
                    ""id"": ""a7a723c2-c844-47e5-b48d-4292d0e66ad1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
                    ""id"": ""49979ae2-7f06-4a70-be3f-fd41082a31a5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                    ""id"": ""943b2ae3-a14b-43bf-a66a-eaf8aa2d4f45"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
                    ""id"": ""5595f04c-8195-43ef-96fd-7f7bccb7bbe4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
                    ""id"": ""39cbfbf7-41a3-46a9-aed9-f20cd88b10e1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
                    ""id"": ""411f0d48-1b7c-4dc2-af7d-89c577b2820e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""ab76664a-cccd-4adf-92ea-6628ae140c37"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
                    ""action"": ""Fire_2/Zoom1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54a7d887-876d-407c-a5d7-b3cd0f6b7137"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b08edc27-5cb2-4ff3-8ccb-93f47f2c37e5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
                    ""action"": ""reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91418210-6667-44d4-af2c-8bbf01633c62"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""126a64b2-d96f-4221-bcec-2627103f4199"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
                    ""action"": ""melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4950f31-471d-4816-bca0-1acc40756fcb"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""reload2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""339a6e94-5e8a-4d23-b0ba-8af1dc81a46c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
                    ""action"": ""reload2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12a84156-277f-4c5b-ba7c-f0031b41b229"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a2841ff-1d95-491b-94a1-9484f9f70dcc"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""kill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bce29ff7-4e25-4a7b-8e0b-17eb7b2a41d8"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""dual"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""674b75de-6f02-4fb8-864d-2dde7933559b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse + keybaord"",
                    ""action"": ""toggleLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerStandby"",
            ""id"": ""f04c75ea-3ed8-4ad6-a39c-2e6eedef36b4"",
            ""actions"": [
                {
                    ""name"": ""looking"",
                    ""type"": ""Value"",
                    ""id"": ""5ce561f3-95fe-4352-b9eb-299e89214af1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5dbf8f4-77e9-4424-9e08-607400fe837e"",
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
                    ""id"": ""f0510197-1fab-4279-b6f5-1bf54382e796"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""gamepad"",
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
        },
        {
            ""name"": ""gamepad"",
            ""bindingGroup"": ""gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
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
        m_Player_drop = m_Player.FindAction("drop", throwIfNotFound: true);
        m_Player_interact = m_Player.FindAction("interact", throwIfNotFound: true);
        m_Player_Fire_1 = m_Player.FindAction("Fire_1", throwIfNotFound: true);
        m_Player_Fire_2Zoom1 = m_Player.FindAction("Fire_2/Zoom1", throwIfNotFound: true);
        m_Player_reload = m_Player.FindAction("reload", throwIfNotFound: true);
        m_Player_reload2 = m_Player.FindAction("reload2", throwIfNotFound: true);
        m_Player_melee = m_Player.FindAction("melee", throwIfNotFound: true);
        m_Player_kill = m_Player.FindAction("kill", throwIfNotFound: true);
        m_Player_dual = m_Player.FindAction("dual", throwIfNotFound: true);
        m_Player_toggleLight = m_Player.FindAction("toggleLight", throwIfNotFound: true);
        // PlayerStandby
        m_PlayerStandby = asset.FindActionMap("PlayerStandby", throwIfNotFound: true);
        m_PlayerStandby_looking = m_PlayerStandby.FindAction("looking", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_looking;
    private readonly InputAction m_Player_jump;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Change;
    private readonly InputAction m_Player_drop;
    private readonly InputAction m_Player_interact;
    private readonly InputAction m_Player_Fire_1;
    private readonly InputAction m_Player_Fire_2Zoom1;
    private readonly InputAction m_Player_reload;
    private readonly InputAction m_Player_reload2;
    private readonly InputAction m_Player_melee;
    private readonly InputAction m_Player_kill;
    private readonly InputAction m_Player_dual;
    private readonly InputAction m_Player_toggleLight;
    public struct PlayerActions
    {
        private @Inputmaster m_Wrapper;
        public PlayerActions(@Inputmaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @looking => m_Wrapper.m_Player_looking;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Change => m_Wrapper.m_Player_Change;
        public InputAction @drop => m_Wrapper.m_Player_drop;
        public InputAction @interact => m_Wrapper.m_Player_interact;
        public InputAction @Fire_1 => m_Wrapper.m_Player_Fire_1;
        public InputAction @Fire_2Zoom1 => m_Wrapper.m_Player_Fire_2Zoom1;
        public InputAction @reload => m_Wrapper.m_Player_reload;
        public InputAction @reload2 => m_Wrapper.m_Player_reload2;
        public InputAction @melee => m_Wrapper.m_Player_melee;
        public InputAction @kill => m_Wrapper.m_Player_kill;
        public InputAction @dual => m_Wrapper.m_Player_dual;
        public InputAction @toggleLight => m_Wrapper.m_Player_toggleLight;
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
                @drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Fire_1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_1;
                @Fire_2Zoom1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
                @Fire_2Zoom1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
                @Fire_2Zoom1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire_2Zoom1;
                @reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @reload2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload2;
                @reload2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload2;
                @reload2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload2;
                @melee.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @melee.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @melee.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMelee;
                @kill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKill;
                @kill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKill;
                @kill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKill;
                @dual.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDual;
                @dual.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDual;
                @dual.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDual;
                @toggleLight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLight;
                @toggleLight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLight;
                @toggleLight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLight;
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
                @drop.started += instance.OnDrop;
                @drop.performed += instance.OnDrop;
                @drop.canceled += instance.OnDrop;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
                @Fire_1.started += instance.OnFire_1;
                @Fire_1.performed += instance.OnFire_1;
                @Fire_1.canceled += instance.OnFire_1;
                @Fire_2Zoom1.started += instance.OnFire_2Zoom1;
                @Fire_2Zoom1.performed += instance.OnFire_2Zoom1;
                @Fire_2Zoom1.canceled += instance.OnFire_2Zoom1;
                @reload.started += instance.OnReload;
                @reload.performed += instance.OnReload;
                @reload.canceled += instance.OnReload;
                @reload2.started += instance.OnReload2;
                @reload2.performed += instance.OnReload2;
                @reload2.canceled += instance.OnReload2;
                @melee.started += instance.OnMelee;
                @melee.performed += instance.OnMelee;
                @melee.canceled += instance.OnMelee;
                @kill.started += instance.OnKill;
                @kill.performed += instance.OnKill;
                @kill.canceled += instance.OnKill;
                @dual.started += instance.OnDual;
                @dual.performed += instance.OnDual;
                @dual.canceled += instance.OnDual;
                @toggleLight.started += instance.OnToggleLight;
                @toggleLight.performed += instance.OnToggleLight;
                @toggleLight.canceled += instance.OnToggleLight;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // PlayerStandby
    private readonly InputActionMap m_PlayerStandby;
    private IPlayerStandbyActions m_PlayerStandbyActionsCallbackInterface;
    private readonly InputAction m_PlayerStandby_looking;
    public struct PlayerStandbyActions
    {
        private @Inputmaster m_Wrapper;
        public PlayerStandbyActions(@Inputmaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @looking => m_Wrapper.m_PlayerStandby_looking;
        public InputActionMap Get() { return m_Wrapper.m_PlayerStandby; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerStandbyActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerStandbyActions instance)
        {
            if (m_Wrapper.m_PlayerStandbyActionsCallbackInterface != null)
            {
                @looking.started -= m_Wrapper.m_PlayerStandbyActionsCallbackInterface.OnLooking;
                @looking.performed -= m_Wrapper.m_PlayerStandbyActionsCallbackInterface.OnLooking;
                @looking.canceled -= m_Wrapper.m_PlayerStandbyActionsCallbackInterface.OnLooking;
            }
            m_Wrapper.m_PlayerStandbyActionsCallbackInterface = instance;
            if (instance != null)
            {
                @looking.started += instance.OnLooking;
                @looking.performed += instance.OnLooking;
                @looking.canceled += instance.OnLooking;
            }
        }
    }
    public PlayerStandbyActions @PlayerStandby => new PlayerStandbyActions(this);
    private int m_mousekeybaordSchemeIndex = -1;
    public InputControlScheme mousekeybaordScheme
    {
        get
        {
            if (m_mousekeybaordSchemeIndex == -1) m_mousekeybaordSchemeIndex = asset.FindControlSchemeIndex("mouse + keybaord");
            return asset.controlSchemes[m_mousekeybaordSchemeIndex];
        }
    }
    private int m_gamepadSchemeIndex = -1;
    public InputControlScheme gamepadScheme
    {
        get
        {
            if (m_gamepadSchemeIndex == -1) m_gamepadSchemeIndex = asset.FindControlSchemeIndex("gamepad");
            return asset.controlSchemes[m_gamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnLooking(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnChange(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnFire_1(InputAction.CallbackContext context);
        void OnFire_2Zoom1(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnReload2(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnKill(InputAction.CallbackContext context);
        void OnDual(InputAction.CallbackContext context);
        void OnToggleLight(InputAction.CallbackContext context);
    }
    public interface IPlayerStandbyActions
    {
        void OnLooking(InputAction.CallbackContext context);
    }
}
