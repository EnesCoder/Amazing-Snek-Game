using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool SawPlayer = false;
    private PlayerMovement p;
    public float speed;

    public Transform ViewPos;
    public float ViewRange;
    public LayerMask playerLayerMask;

    private int dir;
    private int dirY;
    public Vector2 maxSpeed;
<<<<<<< HEAD
	
	public float knockbackForce = 2.5f;
	public float attackGetPushed = 1.5f;
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d

    [SerializeField] PlayerHealthManager phm;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        p = FindObjectOfType<PlayerMovement>();
    }
     
    void Update()
    {
        SawPlayer = Physics2D.OverlapCircle(ViewPos.position, ViewRange, playerLayerMask);

        if (SawPlayer && p.InWater)
        {

            Vector3 direction = p.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            //transform.position = Vector2.MoveTowards(transform.position, p.transform.position, speed * Time.deltaTime);
            rb.AddForce(Vector3.right * speed * dir * Time.deltaTime);
            rb.AddForce(Vector3.up * speed * dirY * Time.deltaTime);


<<<<<<< HEAD
            if (Mathf.Abs(rb.velocity.x) > maxSpeed.x || Mathf.Abs(rb.velocity.y) > maxSpeed.y)
=======
            if (rb.velocity.x > maxSpeed.x || rb.velocity.y > maxSpeed.y)
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
            {
                rb.velocity = maxSpeed;
            }


            if (p.transform.position.x > transform.position.x){
                dir = 1;
                transform.localScale = new Vector2(-0.5483357f, 0.5483357f);
            }
            else
            {
                dir = -1;
                transform.localScale = new Vector2(-0.5483357f, -0.5483357f);
            }

            if (p.transform.position.y > transform.position.y){
                dirY = 1;
            }
            else{
                dirY = -1;
            }

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ViewPos.position, ViewRange);
    }
<<<<<<< HEAD
	
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player" || other.gameObject.tag == "trident")
        {
            phm.playerHealth -= 1f;
<<<<<<< HEAD
			p.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			p.gotKnocked = true;
			p.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * knockbackForce, 0f), ForceMode2D.Impulse);
			rb.velocity = Vector2.zero;
			rb.AddForce(transform.right * attackGetPushed, ForceMode2D.Impulse);
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
        }
    }
}
