using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerDoAction : MonoBehaviour {

	public UnityEvent enterAction;
	public UnityEvent exitAction;
	public string targetTag;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(targetTag))
		{
            enterAction.Invoke();
		}
	}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            exitAction.Invoke();
        }
    }
}
