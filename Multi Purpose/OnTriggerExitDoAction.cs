using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerExitDoAction : MonoBehaviour {
	
	public string targetTag;
	public UnityEvent collisionEvent;

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag(targetTag))
		{
			collisionEvent.Invoke();
		}
	}
}
