using System;
using System.Collections.Generic;
using System.Linq;
using EvilOwl.Player.Input_System;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EvilOwl.Player.User_Management
{
	public class PlayerManager : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private InputManager mainUser;
		[SerializeField] private GameObject playerPrefab;
		[SerializeField][PositiveValueOnly] private int maxUsers;
		
		private List<InputDevice> _availableKeyboards;
		private List<InputDevice> _availableGamepads;
		private List<InputManager> _users;
		
		private MainControls _controls;
		private InputDevice _mainUserDevice;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_controls = new MainControls();

			_availableKeyboards = new List<InputDevice>();
			_availableGamepads = new List<InputDevice>();
			_users = new List<InputManager>();

			foreach (var device in InputSystem.devices)
			{
				CaptureDevice(device);
			}
			
			var mainPlayerIsInitialized = AddAllDevicesToMainUser(_availableKeyboards, false);
			AddAllDevicesToMainUser(_availableGamepads, mainPlayerIsInitialized);
			
			InputSystem.onDeviceChange += DeviceChanged;

			_controls.Interface.Join.performed += PlayerJoin;
		}
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		private void CaptureDevice(InputDevice device)
		{
			switch (device)
			{
				case Keyboard _:
					
					if (_availableKeyboards.Contains(device)) return;
					_availableKeyboards.Add(device);
					break;
				case Gamepad _:
					
					if (_availableGamepads.Contains(device)) return;
					_availableGamepads.Add(device);
					break;
			}
		}

		private bool AddAllDevicesToMainUser(IEnumerable<InputDevice> devices, bool mainPlayerIsInitialized)
		{
			foreach (var device in devices)
			{
				if (mainPlayerIsInitialized)
				{
					AddDeviceToUser(mainUser, false, device);
				}
				else
				{
					AddDeviceToUser(mainUser, true, device);
					mainPlayerIsInitialized = true;
				}
			}
			
			return mainPlayerIsInitialized;
		}
		
		private void AddDeviceToUser(InputManager user, bool initialiseUser, InputDevice device)
		{
			if (initialiseUser)
			{
				
				user.InitializeInput(device);
				_users.Add(user);
			}
			else
			{
				user.user.AddDevice(device);
			}
			
		}
		
		private void DeviceChanged(InputDevice device, InputDeviceChange change)
		{
			switch (change)
			{
				case InputDeviceChange.Added:
					print($"Info: Device Added: {device}");
					
					CaptureDevice(device);
					
					var deviceIsOrphan = true;
					foreach (var unused in _users.Where(user => user.user.Devices.Contains(device)))
					{
						deviceIsOrphan = false;
					}
					
					if(deviceIsOrphan) mainUser.user.AddDevice(device);
					
					break;
				case InputDeviceChange.Removed:
					break;
				case InputDeviceChange.Disconnected:
					break;
				case InputDeviceChange.Reconnected:
					break;
				case InputDeviceChange.Enabled:
					break;
				case InputDeviceChange.Disabled:
					break;
				case InputDeviceChange.UsageChanged:
					break;
				case InputDeviceChange.ConfigurationChanged:
					break;
				case InputDeviceChange.Destroyed:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(change), change, null);
			}
		}

		private void PlayerJoin(InputAction.CallbackContext ctx)
		{
			if (_users.Count >= maxUsers)
			{
				print("Game can't host more users");
				return;
			}

			var deviceToChange = ctx.control.device;
			var userHasOneDevice = false;
			foreach (var user in _users.Where(user => user.user.Devices.Contains(deviceToChange)))
			{
				if(user.user.Devices.Count <= 1) userHasOneDevice = true;
				else user.user.RemoveDevice(deviceToChange);
			}

			if (userHasOneDevice)
			{
				print("Can't Remove Device from user");
				return;
			}

			var newUser = Instantiate(playerPrefab);
			var newUserInputManager = newUser.GetComponent<InputManager>();

			newUser.name = $"Player_{_users.Count + 1}";
			newUserInputManager.InitializeInput(ctx.control.device);

			_users.Add(newUserInputManager);
		}
		
		private void OnEnable()
		{
			_controls?.Enable();
		}

		private void OnDisable()
		{
			_controls?.Disable();
		}
	}
}
