using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    public GameObject enemyUI;
    public Slider enemyHealthBar;
    public Image enemyImage;

    //Objeto para armazenar os dados do player
    private PlayerController player;

    //timers e controles do enemyUI
    public float enemyUITime = 4f;
    private float enemyTimer;

    void Start()
    {
        //obtem od dados do player
        player = FindFirstObjectByType<PlayerController>();

        //definir o valor maximo da barra de vida igual a maximo a vida do player
        playerHealthBar.maxValue = player.maxHealth;

        //iniciar a barra de vida cheia
        playerHealthBar.value = playerHealthBar.maxValue;

        //definir a imagem do player
        playerImage.sprite = player.playerImage; 

    }

    void Update()
    {
        // inicia o contador
        enemyTimer += Time.deltaTime;

        // se o tempo limite for atingido, oculta a ui e reseta o timer
        if (enemyTimer >= enemyUITime)
        {
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }


    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, Sprite image)
    {
        //atualiza os dados do inimigo de acordo com o inimigo atacado
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
        enemyImage.sprite = image;

        //zera o timer par começar a contar 4s
        enemyTimer = 0;

        //habilita a enemyUI, deixando-a visivel
        enemyUI.SetActive(true);

    }
}
