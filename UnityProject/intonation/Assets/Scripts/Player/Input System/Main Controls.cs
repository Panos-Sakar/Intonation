// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Input System/Main Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace EvilOwl.Player.Input_System
{
    public class @MainControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Main Controls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""7cf7de7e-fea3-4cf6-836f-61f8e2d8a1d8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""92094801-5686-4bba-a955-57d8046aed08"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""aa45637f-8a60-43f3-badc-3761d32b84f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""732efc4e-f64f-44d1-bcdf-7ff7c577afe2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""0ee46d11-b62b-4508-b33d-4d5532916abc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RedSpell"",
                    ""type"": ""Button"",
                    ""id"": ""774e07ad-ab95-4e14-ad05-bb25c307e915"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GreenSpell"",
                    ""type"": ""Button"",
                    ""id"": ""dbf80158-968f-4996-84be-087685d0a7dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BlueSpell"",
                    ""type"": ""Button"",
                    ""id"": ""52ad5c2a-0811-4724-b514-6f05e9f4137a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""YellowSpell"",
                    ""type"": ""Button"",
                    ""id"": ""f5058355-2ff0-4796-af3c-001a989bfaa5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""336d7ac7-891c-4788-af5d-f90956dc7a7f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""869118c2-b7b7-4076-884a-a4a8df63cf9d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""619d90e2-3587-4db7-9e62-db0b18f4ff32"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""326b9956-8642-46fd-83eb-149fbcd13dd2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ed79c617-b92f-4726-b014-ce0c2f004346"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ecb9f1f8-eb84-4fe9-86dc-22ec8e1090d6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d10634e5-16d6-4b4a-a928-622a09c4785b"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""203cbdca-d85e-41c4-b486-f73dcfe1d37a"",
                    ""path"": ""<Gamepad>/dpad/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""953d709a-c5fa-4598-a510-508b83cff16f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85fbc9f6-7b5c-47c4-a961-bf198eb04da8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8dbc074-f892-414b-a7eb-180efa525687"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b83a8c60-d3ad-43a7-b98a-21270deb6a1e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b28d9e2-9194-4e57-9881-2f8d60491f0d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7187a95b-63e4-43ab-a2d9-c4f102f07b66"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1e55865-0ebe-413f-bc8c-e2839a97b732"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf8ad2e9-081e-4e1b-a166-69656db6077b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9068864e-9f07-42aa-aa94-e3927bff92ce"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""RedSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df818a18-3416-4346-91b7-4aedd54bccdf"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RedSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4a65861-464c-474a-8af8-30141d76e0b9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""GreenSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d75e4db-dfac-434e-91f7-e84dcf34a061"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GreenSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64b06f48-a9c6-416e-8d21-cfb314d23b79"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""BlueSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82169661-c03c-4ab9-8543-fbb255b93ade"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(pressPoint=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""BlueSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""682b8d1d-c35d-4d46-bc17-5cedeaccdfcc"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""YellowSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7bf3a85-b186-4d3d-84f8-512005b85f31"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(pressPoint=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""YellowSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interface"",
            ""id"": ""ffc39475-d03f-4dbf-9f0b-6d19fcc532ec"",
            ""actions"": [
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""2b3476bc-201e-4fb9-ab7c-b52f73f1e813"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""119cefde-119b-4f58-b0e1-bcd7fb83a0a2"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""244627ec-82f7-4213-a998-967f84711cdb"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard And Mouse"",
            ""bindingGroup"": ""Keyboard And Mouse"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
            m_Main_Jump = m_Main.FindAction("Jump", throwIfNotFound: true);
            m_Main_Fire = m_Main.FindAction("Fire", throwIfNotFound: true);
            m_Main_Sprint = m_Main.FindAction("Sprint", throwIfNotFound: true);
            m_Main_RedSpell = m_Main.FindAction("RedSpell", throwIfNotFound: true);
            m_Main_GreenSpell = m_Main.FindAction("GreenSpell", throwIfNotFound: true);
            m_Main_BlueSpell = m_Main.FindAction("BlueSpell", throwIfNotFound: true);
            m_Main_YellowSpell = m_Main.FindAction("YellowSpell", throwIfNotFound: true);
            // Interface
            m_Interface = asset.FindActionMap("Interface", throwIfNotFound: true);
            m_Interface_Join = m_Interface.FindAction("Join", throwIfNotFound: true);
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

        // Main
        private readonly InputActionMap m_Main;
        private IMainActions m_MainActionsCallbackInterface;
        private readonly InputAction m_Main_Move;
        private readonly InputAction m_Main_Jump;
        private readonly InputAction m_Main_Fire;
        private readonly InputAction m_Main_Sprint;
        private readonly InputAction m_Main_RedSpell;
        private readonly InputAction m_Main_GreenSpell;
        private readonly InputAction m_Main_BlueSpell;
        private readonly InputAction m_Main_YellowSpell;
        public struct MainActions
        {
            private @MainControls m_Wrapper;
            public MainActions(@MainControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Main_Move;
            public InputAction @Jump => m_Wrapper.m_Main_Jump;
            public InputAction @Fire => m_Wrapper.m_Main_Fire;
            public InputAction @Sprint => m_Wrapper.m_Main_Sprint;
            public InputAction @RedSpell => m_Wrapper.m_Main_RedSpell;
            public InputAction @GreenSpell => m_Wrapper.m_Main_GreenSpell;
            public InputAction @BlueSpell => m_Wrapper.m_Main_BlueSpell;
            public InputAction @YellowSpell => m_Wrapper.m_Main_YellowSpell;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                    @Jump.started -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                    @Fire.started -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnFire;
                    @Sprint.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                    @Sprint.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                    @Sprint.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                    @RedSpell.started -= m_Wrapper.m_MainActionsCallbackInterface.OnRedSpell;
                    @RedSpell.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnRedSpell;
                    @RedSpell.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnRedSpell;
                    @GreenSpell.started -= m_Wrapper.m_MainActionsCallbackInterface.OnGreenSpell;
                    @GreenSpell.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnGreenSpell;
                    @GreenSpell.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnGreenSpell;
                    @BlueSpell.started -= m_Wrapper.m_MainActionsCallbackInterface.OnBlueSpell;
                    @BlueSpell.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnBlueSpell;
                    @BlueSpell.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnBlueSpell;
                    @YellowSpell.started -= m_Wrapper.m_MainActionsCallbackInterface.OnYellowSpell;
                    @YellowSpell.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnYellowSpell;
                    @YellowSpell.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnYellowSpell;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                    @Sprint.started += instance.OnSprint;
                    @Sprint.performed += instance.OnSprint;
                    @Sprint.canceled += instance.OnSprint;
                    @RedSpell.started += instance.OnRedSpell;
                    @RedSpell.performed += instance.OnRedSpell;
                    @RedSpell.canceled += instance.OnRedSpell;
                    @GreenSpell.started += instance.OnGreenSpell;
                    @GreenSpell.performed += instance.OnGreenSpell;
                    @GreenSpell.canceled += instance.OnGreenSpell;
                    @BlueSpell.started += instance.OnBlueSpell;
                    @BlueSpell.performed += instance.OnBlueSpell;
                    @BlueSpell.canceled += instance.OnBlueSpell;
                    @YellowSpell.started += instance.OnYellowSpell;
                    @YellowSpell.performed += instance.OnYellowSpell;
                    @YellowSpell.canceled += instance.OnYellowSpell;
                }
            }
        }
        public MainActions @Main => new MainActions(this);

        // Interface
        private readonly InputActionMap m_Interface;
        private IInterfaceActions m_InterfaceActionsCallbackInterface;
        private readonly InputAction m_Interface_Join;
        public struct InterfaceActions
        {
            private @MainControls m_Wrapper;
            public InterfaceActions(@MainControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Join => m_Wrapper.m_Interface_Join;
            public InputActionMap Get() { return m_Wrapper.m_Interface; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InterfaceActions set) { return set.Get(); }
            public void SetCallbacks(IInterfaceActions instance)
            {
                if (m_Wrapper.m_InterfaceActionsCallbackInterface != null)
                {
                    @Join.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnJoin;
                    @Join.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnJoin;
                    @Join.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnJoin;
                }
                m_Wrapper.m_InterfaceActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Join.started += instance.OnJoin;
                    @Join.performed += instance.OnJoin;
                    @Join.canceled += instance.OnJoin;
                }
            }
        }
        public InterfaceActions @Interface => new InterfaceActions(this);
        private int m_KeyboardAndMouseSchemeIndex = -1;
        public InputControlScheme KeyboardAndMouseScheme
        {
            get
            {
                if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard And Mouse");
                return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IMainActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
            void OnRedSpell(InputAction.CallbackContext context);
            void OnGreenSpell(InputAction.CallbackContext context);
            void OnBlueSpell(InputAction.CallbackContext context);
            void OnYellowSpell(InputAction.CallbackContext context);
        }
        public interface IInterfaceActions
        {
            void OnJoin(InputAction.CallbackContext context);
        }
    }
}
