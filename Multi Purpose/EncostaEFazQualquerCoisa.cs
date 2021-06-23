using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncostaEFazQualquerCoisa : MonoBehaviour {

	public string targetTag;
	public UnityEvent collisionEvent;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(targetTag))
		{
			collisionEvent.Invoke();
		}
	}
}

