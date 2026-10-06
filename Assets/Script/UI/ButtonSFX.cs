using UnityEngine;

/// <summary>
/// Shared click sound player for menu buttons.
/// </summary>
public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;
    [SerializeField, Range(0f, 1f)] private float volume = 0.8f;

    public void PlayClick()
    {
        PlayClipFromListener(clickClip, volume);
    }

    public static void PlayClipFromListener(AudioClip clip, float clipVolume = 0.8f)
    {
        if (clip == null)
            return;

        Camera mainCamera = Camera.main;
        Vector3 position = mainCamera != null ? mainCamera.transform.position : Vector3.zero;
        AudioSource.PlayClipAtPoint(clip, position, clipVolume);
    }
}
