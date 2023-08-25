using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
<<<<<<< HEAD
	public float waterGravity = 0.5f;
	public List<Rigidbody2D> bodiesInside = new List<Rigidbody2D>();
	public List<float> originalGravities = new List<float>();
	
    void Update()
    {
        foreach(Rigidbody2D body in bodiesInside)
			body.gravityScale = waterGravity;
=======


    void Update()
    {
        
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().InWater = true;
        }
<<<<<<< HEAD
		
		var rb = other.gameObject.GetComponent<Rigidbody2D>();
		if(rb != null && !other.CompareTag("Fish") && !bodiesInside.Contains(rb)){
			originalGravities.Add(rb.gravityScale);
			bodiesInside.Add(rb);
			rb.gravityScale = originalGravities[bodiesInside.IndexOf(rb)];
		}
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().InWater = false;
        }
<<<<<<< HEAD
		
		var rb = other.gameObject.GetComponent<Rigidbody2D>();
		if(rb != null && !other.CompareTag("Fish") && bodiesInside.Contains(rb)){
			var rbIndex = bodiesInside.IndexOf(rb);
			bodiesInside[rbIndex].gravityScale = originalGravities[rbIndex];
			bodiesInside.Remove(rb);
			originalGravities.Remove(originalGravities[rbIndex]);
		}
=======
>>>>>>> b059c604061ea994ec69b7a0736e6b2cac10c30d
    }
}
