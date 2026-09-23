using UnityEngine;

public class DoorSoundBank : MonoBehaviour
{
    [Header("Default Door Sounds")]
    public AudioClip openClip;
    public AudioClip closeClip;

    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    public void PlayOpen()
    {
        Debug.Log("[DoorSoundBank] PlayOpen 호출됨!");

        if (openClip != null)
        {
            Debug.Log("[DoorSoundBank] Open Clip 재생: " + openClip.name);
            audioSource.PlayOneShot(openClip, volume);
        }
        else
        {
            Debug.LogWarning("[DoorSoundBank] Open Clip이 비어있습니다!");
        }
    }

    public void PlayClose()
    {
        Debug.Log("[DoorSoundBank] PlayClose 호출됨!");

        if (closeClip != null)
        {
            Debug.Log("[DoorSoundBank] Close Clip 재생: " + closeClip.name);
            audioSource.PlayOneShot(closeClip, volume);
        }
        else
        {
            Debug.LogWarning("[DoorSoundBank] Close Clip이 비어있습니다!");
        }
    }
}
