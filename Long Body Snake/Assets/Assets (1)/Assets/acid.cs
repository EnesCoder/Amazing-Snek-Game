using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acid : MonoBehaviour
{
    [SerializeField] PlayerHealthManager phm;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "player"){
            phm.playerHealth = 0f;
        }
    }
}
