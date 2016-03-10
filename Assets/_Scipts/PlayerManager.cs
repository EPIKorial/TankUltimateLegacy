using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour {

	public int life = 100;
	public Text lifeText;
	public Button restartButton;
	
	[SyncVar]
	private int syncLife;
	
	// Use this for initialization
	void Start () {

		if (isLocalPlayer)
		{
			lifeText = transform.FindChild("TankCanvas").transform.FindChild("HP").GetComponent<Text>();
			restartButton = transform.FindChild("TankCanvas").transform.FindChild("RestartButton").GetComponent<Button>();
			restartButton.gameObject.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isLocalPlayer)
			lifeText.text = "PV : " + life.ToString();
			
		if (isLocalPlayer && life <= 0)
		{
			transform.GetComponent<Mouvements>().dead = true;
			//transform.GetComponent<BoxCollider>().enabled = false;
			transform.FindChild("TankRenderers").transform.FindChild("TankChassis").GetComponent<MeshRenderer>().enabled = false;
			transform.FindChild("TankRenderers").transform.FindChild("TankTurret").GetComponent<MeshRenderer>().enabled = false;
			transform.FindChild("TankRenderers").transform.FindChild("TankTracksRight").GetComponent<MeshRenderer>().enabled = false;
			transform.FindChild("TankRenderers").transform.FindChild("TankTracksLeft").GetComponent<MeshRenderer>().enabled = false;
			restartButton.gameObject.SetActive(true);
			restartButton.onClick.AddListener(RestartFunc);
			
			if (Input.GetKeyDown(KeyCode.R))
				RestartFunc();
		}
			
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "bullet")
		{
			life -= 10;	
		}
	}
	
	void RestartFunc()
	{
		life = 100;
		transform.GetComponent<Mouvements>().dead = false;
		transform.GetComponent<BoxCollider>().enabled = true;
		transform.FindChild("TankRenderers").transform.FindChild("TankChassis").GetComponent<MeshRenderer>().enabled = true;
		transform.FindChild("TankRenderers").transform.FindChild("TankTurret").GetComponent<MeshRenderer>().enabled = true;
		transform.FindChild("TankRenderers").transform.FindChild("TankTracksRight").GetComponent<MeshRenderer>().enabled = true;
		transform.FindChild("TankRenderers").transform.FindChild("TankTracksLeft").GetComponent<MeshRenderer>().enabled = true;
		restartButton.onClick.RemoveAllListeners();
		restartButton.gameObject.SetActive(false);
	}
}
