using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    private AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ao colidir, salva na variavel enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();
        EnemyRanged enemyRanged = collision.GetComponent<EnemyRanged>();


        //ao colidir, salva na variavel player, o player que foi colidido
        PlayerController player = collision.GetComponent<PlayerController>();

        //se a colisão foi com um inimigo
        if (enemy != null)
        {
            //inimigo recebe dano 
            enemy.TakeDamage(damage);

            audioSource.clip = hitSound;

            audioSource.Play();
        }
        if (enemyRanged != null)
        {
            //inimigo recebe dano 
            enemyRanged.TakeDamage(damage);

            audioSource.clip = hitSound;

            audioSource.Play();
        }

        //se a colisão foi com o player
        if (player != null)
        {
            player.TakeDamage(damage);

            audioSource.clip = hitSound;

            audioSource.Play();
        }
    }
}
