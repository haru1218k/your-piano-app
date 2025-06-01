using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField]private AudioSource normalMetronome;
    [SerializeField]private AudioClip normalMetronomeClip;
    private int _bpm;
    private bool isMetronomeRunning = false;
    private Coroutine _metronomeCoroutine;

    public void Initialize(int bpm)
    {
        _bpm = bpm;
    }
    public void PlayMetronome()
    {
        if (!isMetronomeRunning)
        {
            isMetronomeRunning = true;
            _metronomeCoroutine = StartCoroutine(MetronomeRoutine());
        }
    }

    public void StopMetronome()
    {
        if (isMetronomeRunning)
        {
            isMetronomeRunning = false;
            if(_metronomeCoroutine != null)
            { 
                StopCoroutine(MetronomeRoutine());
                _metronomeCoroutine = null;
            }
        }
    }

    private IEnumerator MetronomeRoutine()
    {
        while(isMetronomeRunning)
        {
            normalMetronome.clip = normalMetronomeClip;
            normalMetronome.Play();
            yield return new WaitForSeconds(60f / _bpm);

        }
    }
}
