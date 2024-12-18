using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 0.7f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    // Player olhando para a direita
    private bool playerFacingRight = true;

    //Variuavel contadora 
    private int punchCount;

    //Tempo de ataque 
    private float timeCross = 1.3f;

    private bool comboControl;

    //indique se esta vivo ou morto
    public bool isDead;

    //propriedades para a UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    //SFX Player
    private AudioSource playerAudioSource;

    public AudioClip jabSound;
    //public AudioClip crossSound;
    //public AudioClip deathSound;


    void Start()
    {
        // Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

        //iniciar a vida do player
        currentHealth = maxHealth;

        //inicia o componente AudioSource do player
        playerAudioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        PlayerMove();
        UpdateAnimator();


        if (Input.GetKeyDown(KeyCode.X))
        {


            //Iniciar o temporizador


            if (punchCount < 2)
            {

                PlayerJab();
                punchCount++;

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }

            }

            else if (punchCount >= 2)
            {

                PlayerCross();
                punchCount = 0;
            }

            //Parando o temporizador 
            StopCoroutine(CrossController());

        }

        if (isDead && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TelaInicial");

        }

    }

    // Fixed Update geralmente é utilizada para implementação de física no jogo
    // Por ter uma execução padronizada em diferentes dispositivos
    private void FixedUpdate()
    {
        // Verificar se o Player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        // Pega a entrada do jogador, e cria um Vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se o player vai para a ESQUERDA e está olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        // Se o player vai para a DIREITA e está olhando para ESQUERDA
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Definir o valor do parâmetro do animator, igual à propriedade isWalking
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip()
    {
        // Vai girar o sprite do player em 180º no eixo Y

        // Inverter o valor da variável playerfacingRight
        playerFacingRight = !playerFacingRight;

        // Girar o sprite do player em 180º no eixo Y
        // X, Y, Z
        transform.Rotate(0, 180, 0);
    }


    void PlayerJab()
    {
        //Acessa a animação do JAb
        //Ativa o gatilho de ataque Jab
        playerAnimator.SetTrigger("isJab");

        //Definir o SFX à ser reproduzido
        playerAudioSource.clip = jabSound;

        //Executar o SFX
        playerAudioSource.Play();

    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");

        //Definir o SFX à ser reproduzido
        playerAudioSource.clip = jabSound;

        //Executar o SFX
        playerAudioSource.Play();
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        punchCount = 0;

        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);



        }
        if (currentHealth <= 0)
        {
            isDead = true;

            SceneManager.LoadScene("GameOver");
            //gameObject.SetActive(false);
        }

    }
}