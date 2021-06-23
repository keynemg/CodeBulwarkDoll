using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{

	Vector3 startPosition;
	public Transform endPosition;
	float timer;
	public float speed;
	int multiplier = 1;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		
		float distance = Vector3.Distance(startPosition, endPosition.position);

		timer += Time.deltaTime * (speed / distance) * multiplier;
		timer = Mathf.Clamp(timer, 0, 1);
		if (timer >= 1)		
			multiplier = -1;
		
		else if (timer <= 0)
			multiplier = 1;
		transform.position = Vector3.Lerp(startPosition, endPosition.position, timer);
	}
}
