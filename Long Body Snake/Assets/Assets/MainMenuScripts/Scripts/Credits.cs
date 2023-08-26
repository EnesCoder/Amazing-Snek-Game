using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 5f;

    private bool isScrolling = true;


    public void Update()
    {
        if (isScrolling)
        {
            // Move the credits content upwards
            gameObject.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }

        if (transform.localPosition.y >= 1416)
        {
            transform.position = new Vector3(transform.position.x, -185f, 0);
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

    }

    public void StopScrolling()
    {
        isScrolling = false;
    }

}
