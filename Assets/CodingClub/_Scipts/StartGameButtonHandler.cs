using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGameButtonHandler : MonoBehaviour {

	private GameObject network;
	public Button butt;

	
	public void AssignHandler()
	{
		butt.onClick.AddListener(network.GetComponent<NetWorkManagerCustom>().StartupHost);
	}
	
	void Update () {
		if (GameObject.Find("Network") && network == null)
		{
			network = GameObject.Find("Network");
			AssignHandler();
		}

			
	}
}
