using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Frederik");
        CardGameManager.singleton.StopClient();
        CardGameManager.singleton.StopHost();
    }
}
