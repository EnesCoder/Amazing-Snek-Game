using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
	public float playerHealth = 5f;

	public Vector2 respawnPos;

    private void Start()
    {
		respawnPos = transform.position;
    }

    private void Update()
	{
		if(playerHealth > 5f){
			playerHealth = 5f;
		}
		if(playerHealth <= 0f){
			playerHealth = 5f;
			transform.position = respawnPos; 
		}
		Debug.Log(playerHealth);
	}
}