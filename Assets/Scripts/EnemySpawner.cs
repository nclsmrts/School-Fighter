using Assets.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    public int numberOfEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSextion;
    void Update()
    {
        //casoa tinja o numero maximo de inimigos spawnados
        if (currentEnemies >= numberOfEnemies)
        {
            //contar a quantidade de inimigos ativos na cena 
            int enemies = FindAnyObjectByType<EnemyMeleeController>(FindObjectsSortMode.None).length;

            if (enemies <= 0)
            {
                LevelManager.ChangeSection(nextSextion);

                this.gameObject.SetActive(false);
            }

            
        }
    }

    void SpawnEnemy()
    {
        //posição de spawn do inimigo
        Vector2 spawnPosition;

        //limites Y
        //-2,60
        //-3,2

        spawnPosition.y = Random.Range(-2.60f, -3.2f);

        //posição x max (direita) do confiner da camera + 1 de distância
        //pegar o RightBound (limite direito) da Section (confiner) como base

        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        //Define o x do spawnOi=osition, igual ao ponto da direita do confiner
        spawnPosition.x = rightSectionBound;

        //pega um inimigo aleatorio da lista de inimigos
        //quaternion é uma classe ultilizada para trabalhar com rotações
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        //Incrementa o contador de inimigos do spawner

        currentEnemies++;

        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            //destiva o colisor para ativar apenas uma vez
            //Desabilita o collider,mas o objeto Spawner continua ativo
            this.GetComponent<BoxCollider2D>().enabled = false;

            //invoca a primeira vez a função SpawnEnemy
            SpawnEnemy();
        }
    }
}
