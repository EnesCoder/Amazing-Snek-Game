using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] PlayerHealthManager phm;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player"){
            phm.playerHealth += 3f;
            Destroy(gameObject);
        }
    }
}
