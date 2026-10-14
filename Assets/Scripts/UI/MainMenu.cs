using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Controls Background Music")]
    public bool BackgroundMusicEnabled;
    private AudioSource MusicPlayer;
    public AudioClip MainMenuMusic;

    [Header("Controls Flickering Light")]
    public Light DL;
    public float FlickerInterval;
    public float FlickerLength;
    private float FlickerLengthTimer;
    private float FlickerTimer;
    private bool FlickeredOnce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
        FlickerTimer = FlickerInterval;
        FlickerLengthTimer = FlickerLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundMusicEnabled && !MusicPlayer.isPlaying)
        {
            MusicPlayer.PlayOneShot(MainMenuMusic);
        }
        if (FlickerTimer > 0)
        {
            FlickerTimer -= Time.deltaTime;
        }
        else
        {
            DL.intensity = 50;
            FlickerLengthTimer -= Time.deltaTime;
            if (FlickerLengthTimer < 0 & FlickeredOnce)
            {
                DL.intensity = 70;
                FlickerLengthTimer = FlickerLength;
                FlickerTimer = FlickerInterval;
                FlickeredOnce = false;
            }
            if (FlickerLengthTimer < 0 && !FlickeredOnce)
            {
                FlickerLengthTimer = FlickerLength;
                DL.intensity = 70;
                FlickeredOnce = true;
                FlickerTimer = 0.2f;
            }
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GAMEPLAY INTEGRATION");
    }
}
