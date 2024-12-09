using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ao colidir, salva na variavel enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        //ao colidir, salva na variavel player, o player que foi colidido
        PlayerController player = collision.GetComponent<PlayerController>();

        //se a colisão foi com um inimigo
        if (enemy != null)
        {
            //inimigo recebe dano 

            enemy.TakeDamage(damage);
        }

        //se a colisão foi com o player
        if(player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
