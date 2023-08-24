using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    [SerializeField] trident t;
	
	[SerializeField] public GameObject boneObj;
	[SerializeField] GameObject pirateSword;

	[SerializeField] ParticleSystem blood;
	
	private Rigidbody2D rb;

	bool spawnedBones;
	bool gotDamagedTrident;
	bool gotDamagedMelee;
	
	public float health = 1f;
	public float tridentMeleeDamage = 1f;
	public float throwDamageDivider = 10f;
	
	public float tridentMeleeKnockback = 2f;
	
	void Start()
	{
		gotDamagedTrident = false;
		gotDamagedMelee = false;
		
		rb = GetComponent<Rigidbody2D>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "tridentTip"){
            if(t.hasEnoughVel || t.playerAttacking){
                if(t.thrown)
					health -= t.throwForce / throwDamageDivider;
					if(rb)
						rb.AddForce(new Vector2(t.GetComponent<Rigidbody2D>().velocity.x, 0f), ForceMode2D.Impulse);
				else
					health -= tridentMeleeDamage;
					if(rb)
						rb.AddForce(new Vector2(tridentMeleeKnockback, 0f), ForceMode2D.Impulse);
            }
        }
    }
	
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "tridentTip"){
            if(t.hasEnoughVel || t.playerAttacking){
                if(t.thrown && !gotDamagedTrident){
					health -= t.throwForce / throwDamageDivider;
					if(rb)
						rb.AddForce(new Vector2(t.GetComponent<Rigidbody2D>().velocity.x, 0f), ForceMode2D.Impulse);
					gotDamagedTrident = true;
				}
				if(t.playerAttacking && !gotDamagedMelee){
					health -= tridentMeleeDamage;
					if(rb)
						rb.AddForce(new Vector2(tridentMeleeKnockback, 0f), ForceMode2D.Impulse);
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
		if(health <= 0f) Die();
        Debug.Log(t.hasEnoughVel);
		
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
