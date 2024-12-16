using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            player.TakeDamage(damage);

            //destroi o objeto
            Destroy(gameObject);
        }

        //destruir o projetil ao colidir com os limites da fase

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }
}
