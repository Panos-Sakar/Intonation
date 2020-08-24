using System;
using System.Collections.Generic;
using EvilOwl.Player.Input_System;
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
		
		private List<InputDevice> _availableKeyboards;
		private List<InputDevice> _availableGamepads;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_availableKeyboards = new List<InputDevice>();
			_availableGamepads = new List<InputDevice>();

			foreach (var device in InputSystem.devices)
			{
				AddDevice(device);
			}

			if (_availableKeyboards.Count > 0)
			{
				print("Attached keyboard to mainPlayer");
				mainUser.InitializeInput(_availableKeyboards[0]);
			}
			else if (_availableGamepads.Count > 0)
			{
				print("Attached Gamepad to mainPlayer");
				mainUser.InitializeInput(_availableGamepads[0]);
			}
			
			InputSystem.onDeviceChange += DeviceChanged;
		}
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		private void AddDevice(InputDevice device)
		{
			switch (device)
			{
				case Keyboard _:
					//print($"There is a keyboard, ID:{device.deviceId}");
					_availableKeyboards.Add(device);
					break;
				case Gamepad _:
					//print($"There is a gamepad, ID:{device.deviceId}");
					_availableGamepads.Add(device);
					break;
			}
		}
		private void DeviceChanged(InputDevice device, InputDeviceChange change)
		{
			switch (change)
			{
				case InputDeviceChange.Added:
					print($"New device, ID:{device.deviceId}");
					AddDevice(device);
					mainUser.user.AddDevice(device);
					break;
				case InputDeviceChange.Removed:
					if(mainUser.user.Devices.Contains(device)) mainUser.user.RemoveDevice(device);
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
	}
}
