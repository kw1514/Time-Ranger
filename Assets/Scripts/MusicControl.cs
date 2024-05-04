using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private AudioSource _song;
    [SerializeField]
    private AudioClip _citysong;
    [SerializeField]
    AudioClip _forestsong;

    private Travel travel;
    private bool playSong = true;
    void Start()
    {
        _song = GetComponent<AudioSource>();
        _song.clip = _citysong;
        travel = FindObjectOfType<Travel>();
    }

    void Update()
    {
        //basic solution, if playsong is true and is currently the cyber city, it plays the cyber city song, then turns off.
        if (playSong && travel.getCyberCity())
        {
            _song.clip = _citysong;
            _song.Play();
            playSong = !playSong;
        }
        //same as above but the booleans are flipped.
        else if (!playSong && !travel.getCyberCity())
        {
            _song.clip = _forestsong;
            _song.Play();
            playSong = !playSong;
        }
       
    }
}
