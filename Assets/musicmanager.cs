using UnityEngine;

public class musicmanager : MonoBehaviour
{
    [Header("---AudioSource---")]
    [SerializeField] AudioSource musicSource;
    [Header("---AudioClip---")]
    public AudioClip background;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
