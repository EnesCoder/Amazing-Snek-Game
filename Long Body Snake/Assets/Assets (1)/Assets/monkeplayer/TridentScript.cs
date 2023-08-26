using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentScript : MonoBehaviour
{
	public Transform player;
	[SerializeField] trident tridentMelee;
	public float maxThrowForce = 100f;
	private float throwForce;
	private bool beingThrown;
	private Vector3 mouseDist;
	private Vector3 mousePos;
	int flipped;
	bool lookingAtCursor;
	
	[Header("Keys And Buttons")]
	public int setButton = 1;
	
	private void Update()
	{
		if(player.localScale.x < 0f){
			flipped = -1;
		}
		if(player.localScale.x > 0f){
			flipped = 0;
		}
		if(beingThrown && GetComponent<BoxCollider2D>() != null){
			Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.gameObject.GetComponent<Collider2D>()); //ignore collision with player
		}
		mousePos = Input.mousePosition; //why is this here?
		mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
		
		if(Input.GetMouseButton(setButton)){
			LookAtCursor();
			throwForce += Time.deltaTime;
			throwForce = Mathf.Min(throwForce, maxThrowForce);
			lookingAtCursor = true;
			//makes sense
		}
		if(Input.GetMouseButtonUp(setButton)){
			gameObject.AddComponent<Rigidbody2D>();
			gameObject.AddComponent<BoxCollider2D>();
			beingThrown = true;
			StartCoroutine("Throw");
		}
		if(lookingAtCursor == true){
		mouseDist = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0f);
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		if(flipped == 0){
			transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
		}
		if(flipped == -1){
			transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle - 90);
		}
		}
	}
	
	private void FixedUpdate()
	{
		if(GetComponent<Rigidbody2D>() == null && !Input.GetMouseButton(setButton) && tridentMelee.playerAttacking == false){
			//reset();
		}
	}
	
	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.CompareTag("Player") && GetComponent<Rigidbody2D>() != null){ 
			//reset();
		}
	}
	
	private void LookAtCursor(){
		// mouseDist = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0f);
		// float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		// if(flipped == 0){
		// 	transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
		// }
		// if(flipped == -1){
		// 	transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle - 90);
		// }
	}
	
	// public void Throw(){
	// 	// Rigidbody2D rb = GetComponent<Rigidbody2D>();
	// 	// rb.AddForce(throwForce * transform.right, ForceMode2D.Impulse);
	// 	// float oGravity = rb.gravityScale;
	// 	// rb.gravityScale = 0f;
	// 	// while(rb.velocity.magnitude > 0f){
	// 	// 	rb.gravityScale = 0f;
	// 	// }
	// 	// rb.gravityScale = oGravity;
	// 	// beingThrown = false;
	// 	Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //     rb.AddForce(throwForce * transform.right, ForceMode2D.Impulse);
    //     float oGravity = rb.gravityScale;
    //     rb.gravityScale = 0f;
    //     yield return new WaitForSeconds(0.1f); // Wait for a short time
    //     rb.gravityScale = oGravity;
    //     beingThrown = false;
	// 	reset();
	// }
	public IEnumerator Throw()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(throwForce * transform.right * 10 * flipped, ForceMode2D.Impulse);
        float oGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.1f); // Wait for a short time
        rb.gravityScale = oGravity;
        beingThrown = false;
    }
	public void reset(){
		transform.position = new Vector3(player.transform.position.x + 0.5f,player.transform.position.y + 0f, 1);
		transform.eulerAngles = new Vector3(player.transform.rotation.x, player.transform.rotation.y, 55f);	
	}
}
