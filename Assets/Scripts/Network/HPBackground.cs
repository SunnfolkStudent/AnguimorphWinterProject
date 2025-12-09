using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class HPBackground : NetworkBehaviour
{
	[SerializeField] private List<Component> Indicators;
    void Start()
    {
	    foreach (Component c in Indicators)
	    {
		    if (isLocalPlayer) gameObject.GetComponent<RawImage>().color = new Color(0, 0, 255, 128);
		    else gameObject.GetComponent<RawImage>().color = new Color(255, 0, 0, 128);
	    }
    }

}
