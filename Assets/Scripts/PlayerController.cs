using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // inputit
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        // liike sivusuunnassa
        movement.x = horizontalMovement * moveSpeed;
        // hahmon kääntäminen
        if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // kiipeäminen
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
        // hyppääminen
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // animaatio
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        animator.SetBool("climbing", isClimbing);
    }
    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dragon"))
        {
            Win();
        }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            Lose();
        }
    }
    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Win()
    {
        Debug.Log("You win!");
        RestartScene();
    }
    void Lose()
    {
        Debug.Log("You lose!");
        RestartScene();
    }
}
