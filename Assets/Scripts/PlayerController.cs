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
    private float verticalMovement;
    // liikkumismuuttujat
    private float moveSpeed = 5f;
    private Vector2 movement = new Vector2();
    // hyppimismuuttujat
    private float jumpForce = 10f;
    private bool grounded;
    // kiipeämismuuttujat
    private bool canClimb;
    private bool isClimbing;
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
        verticalMovement = Input.GetAxisRaw("Vertical");
        movement.x = horizontalMovement * moveSpeed;
        if (canClimb && verticalMovement != 0)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing)
        {
            movement.y = verticalMovement * moveSpeed;
            body.isKinematic = true;
        }
        else
        {
            movement.y = 0f;
            body.isKinematic = false;
        }
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
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }
}
