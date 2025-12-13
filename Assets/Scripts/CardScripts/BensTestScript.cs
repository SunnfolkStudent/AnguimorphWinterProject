using UnityEngine;
using UnityEngine.SceneManagement;

public class BensTestScript : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("WinScene");
    }
}
