using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip clipFx;
    public AudioClip clipBGM;

    AudioSource audioSourceFx;
    AudioSource audioSourceBGM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        audioSourceFx = gameObject.AddComponent<AudioSource>();
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundFx()
    {
        audioSourceFx.PlayOneShot(clipFx);
    }

    public void PlaySoundBGM()
    {
        audioSourceBGM.clip = clipBGM;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }
}
