using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tridentBar : MonoBehaviour
{
    [SerializeField] trident t;
    void Update()
    {
        transform.localScale = new Vector3(t.throwForce*0.5f / 40f, 0.1f, 1f);
    }
}
