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
	public LayerMask tridentLayerMask;
	
	public float knockbackForce = 2.5f;
	public float attackGetPushed = 1.5f;
	
	private float cooldown = 0f;
	public float cooldownOrigin = 0.4f;

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
        
		if(rb && !CanAttack){
			rb.velocity = new Vector2(
				speed * direction,
				rb.velocity.y
			);
		}
       
        rb.velocity = new Vector2(
            speed * direction,
            rb.velocity.y
        );

        if (CanAttack)
        {
			cooldown -= Time.deltaTime;
			if(cooldown <= 0f){
            anim.SetTrigger("attack");
            anim.SetBool("Attacked", false);
            Invoke("AttackDone", 0.4f);

			var p = player.GetComponent<PlayerMovement>();
			p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			p.gotKnocked = true;
			p.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * knockbackForce, 0f), ForceMode2D.Impulse);
			
			rb.AddForce(new Vector2(direction * knockbackForce, 0f), ForceMode2D.Impulse);

			cooldown = cooldownOrigin;
			}
            Invoke("AttackDone", 0.5f);
            CanAttack = false;
        }
    }

    public void AttackDone()
    {
        anim.SetBool("Attacked", true);
        anim.ResetTrigger("attack");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
