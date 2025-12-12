using System.Collections.Generic;
using System.Linq;
using Mirror.Discovery;
using UnityEngine;

public class UI : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    private List<Sprite> sprites;
    
    public GameObject enemySprite;
    private void Start()
    {
        AddDescendants(gameObject.transform, children);
        sprites = Resources.LoadAll<Sprite>("EnemySprites").ToList();
        enemySprite.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        CardGameManager.singleton.OnConnected.AddListener(OnConnect);
        CardGameManager.singleton.OnDisconnected.AddListener(OnDisconnect);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartHostButton()
    {
        CardGameManager.singleton.StartHost();
        CardGameManager.singleton.gameObject.GetComponent<NetworkDiscovery>().AdvertiseServer();
    }

    public void OnConnect()
    {
        //gameObject.SetActive(false);
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
    }

    public void OnDisconnect()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(true);
        }
    }
    private void AddDescendants(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child.gameObject);
            AddDescendants(child, list);
        }
    }
    
    
}
