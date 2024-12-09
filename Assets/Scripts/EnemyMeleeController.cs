using System;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    //indique se esta vivo
    public bool isDead;

    //Variaveis para controlar o lado que o inimigo esta virado
    private bool facingRight;
    public bool previousDirectionRight;

    //variavel para armazenar posição player
    private Transform target;

    //vaviavel para movimentação do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;
    private bool isWalking;
    private float horizontalForce;
    private float verticalForce;
    private float walkTimer;

    //variaveis ára mecânica de ataque
    private float attackRate = 1f;
    private float nextAttack;

    //variaveis para mecanica de dano
    public int maxHealth;
    public int currentHealth;
    public Sprite enemyImage;
    public float staggerTime = 0.5f;
    private float damageTimer;
    public bool isTakingDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //buscar o player e armazenar a posicao
        target = FindAnyObjectByType<PlayerController>().transform;

        //inicializar velocidade do inimigo 
        currentSpeed = enemySpeed;

        //inicializar vida do inimigo
        currentHealth = maxHealth;
    }

    void Update()
    {
        //verificar se o player esta para a direita ou esquerda e determinar o lado que o inimigo ficara virado

        if (target.position.x < transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        //se facingRigth foi true, virar o inimigo em 180 mo eixo Y
        //se não virar o inimigo para a esquerda

        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        //iniciar o timer de caminhar do inimigo
        walkTimer += Time.deltaTime;

        //Gerenciar a animação do inimigo

        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        //gerenciar  o tempo de stegger
        if (isTakingDamage && !isDead)
        {
            damageTimer += Time.deltaTime;

            ZeroSpeed();

            if (damageTimer >= staggerTime)
            {
                isTakingDamage = false;
                damageTimer = 0;
                ResetSpeed();
            }
        }
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            //movimentação

            //variavel para armazenar a distancia entre o inimigo e o player

            Vector3 targetDistance = target.position - transform.position;

            horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            //entre 1 e 2 segundos será feita uma definição de direção vertical
            if (walkTimer >= UnityEngine.Random.Range(1f, 2f))
            {
                verticalForce = UnityEngine.Random.Range(-1, 2);

                walkTimer = 0;
            }

            //caso esteja perto do player, parar a movimentação

            if (Mathf.Abs(targetDistance.x) < 0.2f)
            {
                horizontalForce = 0;
            }

            //aplica a velocidade no inimigo fazendo movimentar

            rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);

            //Attack
            //se estiver perto do player e o timer do jogo for maior que o valor de nextAttack 

            if (MathF.Abs(targetDistance.x) < 0.2f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
            {
                //executa o ataque do inimigo 
                animator.SetTrigger("Attack");

                ZeroSpeed();

                //pega o tempo atual e soma o attackrate, para definir a partir de quando o inimigo poderá atacar novamente
                nextAttack = Time.time + attackRate;

            }
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;
            currentHealth -= damage;

            animator.SetTrigger("HitDamage");

            //atualiza a UI do inimigo
            FindFirstObjectByType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyImage);

            if (currentHealth <= 0)
            {
                isDead = true;
                ZeroSpeed();

                animator.SetTrigger("Dead");
            }
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = enemySpeed;

    }
    public void Disableenemy()
    {
        gameObject.SetActive(false);
    }
}
