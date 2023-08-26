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
    public bool AttackReady;
    public float attackRange;
    public float Cooldown;

    public float ViewRange;
    public LayerMask playerLayerMask;

    void Start()
    {
        speed = 3f;
        direction = 1;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SawPlayer = Physics2D.OverlapCircle(transform.position, ViewRange, playerLayerMask);
        AttackReady = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayerMask);

        if (SawPlayer)
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
       
        rb.velocity = new Vector2(
            speed * direction,
            rb.velocity.y
        );

        if (AttackReady && CanAttack)
        {
            anim.SetTrigger("attack");
            anim.SetBool("Attacked", false);
            Invoke("AttackDone", 0.5f);
            CanAttack = false;
        }
    }

    public void AttackDone()
    {
        anim.SetBool("Attacked", true);
        anim.ResetTrigger("attack");
        Invoke("SetCanAttack", Cooldown);
        if(AttackReady){ 
            player.GetComponent<PlayerHealthManager>().playerHealth -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRange);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void SetCanAttack()
    {
        CanAttack = true;
    }
}
