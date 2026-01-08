using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoSaltar;
    public AudioClip sonidoRaqueta;
    public AudioClip sonidoPunto;
    
    public void PlaySaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);
    }
    public void PlayRaqueta()
    {
        audioSource.PlayOneShot(sonidoRaqueta);
    }
    public void PlayPunto()
    {
        audioSource.PlayOneShot(sonidoPunto);
    }
}
