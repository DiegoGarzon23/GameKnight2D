using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxVel = 0;
    bool voltear = true;
    SpriteRenderer knightRenderer;
    Animator knightAnimator;
    bool puedeMover = true;

    //Saltar
    bool enSuelo = false;
    float revisarRadioSuelo = 0.2f;
    public LayerMask capaSuelo;
    public Transform revisarSuelo;
    public float powerSalto;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knightRenderer = GetComponent<SpriteRenderer>();
        knightAnimator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            knightAnimator.SetBool("EstaFloor", false);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
          
            rb.AddForce(new Vector2(0, powerSalto), ForceMode2D.Impulse);
            enSuelo = false;

        }
        enSuelo = Physics2D.OverlapCircle (revisarSuelo.position, revisarRadioSuelo, capaSuelo);
        knightAnimator.SetBool("EstaFloor", enSuelo);
        float mh = Input.GetAxis("Horizontal");
        if (puedeMover)
        {
            if (mh > 0 && !voltear)
            {
                Voltealo();
            }
            else if (mh < 0 && voltear)
            {
                Voltealo();
            }
            rb.velocity = new Vector2(mh * maxVel, rb.velocity.y);


            //Hacer que corra nuestro personaje
            knightAnimator.SetFloat("VelMov", Mathf.Abs(mh));

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            knightAnimator.SetFloat("VelMov", 0);
        }
    }
    public void Voltealo()
    {
        voltear = !voltear;
        knightRenderer.flipX = !knightRenderer.flipX;
    }
    public void togglePuedeMover()
    {
        puedeMover = !puedeMover;
    }
}
