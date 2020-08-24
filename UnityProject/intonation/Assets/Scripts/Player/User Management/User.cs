using System.Collections.Generic;
using EvilOwl.Player.Input_System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace EvilOwl.Player.User_Management
{
	public class User 
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public readonly MainControls Controls;
		public readonly List<InputDevice> Devices;
		
		public MainControls.MainActions Actions;
		
		private InputUser _inputUser;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		public User(InputDevice device)
		{
			Controls = new MainControls();
			Devices = new List<InputDevice>();

			_inputUser = InputUser.PerformPairingWithDevice(device: device);
			_inputUser.AssociateActionsWithUser(Controls);
			Devices.Add(device);
			Actions = Controls.Main;
		}
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		public void AddDevice(InputDevice device)
		{
			InputUser.PerformPairingWithDevice(device: device, _inputUser);
			Devices.Add(device);
		}
		
		public void RemoveDevice(InputDevice device)
		{
			_inputUser.UnpairDevice(device: device);
			Devices.Remove(device);
		}
	}
}
