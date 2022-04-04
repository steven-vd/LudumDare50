using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip[] clips;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandom() {
        audioSource.clip = clips[Random.Range(0, clips.Length - 1)];
        audioSource.Play();
    }

    public void PlayRandom(int clipIndex) {
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }

}
