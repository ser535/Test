using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    [SerializeField] bool isGravityNegative;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var gravity = Mathf.Abs(collision.GetComponent<Rigidbody2D>().gravityScale);
            collision.GetComponent<Rigidbody2D>().gravityScale = isGravityNegative ? gravity * -1 : gravity;
            Debug.Log("gravity");
        }
    }
}






          

