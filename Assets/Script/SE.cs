using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _PlayingEnd();
    }

    private void _PlayingEnd() {
        if (_audioSource.isPlaying) return;
        Destroy(gameObject);
    }
}
