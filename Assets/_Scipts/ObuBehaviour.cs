using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ObuBehaviour : NetworkBehaviour {
	
	[SyncVar]
	public Quaternion direction;
    public GameObject shooter;


	
	// Update is called once per frame
	void Update () {
		
		this.transform.rotation = direction;
		transform.Translate(Vector3.forward * 50 * Time.deltaTime, Space.Self);
		
	}
}