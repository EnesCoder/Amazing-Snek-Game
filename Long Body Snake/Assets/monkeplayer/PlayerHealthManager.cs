using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
	public float playerHealth = 5f;
	
	private void Update()
	{
		if(playerHealth > 5f){
			playerHealth = 5f;
		}
		if(playerHealth <= 0f){
			Destroy(gameObject);
		}
		Debug.Log(playerHealth);
	}
}