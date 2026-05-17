using UnityEngine;

public class gramaphone : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;
    public LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.gramaphoneAllowed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        } else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
