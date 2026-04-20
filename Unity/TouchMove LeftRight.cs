using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Touch touch;
    private float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
         
            if (touch.phase == TouchPhase.Moved)
            {
                rb.velocity = (touch.deltaPosition.x > 0)
                    ? Vector2.right * moveSpeed
                    : -Vector2.right * moveSpeed;
            }
        
        }
    }
}
