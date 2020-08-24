using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EvilOwl.Player.Vfx
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
		public Gradient defaultGradient;
		private List<Color> _colors;
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
			_colors = new List<Color>();
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

		public void AddGradientColor(Color newColor)
		{
			_colors.Add(newColor);
			
			var main = ps.main;
			var newGradient = new Gradient();

			var colorKey = new GradientColorKey[_colors.Count];
			for (var i = 0; i < _colors.Count; i++)
			{
				colorKey[i].color = _colors[i];
				colorKey[i].time = (float) i/_colors.Count;
			}
			
			var alphaKey = new GradientAlphaKey[1];
			alphaKey[0].alpha = 0.8f;
			alphaKey[0].time = 0.0f;
			
			newGradient.SetKeys(colorKey, alphaKey);
			
			main.startColor = new ParticleSystem.MinMaxGradient(newGradient);
		}
		public void StopVfx(VfxName vfxName)
		{
			foreach (var vfx in _vfx.Where(vfx => vfx.Name == vfxName))
			{
				vfx.Enabled = false;
				vfx.StopVfx();
				var main = ps.main;
				main.startColor = new ParticleSystem.MinMaxGradient(defaultGradient);
				ps.Stop();
				_colors.Clear();
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
