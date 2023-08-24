using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
	public float playerHealth = 6f;
	
	private void Update()
	{
		if(playerHealth <= 0f) Debug.Log("Ded");
		Debug.Log(playerHealth);
	}
}

