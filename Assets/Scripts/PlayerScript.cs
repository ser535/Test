using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float jumpHeight = 10;
    [SerializeField] float trampolineJumpHeight = 17f; 
    [Space]
    [SerializeField] float startSpeed = 5f;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundMask;
    Rigidbody2D rb;
    bool isGround;
    bool Check = false;
    bool isJump = false;
    bool isFinish = false;
    bool isHoldInAir = false;
    bool isGroundOnce = false;
    float lastPosX;
    float speed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosX = transform.position.x;

        speed = startSpeed;
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        isGround = Physics2D.OverlapCircle(transform.position, radius, groundMask);
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) && isGround && !isJump && !isFinish)
        {
            Jump(jumpHeight);
            isGroundOnce = true;
        }
        if (Check)
        {
            if (transform.position.x - lastPosX <= 0.005f && !isFinish)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
        else if (transform.position.x - lastPosX > 0.05)
            Check = true;
        
        
        if (JumpInput() && !isGround)
        {
            isHoldInAir = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || isGroundOnce)
        {
            isHoldInAir = false;
            isGroundOnce = false;
        }
    }
    private void FixedUpdate()
    {
        lastPosX = transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("гг не будет");
            speed = 0;
            isFinish = true;
        }
        else if (collision.gameObject.CompareTag("Trampoline"))
        {
            Jump(trampolineJumpHeight);
        }
    }

    IEnumerator JumpCheck()
    {
        yield return new WaitForSeconds(0.1f);
        isJump = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Orb") && (JumpInput() || isHoldInAir))
        {
            Jump(jumpHeight);
        }
    }

    void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJump = true;
        StartCoroutine(JumpCheck());
    }

    bool JumpInput()
    {
        return (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space));
    }
}
