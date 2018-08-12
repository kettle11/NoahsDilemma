using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour {

    static AudioSource audioSource;
    public AudioClip pickup;
    public AudioClip drop;

    public AudioClip pickupGrid;
    public AudioClip dropGrid;

    public AudioClip rotate;
    public AudioClip error;
    public AudioClip complete;
    public AudioClip completeGame;

    public AudioClip clapping;

    public static SoundSource instance;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        instance = this;
	}

    public static void PlayPickup()
    {
        audioSource.PlayOneShot(instance.pickup);
    }

    public static void PlayDrop()
    {
        audioSource.PlayOneShot(instance.drop);
    }

    public static void PlayPickupGrid()
    {
        audioSource.PlayOneShot(instance.pickupGrid);
    }

    public static void PlayDropGrid()
    {
        audioSource.PlayOneShot(instance.dropGrid);
    }

    public static void PlayRotate()
    {
        audioSource.PlayOneShot(instance.rotate);
    }

    public static void PlayError()
    {
        audioSource.PlayOneShot(instance.error);
    }

    public static void PlayCompleteLevel()
    {
        audioSource.PlayOneShot(instance.complete);
        audioSource.PlayOneShot(instance.clapping);
    }

    public static void PlayCompleteGame()
    {
        audioSource.PlayOneShot(instance.completeGame);

    }
}
