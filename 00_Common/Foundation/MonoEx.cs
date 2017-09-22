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
                    if (_animator == null)
                    {
                        Debug.LogError("Error this is no Animator component in this gameobject!");
                    }
				}
				return _animator;
			}
		}

		private Transform _cachedTransform;
		public Transform cachedTransform
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

        private Rigidbody _cachedRigidbody;
        public Rigidbody cachedRigidbody
        {
            get
            {
                if (_cachedRigidbody == null)
                {
                    _cachedRigidbody = GetComponent<Rigidbody>();
                    if (_cachedRigidbody == null)
                    {
                        Debug.LogError("Error this is no RigidBody component in this gameobject!");
                    }
                }

                return _cachedRigidbody;
            }
        }
	}	
}

