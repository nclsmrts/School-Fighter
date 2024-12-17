using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Classe responsavel por controlar qualquer tipo de audio
    private AudioSource audioSource;

    //AudioClip � o arquivo de audio que ser� executado
    public AudioClip levelMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Ao iniciar a MusicController, inicia a m�sica da fases
        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        //Define o som que ir� ser tocado
        audioSource.clip = music;

        audioSource.Play();



    }   
}
