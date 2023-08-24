using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	public float fishDamage = 3f;
	
    private Rigidbody2D rb;

    public bool SawPlayer = false;
    private PlayerMovement p;
	private PlayerHealthManager ph;
    public float speed;

    public Transform ViewPos;
    public float ViewRange;
    public LayerMask playerLayerMask;

    private int dir;
    private int dirY;
    public Vector2 maxSpeed;
	
	public float knockbackForce = 2.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        p = FindObjectOfType<PlayerMovement>();
		ph = FindObjectOfType<PlayerHealthManager>();
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
			if(rb != null){
				rb.AddForce(Vector3.right * speed * dir * Time.deltaTime);
				rb.AddForce(Vector3.up * speed * dirY * Time.deltaTime);


				if (rb.velocity.x > maxSpeed.x || rb.velocity.y > maxSpeed.y)
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
    }
	
	private void OnCollisionEnter2D(Collision2D col)
	{
		var obj = col.collider.gameObject;
		if(obj.GetComponent<PlayerMovement>()){
			ph.playerHealth -= fishDamage;
			obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0f) * knockbackForce, ForceMode2D.Impulse);
		}
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ViewPos.position, ViewRange);
    }
}
