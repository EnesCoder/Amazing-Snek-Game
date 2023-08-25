using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirate : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] Transform attackPoint;
    int direction;

    public bool SawPlayer = false;
    public bool CanAttack;
    public float attackRange;

    public float ViewRange;
    public LayerMask playerLayerMask;
<<<<<<< HEAD:Long Body Snake/Assets/pirate.cs
	public LayerMask tridentLayerMask;
	
	public float knockbackForce = 2.5f;
	public float attackGetPushed = 1.5f;
	
	private float cooldown = 0f;
	public float cooldownOrigin = 0.4f;
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d:Long Body Snake/Assets/monkeplayer/pirate.cs

    void Start()
    {
        anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        SawPlayer = Physics2D.OverlapCircle(transform.position, ViewRange, playerLayerMask);
        CanAttack = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayerMask) || 
		Physics2D.OverlapCircle(attackPoint.position, attackRange, tridentLayerMask);

        if (SawPlayer && !CanAttack)
        {
            anim.SetBool("movement", true);
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(
                    1f,
                    transform.localScale.y,
                    transform.localScale.z
                );
                direction = 1;
            }
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(
                    -1f,
                    transform.localScale.y,
                    transform.localScale.z
                );
                direction = -1;
            }
        }
        else
        {
            anim.SetBool("movement", false);
            direction = 0;
        }
<<<<<<< HEAD:Long Body Snake/Assets/pirate.cs
        
		if(rb && !CanAttack){
			rb.velocity = new Vector2(
				speed * direction,
				rb.velocity.y
			);
		}
=======
       
        rb.velocity = new Vector2(
            speed * direction,
            rb.velocity.y
        );
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d:Long Body Snake/Assets/monkeplayer/pirate.cs

        if (CanAttack)
        {
			cooldown -= Time.deltaTime;
			if(cooldown <= 0f){
            anim.SetTrigger("attack");
            anim.SetBool("Attacked", false);
<<<<<<< HEAD:Long Body Snake/Assets/pirate.cs
            Invoke("AttackDone", 0.4f);
            player.GetComponent<PlayerHealthManager>().playerHealth -= pirateDamage;

			var p = player.GetComponent<PlayerMovement>();
			p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			p.gotKnocked = true;
			p.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * knockbackForce, 0f), ForceMode2D.Impulse);
			
			rb.AddForce(new Vector2(direction * knockbackForce, 0f), ForceMode2D.Impulse);

			cooldown = cooldownOrigin;
			}
=======
            Invoke("AttackDone", 0.5f);
            CanAttack = false;
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d:Long Body Snake/Assets/monkeplayer/pirate.cs
        }
    }

    public void AttackDone()
    {
        anim.SetBool("Attacked", true);
        anim.ResetTrigger("attack");
<<<<<<< HEAD:Long Body Snake/Assets/pirate.cs
=======
        Invoke("SetCanAttack", Cooldown);
        if(AttackReady){ 
            player.GetComponent<PlayerHealthManager>().playerHealth -= 1;
        }
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d:Long Body Snake/Assets/monkeplayer/pirate.cs
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
