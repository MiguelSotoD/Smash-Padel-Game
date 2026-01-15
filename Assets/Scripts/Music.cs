using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioSource music;

    public void onMusic(){
        music.Play();    
    }

    public void offMusic(){
        music.Stop();
    }
}
