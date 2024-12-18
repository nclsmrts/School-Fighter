using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public float transitionTime = 1f;

    private AudioSource audioSource;
    public AudioClip AudioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.clip = AudioClip;
            audioSource.Play();


        }

    }
}
