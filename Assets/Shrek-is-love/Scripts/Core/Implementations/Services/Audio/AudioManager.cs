using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;
    void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixer;
        }
    }

    void Start()
    {
        EventManager.Instance.OnBossFirstAttack += OnBossAttackMusic;
        EventManager.Instance.OnVictory += OnVictoryMusic;
    }

    private void OnBossAttackMusic()
    {
        StopPlaying("MainTheme");
        Play("BossTheme");
    }

    public void OffBossAttackMusic()
    {
        EventManager.Instance.OnBossFirstAttack -= OnBossAttackMusic;
        StopPlaying("BossTheme");
        Play("MainTheme");
    }

    private void OnVictoryMusic()
    {
        StopPlaying("MainTheme");
        PlayAndResume("VictoryTheme", "MainTheme");
    }

    public void Play(string name)
    {
        Sound sound = FindSound(name);
        if (sound != null)
        {
            sound.source.Play();
        }
    }

    public void StopPlaying(string name)
    {
        Sound sound = FindSound(name);
        if (sound != null)
        {
            sound.source.Stop();
        }
    }

    private Sound FindSound(string name)
    {
        foreach (Sound sound in _sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }
        return null;
    }

    public void PlayAndResume(string tempTheme, string resumeTheme)
    {
        Sound tempSound = FindSound(tempTheme);
        if (tempSound != null)
        {
            tempSound.source.Play();
            if (!tempSound.source.loop)
            {
                StartCoroutine(WaitAndResume(tempSound.source.clip.length, resumeTheme));
            }
        }
    }
    
    private IEnumerator WaitAndResume(float delay, string resumeTheme)
    {
        yield return new WaitForSeconds(delay);
        Play(resumeTheme);
    }
}
