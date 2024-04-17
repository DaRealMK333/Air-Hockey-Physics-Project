using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagementScript : MonoBehaviour
{

    public AudioClip AudioPuckCollision, AudioGoal,AudioAiGoal ,AudioLostGame ,AudioWonGame;

    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayPuckCollisionAudio()
    {
        audioSource.PlayOneShot(AudioPuckCollision);
    }

    public void PlayPlayerAudioGoal()
    {
        audioSource.PlayOneShot(AudioGoal);  
    }

    public void PlayAiAudioGoal()
    {
        audioSource.PlayOneShot(AudioAiGoal);
    }
    
    public void PlayAudioWonGame()
    {
        audioSource.PlayOneShot(AudioWonGame);
    }
    public void PlayAudioLostGame()
    {
        audioSource.PlayOneShot(AudioLostGame);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
