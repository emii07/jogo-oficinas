using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // IMPORTANTE: Mantém a compatibilidade com o novo sistema

[RequireComponent(typeof(Rigidbody2D))]
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

        // CONFIGURAÇÕES DE FÍSICA CONTRA TREMORES E ROTAÇÕES INDESEJADAS
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Deixa o movimento suave na tela
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;   // Impede o player de cair deitado
    }

    void Update()
    {
        // SISTEMA DE INPUT INTELIGENTE (Tenta ler o Novo Input, se der falha ou for nulo, usa o Clássico)
        moveInput = 0f;
        bool jumpPressed = false;
        bool isHoldingJump = false;

        try
        {
            if (Keyboard.current != null)
            {
                // Leitura do Novo Input System
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput = -1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput = 1f;

                jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
                isHoldingJump = Keyboard.current.spaceKey.isPressed;
            }
            else
            {
                // Fallback para o Input Clássico caso o Teclado do Novo Sistema não esteja ativo
                moveInput = Input.GetAxisRaw("Horizontal");
                jumpPressed = Input.GetKeyDown(KeyCode.Space);
                isHoldingJump = Input.GetKey(KeyCode.Space);
            }
        }
        catch
        {
            // Fallback de segurança absoluto
            moveInput = Input.GetAxisRaw("Horizontal");
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
            isHoldingJump = Input.GetKey(KeyCode.Space);
        }

        // Verificação de chão
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Mecânica de Pulo
        if (jumpPressed && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f; 
        }

        // Modificadores de Gravidade (Pulo dinâmico)
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

        // Animações
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
            anim.SetBool("isGrounded", isGrounded);
        }

        // Inversão do Sprite (Flip)
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    void FixedUpdate()
    {
        // MOVIMENTAÇÃO POR VELOCIDADE DE ALTA PRECISÃO (Evita travar nas paredes/platfomas)
        float targetSpeed = moveInput * maxSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        
        float movement = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, accelRate * Time.fixedDeltaTime);
        
        rb.linearVelocity = new Vector2(movement, rb.linearVelocity.y);
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
    
    // Coletar itens (Triggers)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coletavel"))
        {
            Debug.Log("Pegou um coletável!");
            Destroy(collision.gameObject); 
        }
    }

    // Colisão física com inimigos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("O jogador Morreu!");
            Morrer();
        }
    }

    // Reiniciar a fase ao morrer
    void Morrer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}