using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static void SpawnLittleObjects(int count, GameObject obj, float g, Vector3 pos, float force = 2f){
		for(int i = 0; i < count; i++){
			var rot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360));
			var inst = Instantiate(obj, pos, rot);
			Rigidbody2D rb = null;
			if (inst.GetComponent<Rigidbody2D>() == null)
				rb = inst.AddComponent<Rigidbody2D>();
			rb.gravityScale = g;
			Vector2 forceDir = Random.insideUnitCircle;
			rb.AddForce(forceDir * force, ForceMode2D.Impulse);
		}
	}
}
