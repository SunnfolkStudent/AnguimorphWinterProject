using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : NetworkBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Frederik");
    }
}
