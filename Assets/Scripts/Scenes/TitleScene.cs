using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.TitleSceneMusic);
    }

    public void Play()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        SceneManager.LoadScene("BriefingScene");
    }
}
