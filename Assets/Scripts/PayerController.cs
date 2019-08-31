using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PayerController : MonoBehaviour
{
    public bool isGrounded = false;
    public float jumpForce = 3.0f;
    public float speed = 20.0f;
    public Animator animator;
    
    private Rigidbody2D rb;
    private bool facingRight = true;

    void Start () {
        rb = GetComponent <Rigidbody2D> ();
    }

    private void Update() {
         if (Input.GetButtonDown ("Jump") && isGrounded) {
            isGrounded = false;
            animator.SetTrigger("isJumping");
            animator.SetBool("isGrounded",false);
            rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() {
        
        float horizonatal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //float acceleration = isGrounded ? walkAcceleration : airAcceleration;
        //float deceleration = groundDeceleration;
       
        // if (horizonatal != 0)
        // {
        //     velocity.x = Mathf.MoveTowards(velocity.x, , acceleration * Time.fixedDeltaTime);
        // }
        // else
        // {
        //     velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        // }
        // transform.Translate(velocity * Time.fixedDeltaTime);

        Vector2 move = new Vector2(speed * horizonatal, rb.velocity.y);
        if (speed * horizonatal != 0){
            animator.SetBool("isMoving",true);
        }else{
            animator.SetBool("isMoving",false);
        }
        
        if (speed * horizonatal > 0 && !facingRight  || speed * horizonatal < 0 && facingRight){
            Flip();
        }
        rb.velocity = move;
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.otherCollider is BoxCollider2D)
            {
                isGrounded = true;
                animator.SetBool("isGrounded",true);
                //Debug.Log(col.collider);
            }
        if (col.collider.gameObject.tag is "enemy"){
            animator.SetTrigger("isHit");
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        //animator.SetBool("isGrounded",false);
        //isGrounded = false;
    }
}
