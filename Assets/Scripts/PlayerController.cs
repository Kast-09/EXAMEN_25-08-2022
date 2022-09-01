using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 10, fuerzaSalto, jumpForce = 5;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    bool puedeSaltar = true;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_SALTAR = 3;
    const int ANIMATION_ATACAR = 4;
    Vector3 lastCheckPointPosition;

    int saltar = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))//si presiono flecha derecha va a la derecha
        {
            rb.velocity = new Vector2(velocity * 2, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.RightArrow))//si presiono flecha derecha va a la derecha
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(-velocity * 2, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && puedeSaltar)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ChangeAnimation(ANIMATION_SALTAR);
            if (saltar > 2)
            {
                saltar++;
                return;
            }
            puedeSaltar = false;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Puede Saltar");
        puedeSaltar = true;
        saltar = 0;
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Estas Muerto");
        }
        if (collision.gameObject.tag == "DarkHole")
        {
            if (lastCheckPointPosition != null)
            {
                transform.position = lastCheckPointPosition;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
