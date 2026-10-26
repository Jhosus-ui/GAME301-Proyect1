using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Controls Background Music")]
    public bool BackgroundMusicEnabled;
    private AudioSource MusicPlayer;
    public AudioClip MainMenuMusic;

    [Header("Controls Loading Out")]
    public SpriteRenderer ShadowSR;
    private bool StartLeaving;
    private float Opacity;
    public float FadeSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundMusicEnabled && !MusicPlayer.isPlaying)
        {
            MusicPlayer.PlayOneShot(MainMenuMusic, 0.75f);
        }

        if (StartLeaving == true)
        {
            Opacity += FadeSpeed * Time.deltaTime;
            MusicPlayer.volume = 1 - Opacity;
            ShadowSR.color = new Color(0, 0, 0, Opacity);

            if (Opacity > 1)
            {
                SceneManager.LoadScene("GAMEPLAY INTEGRATION");
            }
        }
    }
    public void LoadGame()
    {
        StartLeaving = true;
        
    }
}
