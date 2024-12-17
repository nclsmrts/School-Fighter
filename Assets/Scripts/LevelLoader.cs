using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CarregarFase("fase1"));
        }   
    }

    // Corrotina - Coroutine

    public IEnumerator CarregarFase(string nomeFase)
    {
        //Iniciar anima��o
        transition.SetTrigger("Start");

        //esperar o tempo de anima��o
        yield return new WaitForSeconds(transitionTime);

        //carregar cena
        SceneManager.LoadScene(nomeFase);
    }
}
