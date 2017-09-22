using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{

	public class MonoEx : MonoBehaviour 
	{
		private Animator _animator;
		protected Animator animator
		{
			get
			{
				if (_animator == null) 
				{
					_animator = GetComponent<Animator> ();
				}
				return _animator;
			}			
		}

		private Transform _cachedTransform;
		private Transform cachedTransform
		{
			get
			{
				if (_cachedTransform == null) 
				{
					_cachedTransform = transform;
				}
				return _cachedTransform;
			}
		}
	}	
}

