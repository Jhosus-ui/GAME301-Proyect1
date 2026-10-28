using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource AmbienceAS;
    public AudioSource AmbienceAS2;
    public AudioClip AmbienceSFX;
    public AudioClip AmbienceSFX2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!AmbienceAS.isPlaying)
        {
            AmbienceAS.PlayOneShot(AmbienceSFX, 0.5f);
        }
        if (!AmbienceAS2.isPlaying)
        {
            AmbienceAS2.PlayOneShot(AmbienceSFX2, 0.75f);
        }
    }
}
