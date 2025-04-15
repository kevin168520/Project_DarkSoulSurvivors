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
        //�P�_�O�_��BGM
        if (b)
        {
            //�DBGM
            StartCoroutine(ieDestroyWhenAudioFinished());
        }
    }

    private IEnumerator ieDestroyWhenAudioFinished()
    {
        yield return new WaitForSeconds(_audio.clip.length + 10f);
        Destroy(gameObject); // ���񧹲���R������
    }
}
