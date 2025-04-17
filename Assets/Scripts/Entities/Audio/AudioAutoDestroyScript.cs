using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioAutoDestroyScript : MonoBehaviour
{
    private AudioSource _audio;
    private bool b;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        b = !Enum.GetNames(typeof(enAudioBgmData)).Contains(_audio.name);
    }

    void Update()
    {
        //判斷是否為BGM
        if (b && !_audio.isPlaying)
        {
            Destroy(gameObject); // 播放完畢後摧毀物件

            //非BGM
            //StartCoroutine(ieDestroyWhenAudioFinished());
        }
    }

    private IEnumerator ieDestroyWhenAudioFinished()
    {
        yield return new WaitForSeconds(_audio.clip.length);
        Destroy(gameObject); // 播放完畢後摧毀物件
    }
}
