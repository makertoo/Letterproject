using UnityEngine;
using UnityEngine.UI;

public class AudioButtonScript : MonoBehaviour
{
    public GameObject subImageGameObject;
    public Sprite audioOn;
    public Sprite audioOff;
    private void Awake()
    {
        bool isMuted = SoundManager.Instance.isAmbianceMusicMuted;
        subImageGameObject.GetComponent<Image>().sprite = isMuted ? audioOff : audioOn;
    }

    //Not sure if this works
    void OnTouchDown()
    {
        TurnOnOffBackgroundMusic();
    }

    public void TurnOnOffBackgroundMusic()
    {
        bool isMuted;
        SoundManager.Instance.TurnOnOffBackgroundMusic(out isMuted);
        subImageGameObject.GetComponent<Image>().sprite = isMuted ? audioOff : audioOn;
    }
}
