using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLoseScene : NetworkBehaviour
{
    void Lose()
    {
        Debug.Log("Lose");
        SceneManager.LoadScene("LoseScene");
    }
}
