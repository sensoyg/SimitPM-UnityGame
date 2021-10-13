using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static AudioClip deathSound, powerSound, jumpSound; //Ses dosyalarımızın tanımlandığı değişken
    static AudioSource audioSrc;
    
    void Start()
    {
        //Ses dosyalarımzın dosya ismini scripte söylediğimiz satırlar. 
        deathSound = Resources.Load<AudioClip>("deathSound");                                                            
        jumpSound = Resources.Load<AudioClip>("jump1Sound"); 
        powerSound = Resources.Load<AudioClip>("powerSound");

        audioSrc = GetComponent<AudioSource>();
    }

    
    
    public static void PlaySound(string clip) //Ses çalmamızı sağlayan fonksiyon. Diğer classlardan erişilebilmesi için public, nesne oluşturmamak için static bir metod kullandık.
    {
        switch (clip) //Bu switch case yapısı istediğimiz şarkıyı çalmamızı sağlar 
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "death":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "power":
                audioSrc.PlayOneShot(powerSound);
                break;
        }
    }
}
