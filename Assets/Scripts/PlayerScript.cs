using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int speed = 5;
    Rigidbody2D rb;
    [SerializeField] float jumpHeight = 10;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float radius;
    bool isGround;
    bool Check = false;
    bool isJump = false;
    float lastPosX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosX = transform.position.x;

    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        isGround = Physics2D.OverlapCircle(transform.position, radius, groundMask);
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && isGround && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJump = true;
            StartCoroutine(JumpCheck());
        }
        if (Check)
        {
            if (transform.position.x - lastPosX <= 0.005f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
        else if (transform.position.x - lastPosX > 0.05)
            Check = true;

    }
    private void FixedUpdate()
    {
        lastPosX = transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("гг не будет");
        if (collision.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    IEnumerator JumpCheck()
    {
        yield return new WaitForSeconds(0.1f);
        isJump = false;
    }
}
