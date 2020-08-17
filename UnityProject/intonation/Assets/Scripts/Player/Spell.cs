using System;
using EvilOwl.Core;
using Pathfinding;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace EvilOwl.Player
{
	public class Spell : MonoBehaviour
	{
#pragma warning disable CS0649
		/*****************************
		 *         Variables         *
		 *****************************/
		[SerializeField] private SpriteRenderer spellSprite;
		[SerializeField] private SpellColors effectColours;
		[SerializeField] private Light2D pointLight;
		[SerializeField] private AIDestinationSetter aStarDestinationSetter;
		[SerializeField] private DistanceJoint2D joint;

		private int _position;
		private float _spacing;
		public SpellType type;
		private bool _useJoint;
		
		private bool _isSpellLeader;
		private GameObject _previousSpell;
		private GameObject _nextSpell;

		private bool _lerpLight;
		private float _lerpTarget;
		private float _lerpStart;
		private float _lerpPercent;

#pragma warning restore CS0649

		/*****************************
		 *           Init            *
		 *****************************/

		/*****************************
		 *          Update           *
		 *****************************/
		private void FixedUpdate()
		{
			if (_lerpLight)
			{
				pointLight.intensity = Mathf.Lerp(_lerpStart, _lerpTarget, _lerpPercent);
				_lerpPercent += 0.05f;
				if (_lerpPercent > 1) _lerpLight = false;
			}
			
		}

		/*****************************
		 *          Methods          *
		 *****************************/
		public void Initialise(int position, float spacing, SpellType typeOfSpell, bool useJoint)
		{
			_position = position;
			_spacing = spacing;
			type = typeOfSpell;
			_useJoint = useJoint;
			_lerpStart = pointLight.intensity;
			switch (type)
			{
				case SpellType.Red:
					spellSprite.color = effectColours.redEffectColour;
					pointLight.color = effectColours.redEffectColour;
					break;
				case SpellType.Green:
					spellSprite.color = effectColours.greenEffectColour;
					pointLight.color = effectColours.greenEffectColour;
					break;
				case SpellType.Blue:
					spellSprite.color = effectColours.blueEffectColour;
					pointLight.color = effectColours.blueEffectColour;
					break;
				case SpellType.Yellow:
					spellSprite.color = effectColours.yellowEffectColour;
					pointLight.color = effectColours.yellowEffectColour;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public void PlaceSpellAroundCircle (Vector3 center, float radius, float offset){
			
			var radians = 2 * Math.PI + (_position * (_spacing - 1) + offset);
			var vertical = (float) Math.Sin(radians);
			var horizontal = (float) Math.Cos(radians); 
	         
			var spawnDir = new Vector3 (horizontal, vertical, 0);
			
			transform.localPosition = center + spawnDir * radius;
			
		}

		public void SetNextSpell(GameObject nextSpell)
		{
			_nextSpell = nextSpell;
		}

		public void SetPreviousSpell(GameObject previousSpell)
		{
			_previousSpell = previousSpell;
		}

		public void MakeSpellLeader()
		{
			_isSpellLeader = true;
			var spellLeader = gameObject.AddComponent(typeof(SpellLeader)) as SpellLeader;
			if (spellLeader != null) spellLeader.spellObject = this;
		}

		public void SetSpellTarget(GameObject target)
		{
			aStarDestinationSetter.target = _isSpellLeader ? target.transform : _previousSpell.transform;

			if (_useJoint)
			{
				if(_isSpellLeader) return;
				joint.enabled = true;
				joint.connectedBody = aStarDestinationSetter.target.GetComponent<Rigidbody2D>();
			}
		}

		public void SelfDestroy()
		{
			if(_nextSpell != null) _nextSpell.GetComponent<Spell>().SelfDestroy();
			Destroy(gameObject);
			
			//TODO: Set next spell in chain to follow enemy?
			
		}

		public void LerpLight(float target)
		{
			_lerpTarget = target;
			_lerpLight = true;
		}
	}
}
