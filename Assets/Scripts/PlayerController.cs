using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    // input-muuttujat
    private float horizontalMovement;
    // liikkumismuuttujat
    private float moveSpeed = 5f;
    private Vector2 movement = new Vector2();
    // hyppimismuuttujat
    private float jumpForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        movement.x = horizontalMovement * moveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
}
