using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Classe responsavel por controlar qualquer tipo de audio
    private AudioSource audioSource;

    //AudioClip é o arquivo de audio que será executado
    public AudioClip levelMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Ao iniciar a MusicController, inicia a música da fases
        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        //Define o som que irá ser tocado
        audioSource.clip = music;

        audioSource.Play();



    }   
}
