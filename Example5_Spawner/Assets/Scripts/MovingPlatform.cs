using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	CharacterController player;
	Vector3 oldPos;

	private void Update()
	{
		if(player != null)
		{
			player.Move(transform.position - oldPos);
			print(transform.position - oldPos);
			oldPos = transform.position;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
        {
			player = collider.GetComponent<CharacterController>();
			oldPos = transform.position;
		}
	}
	private void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			player = null;
		}
	}
}
