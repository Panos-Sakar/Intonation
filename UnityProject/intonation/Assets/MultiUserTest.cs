using EvilOwl.Player.Input_System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace EvilOwl
{
	public class MultiUserTest : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		public InputUser user1;
		public InputUser user2;
		private MainControls _controls1;
		private MainControls _controls2;
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_controls1 = new MainControls();
			_controls2 = new MainControls();
			
			user1 = InputUser.PerformPairingWithDevice(device: InputSystem.devices[0]);
			user1.AssociateActionsWithUser(_controls1);

			_controls1.Main.Fire.performed += ctx => Fire("First", user1);
			_controls2.Main.Fire.performed += ctx => Fire("Second", user2);
			
			InputSystem.onDeviceChange +=
				(device, change) =>
				{
					switch (change)
					{
						case InputDeviceChange.Added:
							user2 = InputUser.PerformPairingWithDevice(device: device);
							user2.AssociateActionsWithUser(_controls2);
							break;
					}
				};
		}
		/*****************************
		 *          Update           *
		 *****************************/
		private void Fire( string player, InputUser user)
		{
			
			if (user.valid)
			{
				print($"Fire from {player}. User ID: {user.id}");
			}
			
		}
		/*****************************
		 *          Methods          *
		 *****************************/
		private void OnEnable()
		{
			_controls1.Enable();
			_controls2.Enable();
		}

		private void OnDisable()
		{
			_controls1.Disable();
			_controls2.Disable();
		}
	}
}
