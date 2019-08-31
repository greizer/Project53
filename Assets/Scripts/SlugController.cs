using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SlugController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;

    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning){
            rb.velocity = new Vector2(-0.2f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        Debug.Log(col.collider.name);
        if (col.collider.gameObject.name is "player")
            {
                animator.SetBool("isRunning",true);
                isRunning = true;
                col.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2( 10*(col.collider.gameObject.GetComponent<Transform>().position.x - col.otherCollider.gameObject.GetComponent<Transform>().position.x),
                    10*(col.collider.gameObject.GetComponent<Transform>().position.y - col.otherCollider.gameObject.GetComponent<Transform>().position.y)),
                    ForceMode2D.Impulse);
            }
    }
}
