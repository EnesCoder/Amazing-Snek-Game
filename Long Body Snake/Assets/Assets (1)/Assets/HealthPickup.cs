using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] PlayerHealthManager phm;
    public Sprite[] food;

    private void Start()
    {
        int r = Random.Range(0, food.Length);
        GetComponent<SpriteRenderer>().sprite = food[r];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player"){
            phm.playerHealth += 3f;
            phm.respawnPos = transform.position;
            Destroy(gameObject);
        }
    }
}
