using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private AudioClip WinFXClip;
    [SerializeField] private AudioClip LoseFXClip;
    [SerializeField] private AudioClip[] MarkusFXClip;
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(MarkusFXClip, transform, 1f);
            SoundFXManager.instance.PlaySoundFXClip(WinFXClip, transform, .2f);
        }

        else
        {
            SoundFXManager.instance.PlaySoundFXClip(LoseFXClip, transform, 1f);
        }
    }
    
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Frederik");
        CardGameManager.singleton.StopClient();
        CardGameManager.singleton.StopHost();
    }
}
