using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics = new List<AudioClip>();

    [SerializeField] private AudioSource _source;

    [SerializeField] private UiElement _radioUI;

    [SerializeField] private Transform _radioRoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Next"))
        {
            NextMusic();
        }
        
        if (_source.isPlaying == false)
        {
            NextMusic();
        }
    }

    public void NextMusic()
    {
        var clipIndex = Random.Range(0, _musics.Count);
        
        if (_source.clip&&_musics[clipIndex].name ==_source.clip.name)
        {
            NextMusic();
            return;
        }

        var ui = Instantiate(_radioUI, _radioRoot);
        ui.UpdateUI(_musics[clipIndex].name);
        _source.clip = _musics[clipIndex];
        _source.Play();

        StartCoroutine(Disable());
        
        IEnumerator Disable()
        {
            yield return new WaitForSeconds(5f);
            ui.DisableUI();
        }


    }
}
