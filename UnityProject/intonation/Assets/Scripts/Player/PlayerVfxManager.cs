using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EvilOwl.Player
{
	public class PlayerVfxManager : MonoBehaviour
	{
		//TODO: Remake this from scratch
		
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		
		private List<Vfx> _vfx;
		public ParticleSystem ps;
		
#pragma warning restore CS0649
		/*****************************
		 *           Init            *
		 *****************************/
		private void Awake()
		{
			_vfx = new List<Vfx>
			{
				new Vfx()
			};

			_vfx[0].Name = VfxName.SpellOrb;
			_vfx[0].Enabled = false;
			_vfx[0].VfxType = VfxTypes.ParticleSystem;
		}
		/*****************************
		 *          Update           *
		 *****************************/
		
		/*****************************
		 *          Methods          *
		 *****************************/
		public void StartVfx(VfxName vfxName)
		{
			foreach (var vfx in _vfx.Where(vfx => vfx.Name == vfxName))
			{
				vfx.Enabled = true;
				vfx.StartVfx();
				ps.Play();
			}
		}

		public void StopVfx(VfxName vfxName)
		{
			foreach (var vfx in _vfx.Where(vfx => vfx.Name == vfxName))
			{
				vfx.Enabled = false;
				vfx.StopVfx();
				ps.Stop();
			}
		}
	}

	public class Vfx 
	{
		public bool Enabled;
		public VfxName Name;
		public VfxTypes VfxType;

		public void StartVfx()
		{
			if(Enabled) return;
			switch (VfxType)
			{
				case VfxTypes.None:
					break;
				case VfxTypes.ParticleSystem:
					break;
				case VfxTypes.VisualEffectsGraph:
					break;
				case VfxTypes.Custom:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void StopVfx()
		{
			if(!Enabled) return;
			
			switch (VfxType)
			{
				case VfxTypes.None:
					break;
				case VfxTypes.ParticleSystem:
					break;
				case VfxTypes.VisualEffectsGraph:
					break;
				case VfxTypes.Custom:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
	
	public enum VfxTypes
	{
		None = 0,
		ParticleSystem = 1,
		VisualEffectsGraph = 2,
		Custom = 3
	}

	public enum VfxName
	{
		SpellOrb
	}
}
