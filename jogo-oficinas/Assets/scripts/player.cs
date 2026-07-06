using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // IMPORTANTE: Nova biblioteca de Input

public class PlayerController2D : MonoBehaviour
{
    [Header("Movimentação Horizontal")]
    public float maxSpeed = 8f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    private float moveInput;
    private bool facingRight = true;

    [Header("Mecânicas de Pulo")]
    public float jumpForce = 12f;
    public float fallMultiplier = 2.5f;      
    public float lowJumpMultiplier = 2f;    
    
    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;        
    private float coyoteTimeCounter;

    [Header("Verificação de Chão")]
    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;
    private bool isGrounded;

    // Componentes
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // NOVO INPUT SYSTEM: Lê as setas ou A/D através do teclado atual
        if (Keyboard.current != null)
        {
            moveInput = 0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput = 1f;
        }

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // NOVO INPUT SYSTEM: Detecta o clique inicial do Espaço
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f; 
        }

        // Modificadores de Gravidade adaptados para checar se o Espaço continua segurado
        bool isHoldingJump = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;

        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !isHoldingJump)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
            anim.SetBool("isGrounded", isGrounded);
        }

        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    void FixedUpdate()
    {
        float targetSpeed = moveInput * maxSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = speedDif * accelRate;
        
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
    
    // Essa função é chamada automaticamente quando o jogador encosta em um objeto com "Is Trigger" marcado (Coletável)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            Debug.Log("Pegou um coletável!");
            
            // Aqui você pode aumentar a pontuação do jogador no futuro
            
            Destroy(collision.gameObject); // Destrói a moeda da cena
        }
    }

    // Essa função é chamada quando o jogador colide fisicamente com algo (Inimigo)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("O jogador Morreu!");
            
            Morrer();
        }
    }

    // Função que lida com a morte do personagem
    void Morrer()
    {
        // Por enquanto, vamos apenas reiniciar a fase atual
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}