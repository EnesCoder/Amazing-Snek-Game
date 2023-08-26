using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    [SerializeField] trident t;
	
	[SerializeField] public GameObject boneObj;
	[SerializeField] GameObject pirateSword;

	[SerializeField] ParticleSystem blood;

	[SerializeField] cinemachineShake cs;

	bool spawnedBones;
	bool gotDamagedTrident;
	bool gotDamagedMelee;
	
	public float health = 1f;
	public float tridentMeleeDamage = 1f;
	public float throwDamageDivider = 5f;
	
	void Start()
	{
		gotDamagedTrident = false;
		gotDamagedMelee = false;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "tridentTip"){
            if(t.hasEnoughVel || t.playerAttacking){
                if(t.thrown){
					health -= 2;
					Debug.Log("trident thrown");
					cs.screenShake(1f, 0.2f);
				}
				if(t.playerAttacking){
					health -= 1;
					Debug.Log("trident melee");
					cs.screenShake(1f, 0.2f);
				}
            }
        }
    }
	
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "tridentTip"){
            if(t.hasEnoughVel || t.playerAttacking){
                if(t.thrown && !gotDamagedTrident){
					health -= t.throwForce / throwDamageDivider;
					gotDamagedTrident = true;
				}
				if(t.playerAttacking && !gotDamagedMelee){
					health -= tridentMeleeDamage;
					gotDamagedMelee = true;
				}
            }
        }
    }
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "tridentTip"){
			if(t.thrown && gotDamagedTrident)
				gotDamagedTrident = false;
		}
	}
	
    void Update()
    {
		if(health <= 0f){
			Die();
		} 
		
		if(!t.playerAttacking){
			gotDamagedMelee = false;
		}
    }
	
	public void Die(){
		int count = Random.Range(1, 1);
		float g = 1f;
		Vector3 pos = transform.position;
		if(spawnedBones) return;
		Destroy(GetComponent<Rigidbody2D>());
		if(this.gameObject.tag == "pirate"){
			blood.Play();
			Destroy(pirateSword);
		}
		if(this.gameObject.tag == "Fish"){
			GameManager.SpawnLittleObjects(count, boneObj, g, pos);
		}
		Destroy(GetComponent<SpriteRenderer>());
		Destroy(GetComponent<Collider2D>());
		spawnedBones = true;
	}
}
