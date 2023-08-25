using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Sprite[] healthBar;
    [SerializeField] PlayerHealthManager phm;
    public SpriteRenderer sr;


    // Update is called once per frame
    void Update()
    {
        sr.sprite = healthBar[(int)phm.playerHealth];
    }
}
