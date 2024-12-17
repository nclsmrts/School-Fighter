using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    public string[] fases;
    [SerializeField] private int faseAtual = 0;

    void Start()
    {

    }


    void Update()
    {
        //telainicial
        if (Input.GetKeyDown(KeyCode.Return) && faseAtual >= 1)
        {
            faseAtual--;
            StartCoroutine(CarregarFase(fases[1]));
        }
        //fase1
        if (Input.GetKeyDown(KeyCode.Return) && faseAtual < 1)
        {
            faseAtual++;
            StartCoroutine(CarregarFase(fases[0]));
        }



    }

    // Corrotina - Coroutine

    IEnumerator CarregarFase(string nomeFase)
    {
        //Iniciar animação
        transition.SetTrigger("Start");

        //esperar o tempo de animação
        yield return new WaitForSeconds(transitionTime);

        //carregar cena
        SceneManager.LoadScene(nomeFase);
    }
}
