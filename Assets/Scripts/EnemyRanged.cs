using UnityEngine;

public class EnemyRanged : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    //move
    private bool facingRight;
    private bool previousDirectionRight;

    private bool isDead;

    //virar para player
    private Transform target;

    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private float verticalForce, horizontalForce;

    private bool isWalking = false;

    private float walkTimer;

    public int maxHealth;
    public int currentHealth;

    private float staggerTime = 0.5f;
    private bool isTakingDamage = false;
    private float damageTimer;

    private float attackRate = 1f;
    private float nextAttack;

    public Sprite enemyImage;

    //variavel para armazenar o projetil
    public GameObject projectile;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = FindAnyObjectByType<PlayerController>().transform;

        currentHealth = maxHealth;
        currentSpeed = enemySpeed;

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

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = enemySpeed;
    }

    public void TakeDamage(int damage)
    {
        isTakingDamage = true;

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        FindFirstObjectByType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyImage);

        if (currentHealth <= 0)
        {
            isDead = true;

            //ZeroSpeed() ;
            //zera a velocidade do inimigo
            rb.linearVelocity = Vector3.zero;

            animator.SetTrigger("Dead");
        }
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 targetDistance = target.position - transform.position;

            if (walkTimer >= Random.Range(2.5f, 3.5f))
            {
                verticalForce = targetDistance.y / Mathf.Abs(targetDistance.y);
                horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

                walkTimer = 0;

            }

            if (Mathf.Abs(targetDistance.x) < 1)
            {
                horizontalForce = 0;
            }

            if (Mathf.Abs(targetDistance.y) < 0.05f)
            {
                verticalForce = 0;
            }

            if (!isTakingDamage)
            {
                rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);
            }

            //attack

            if (Mathf.Abs(targetDistance.x) < 1.3f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
            {

                animator.SetTrigger("Attack");
                 ZeroSpeed();

                nextAttack = Time.time + attackRate;
            }

        }
    }

    public void Shoot()
    {
        //definir a posição de spawn do projetil

        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 0.2f);

        //spawnar o projetil na posição definida
        GameObject shotObject = Instantiate(projectile, spawnPosition, Quaternion.identity);

        //ativar o projetil

        shotObject.SetActive(true);

        var shotPhysics = shotObject.GetComponent<Rigidbody2D>();

        if (facingRight)
        {
            //aplica for no projetil para ele se descolar para a direita
            shotPhysics.AddForceX(80f);
        }
        else
        {
            //aplica for no projetil para ele se descolar para a esquerda
            shotPhysics.AddForceX(-80f);
        }
    }
}
