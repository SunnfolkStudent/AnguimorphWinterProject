using UnityEngine;

public class CardEnableChildren : MonoBehaviour
{
    public GameObject[]  children;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnableChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableChildren()
    {
        foreach (GameObject go in children)
        {
            go.SetActive(true);
        }
        
    }
}
