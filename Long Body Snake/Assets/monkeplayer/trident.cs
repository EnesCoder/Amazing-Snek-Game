using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trident : MonoBehaviour
{
    public bool playerAttacking;
    float attackTime;
    float attackSpeed;
    public float maxThrowForce = 40f;
    public float throwForce = 10f;
	private float startThrowForce;
	public float throwForceAddition = 10f;
	public float throwMultiplier = 10f;
    int flipped;
    float fuckUnity;
    bool unityyyy;
    public bool thrown;
    [SerializeField] Transform player;
    [SerializeField] Collider2D tridentCol;
    [SerializeField] Collider2D fishCol;
    public bool hasEnoughVel;
	
	public float retrievingSpeed = 10f;
	public KeyCode retrieveKey = KeyCode.T;
	public float minDistToStopRetrieve = 1f;
	private bool retrieving;
	
    void Start()
    {
        playerAttacking = false;
        attackTime = 0.4f;
        attackSpeed = 800f;
        fuckUnity = 0.1f;
        unityyyy = false;
        hasEnoughVel = false;
		startThrowForce = throwForce;
		retrieving = false;
    }
	
    void Update()
    {
		Debug.Log(hasEnoughVel);
		
        if(thrown){
			var rb = GetComponent<Rigidbody2D>();
			if(rb){
            if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) > 2f){
                hasEnoughVel = true;
            }
            if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) < 2f)
            {
                hasEnoughVel = false;   
            }
			}
        }
        if(!thrown){
            hasEnoughVel = false;
        }
        if(player.localScale.x < 0f){
            flipped = -1;
        }
        if(player.localScale.x > 0f){
            flipped = 1;
        }
        if(unityyyy){
            fuckUnity -= Time.deltaTime;
        }
        if(fuckUnity < 0){
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                -45f * flipped
            );
            unityyyy = false;
            fuckUnity = 0.1f;
        }
        if(Input.GetMouseButton(1) && thrown == false){
            throwForce += Time.deltaTime * throwForceAddition;
            if(throwForce >= maxThrowForce) throwForce = maxThrowForce;
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            -45f * flipped
            );
            transform.position = new Vector3(player.transform.position.x + 0.55f * flipped, player.transform.position.y + -0.26f, 1f);
        }
        if(Input.GetMouseButtonUp(1) && thrown == false){

            GetComponent<Animator>().enabled = false;

            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            1f * flipped);
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * throwForce * throwMultiplier * flipped);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 20);
            gameObject.GetComponent<Rigidbody2D>().mass = 0.5f;
            transform.parent = null;
            unityyyy = true;
            thrown = true;
			throwForce = startThrowForce;
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerAttacking = true;
            Debug.Log("Attacking");
        }
		
		if(Input.GetKeyDown(retrieveKey) && thrown && !retrieving){
			StartCoroutine(Retrieve());
		}
		
        if(playerAttacking){
            attackTime -= Time.deltaTime;

            player.GetComponent<Animator>().SetTrigger("Attack");
            player.GetComponent<Animator>().SetBool("Attacked", false);

            GetComponent<Animator>().SetTrigger("Attack");
            GetComponent<Animator>().SetBool("Attacked", false);
        }
        if (attackTime <= 0)
        {
            playerAttacking = false;
            attackTime = 0.4f;
            player.GetComponent<Animator>().SetBool("Attacked", true);
            GetComponent<Animator>().SetBool("Attacked", true);
          
        }
    }
	
    void OnTriggerStay2D(Collider2D other) {
		if(Input.GetKey(KeyCode.E) && !retrieving){
			if(other.tag == "pickupTrident"){
				GetPickedUp();
			}
		}
    }

	public void GetPickedUp(){
		GetComponent<Animator>().enabled = true;
        Debug.Log("Picked up trident");
        thrown = false;
        gameObject.transform.eulerAngles= new Vector3(
        gameObject.transform.eulerAngles.x,
        gameObject.transform.eulerAngles.y,
        0f
        );
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        transform.parent = player;
        transform.position = new Vector3(player.transform.position.x + 0.55f * flipped, player.transform.position.y, 1f);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        throwForce = 10f;
        thrown = false;
		retrieving = false;
        transform.localScale = new Vector3(
        0.5f,
        transform.localScale.y,
        transform.localScale.z
        );
	}
	
	public IEnumerator Retrieve(){
		var rb = GetComponent<Rigidbody2D>();
		if(rb)
			rb.velocity = Vector2.zero;
		while(Vector2.Distance(transform.position, player.position) > minDistToStopRetrieve){
			GetComponent<Collider2D>().enabled = false;
			retrieving = true;
			if(rb)
				rb.gravityScale = 0f;
			transform.position = Vector2.MoveTowards(transform.position, player.position, retrievingSpeed * Time.deltaTime);
			yield return null;
		}
		GetComponent<Collider2D>().enabled = true;
		GetPickedUp();
	}
}
