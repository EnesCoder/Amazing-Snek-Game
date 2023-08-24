using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
	public float forceMultiplier = 2f;
	private Rigidbody2D rb;
	public float rotationSpd = 30f;
	private bool rotateClockwise;
	public float playerDamage = 2f;
	public float playerPush = 2f;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		rotateClockwise = true;
		
		AddStartForce();
	}
	
	private void AddStartForce(){
		Vector2 forceDir = Random.insideUnitCircle * forceMultiplier;
		rb.AddForce(forceDir, ForceMode2D.Impulse);
	}
	
	private void Update()
	{
		transform.eulerAngles += new Vector3(0f, 0f, 
		(rotateClockwise ? rotationSpd : -rotationSpd) * Time.deltaTime);
	}
	
	private void OnCollisionEnter2D(Collision2D c)
	{
		var obj = c.collider.gameObject;
		
		rotateClockwise = !rotateClockwise;
		
		if(c.collider.CompareTag("Player")){
			var ph = obj.GetComponent<PlayerHealthManager>();
			var r = obj.GetComponent<Rigidbody2D>();
			var forceDir = obj.transform.position.x < transform.position.x ? -1f : 1f;
			r.AddForce(new Vector2(forceDir * playerPush, 0f), ForceMode2D.Impulse);
			ph.playerHealth -= playerDamage;
		}
	}
}
