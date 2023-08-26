using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed = 5f;
	public float jumpHeight = 6f;
	private Rigidbody2D rb;
	private float horM;

	Vector3 mousePos;

	[SerializeField] GameObject trident;
	[SerializeField] Animator pAnim;
	public Transform groundChecker;
	public float groundCheckRad = .5f;
	public LayerMask groundLayer;
	public LayerMask gooLayer;
	private bool grounded;
	private bool gooed;
	public float gooMovMultiplier = 0.5f;
	private bool canDoubleJump;
	private bool canJump;
	
	public float fallDamageTimer = 2.5f;
	public float fallDamage = 2f;
	private float fdtStart;
	
	private PlayerHealthManager ph;
	
	private bool facingRight = true;

	public bool InWater = false; 
	public float waterMovementMultiplier = 1f;
	
	public bool gotKnocked;
	public float knockTime = 0.5f;
	private float knockTimeStart;
	
	[Header("Keys")]
	public KeyCode jumpKey = KeyCode.Space;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		ph = GetComponent<PlayerHealthManager>();
		
		fdtStart = fallDamageTimer;
		gotKnocked = false;
		knockTimeStart = knockTime;
	}
	
	private void Update()
	{
		mousePos = Input.mousePosition;
		mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
		grounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRad, groundLayer);
		gooed = Physics2D.OverlapCircle(groundChecker.position, groundCheckRad, gooLayer);
		canJump = grounded || canDoubleJump;
		if(grounded && !Input.GetKey(jumpKey)) canDoubleJump = false;
		Jump();
		
		horM = Input.GetAxis("Horizontal");

		if(horM != 0 && grounded){
			pAnim.SetBool("walking", true);
		}
		if(horM == 0 || !grounded){
			pAnim.SetBool("walking", false);
		}
		
		// fall damage
		if(!grounded && rb.velocity.y < 0.5f)
			fallDamageTimer -= Time.deltaTime;		
		if(grounded){
			if(fallDamageTimer <= 0f)
				ph.playerHealth -= fallDamage;
			fallDamageTimer = fdtStart;
		}
		
		// flip
		Flip();
		
		// getting knockbacked
		if(gotKnocked)
			knockTime -= Time.deltaTime;
		if(knockTime <= 0f){
			gotKnocked = false;
			knockTime = knockTimeStart;
		}
	}
	
	private void FixedUpdate()
	{
		Movement();
	}
	
	private void Movement(){
		if(!gotKnocked){
		if(!InWater)
			rb.velocity = new Vector2(horM * movementSpeed * (gooed ? gooMovMultiplier : 1f), rb.velocity.y);
		else
			rb.velocity = new Vector2(horM * movementSpeed * waterMovementMultiplier * (gooed ? gooMovMultiplier : 1f), rb.velocity.y);
		}
	}
	
	private void Jump(){
		if(Input.GetKeyDown(jumpKey)){
			if((canJump || InWater) && !gooed){
				rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
				canDoubleJump = !canDoubleJump;
			}
		}
	}
	
	private void Flip(){
		if(facingRight && horM < 0f || !facingRight && horM > 0f){
			facingRight = !facingRight;
			Vector3 scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
			// 	// Vector3 tridentScale = trident.transform.localScale;
			// 	// tridentScale.x = -tridentScale.x;
		}
		// if(mousePos.x < transform.position.x && facingRight){
		// 	facingRight = !facingRight;
		// 	Vector3 scale = transform.localScale;
		// 	scale.x = -scale.x;
		// 	transform.localScale = scale;
		// }
		// if(mousePos.x > transform.position.x && !facingRight){
		// 	facingRight = !facingRight;
		// 	Vector3 scale = transform.localScale;
		// 	scale.x = -scale.x;
		// 	transform.localScale = scale;
		// }
	}
}