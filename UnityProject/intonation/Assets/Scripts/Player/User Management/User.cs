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

		private InputUser _user;

#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		public User(InputDevice device)
		{
			Controls = new MainControls();
			Devices = new List<InputDevice>();

			_user = InputUser.PerformPairingWithDevice(device: device);
			_user.AssociateActionsWithUser(Controls);
			Devices.Add(device);
		}
		/*****************************
		 *          Update           *
		 *****************************/

		/*****************************
		 *          Methods          *
		 *****************************/
		public void AddDevice(InputDevice device)
		{
			if(Devices.Contains(device)) return;
			
			InputUser.PerformPairingWithDevice(device: device, _user);
			Devices.Add(device);
		}
		
		public void RemoveDevice(InputDevice device)
		{
			_user.UnpairDevice(device: device);
			Devices.Remove(device);
		}
	}
}
