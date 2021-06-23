using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterDoDamage : MonoBehaviour {

	public float damage;
	public string targetTag;

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag(targetTag))
			col.SendMessage("ReceiveDamage", damage, SendMessageOptions.DontRequireReceiver);
	}
}
