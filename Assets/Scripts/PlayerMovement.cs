using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speedPlayer = 250;
    [SerializeField] private float jumpForce = 300;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRender;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping;
    private bool isGrounded = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        // OvelLapArea definie un Box Area dans une zone definie sur base de la position de 2 objets et renvoi en boolean en cas de contact
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // pour une raison inconnue la fonction Input.GetKeyDown ne fonctionne pas bien dans FixedUpdate
            isJumping = true;
            Debug.Log("Jump");
        }
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime;

        MovePlayer(horizontalMovement);
        
        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        // Si le personnage se deplace vers la gauche, rb.velocity.x sera negative
        // Speed ne peut pas être negative donc il faut rendre la valeur absolue.
        animator.SetFloat("Speed", characterVelocity);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        // rb.velocity.y == valeur de velocité par defaut de l'axe y du rigidBody rb
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRender.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRender.flipX = true;
        }
    }
}
