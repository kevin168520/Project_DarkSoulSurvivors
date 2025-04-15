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
        b = !Enum.GetNames(typeof(enAudioDataBGM)).Contains(_audio.name);
    }

    void Update()
    {
        //判斷是否為BGM
        if (b)
        {
            //非BGM
            StartCoroutine(ieDestroyWhenAudioFinished());
        }
    }

    private IEnumerator ieDestroyWhenAudioFinished()
    {
        yield return new WaitForSeconds(_audio.clip.length + 10f);
        Destroy(gameObject); // 播放完畢後摧毀物件
    }
}
