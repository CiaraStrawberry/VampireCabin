using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioSource soundEffects;


    // all of the possible sounds that the game can run

    public AudioClip shotgunSound;

    public AudioClip clickSound;

    public AudioClip UISOUND;

    public AudioClip pain;

    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer[] instance = (MusicPlayer[])FindObjectsOfType(typeof(MusicPlayer)) ;
        if(instance.Length > 1)
        {
            Destroy(this.gameObject);
            return;

        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    // any time a sound can be played, it is passed through the static instance into one of these classes.

   public void playShotgun ()
    {
        soundEffects.PlayOneShot(shotgunSound);
    }
    public void playClick ()
    {
        soundEffects.PlayOneShot(clickSound);
    }

    public void playUISound ()
    {
        soundEffects.PlayOneShot(UISOUND);
    }

    public void playPain ()
    {
        soundEffects.PlayOneShot(pain);
    }

    public void playDeath ()
    {
        soundEffects.PlayOneShot(death);
    }
}
