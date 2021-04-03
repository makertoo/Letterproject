using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundManager Instance = null;

    public AudioSource ambianceMusic;
    public AudioSource efxSource;
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public bool isAmbianceMusicMuted
    {
        get { return ambianceMusic.mute; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);//Allows us to have one "MusicManager" gameobject 
        }
        DontDestroyOnLoad(gameObject);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }


    public void TurnOnOffBackgroundMusic(out bool isMuted)
    {
        TurnOnOff();
        isMuted = ambianceMusic.mute;
    }


    /// <summary>
    /// Lance la lecture d'un son
    /// </summary>
    /// <param name="originalClip"></param>
    private void TurnOnOff()
    {
        ambianceMusic.mute = !ambianceMusic.mute;
    }
}
