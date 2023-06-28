using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // komponentit
    private Rigidbody2D body;
    private Animator animator;
    // input-muuttujat
    private float horizontalMovement;
    // liikkumismuuttujat
    private float moveSpeed = 5f;
    private Vector2 movement = new Vector2();
    // hyppimismuuttujat
    private float jumpForce = 10f;
    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        movement.x = horizontalMovement * moveSpeed;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }
    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
}
