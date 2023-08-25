using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] PlayerHealthManager phm;

    public Sprite[] sprites;


    private void Start()
    {
        int r = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[r]; 
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player"){
            phm.playerHealth += 3f;
            Destroy(gameObject);
        }
    }
}
