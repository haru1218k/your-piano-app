using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RecordScene
{
  public class RecordPlayButton : MonoBehaviour
  {
    [SerializeField] Metronome _metronome;
    private AudioSource _audioSource;
    private AudioClip _recordedClip;

    private Button _recordButton;
    private TextMeshProUGUI _recordButtonText;
    private int _recordingDuration = 10;
    private int _sampleRate = 44100;

    private int _bpm = 120;
    
    private bool _isFinishedRecording = false;

    public void Start()
    {
      _audioSource = GetComponent<AudioSource>();
      _recordButton = GetComponent<Button>();
      _recordButtonText = _recordButton.GetComponentInChildren<TextMeshProUGUI>();
      Initialize();
    }

    public void Initialize()
    {
      _recordButtonText.text = "record";
      _metronome.Initialize(_bpm);
    }

    private void OnClickRecordButton()
    {
      if (!_isFinishedRecording)
      {
        StartCoroutine(Record());
      }
      else
      {
        PlayClip();
      }
    }
   
    private IEnumerator Record()
    {
      _recordButton.interactable = false;
      _metronome.PlayMetronome();
      _recordedClip = Microphone.Start(null, false, _recordingDuration, _sampleRate);
      Debug.Log("録音中・・・");
      yield return new WaitForSeconds(_recordingDuration);
      _metronome.StopMetronome();
      _isFinishedRecording = true;
      _recordButton.interactable = true;
      _recordButtonText.text = "play";  
    }

    private void PlayClip()
    {
      if (_isFinishedRecording)
      {
        _audioSource.clip = _recordedClip;
        _audioSource.Play();
      }
    }

    

  }
}
