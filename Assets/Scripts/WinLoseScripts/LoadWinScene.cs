using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : NetworkBehaviour
{
    void Win()
    {
        Debug.Log("Win");
        SceneManager.LoadScene("WinScene");
    }
}
