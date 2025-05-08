using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance { 
        get {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundFXManager>();
        } 
    }
    [SerializeField] private AudioSource soundFXObject;
    // Start is called before the first frame update


    public AudioSource PlaySoundOnLoop(AudioClip audioClip, Transform spawnTransform, float volume) {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.loop = true;

        audioSource.Play();

        return audioSource;
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume) 
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        try
        {
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
        catch (System.Exception e)
        {
            //Debug.Log("Missing SoundFile\r" + e.ToString());
            Debug.Log(e);
        }
    }
}
