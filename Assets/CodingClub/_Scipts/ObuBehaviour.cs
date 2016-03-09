using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ObuBehaviour : NetworkBehaviour {
	
	[SyncVar]
	public Quaternion direction;
	

	
	// Update is called once per frame
	void Update () {
		
		this.transform.rotation = direction;
		transform.Translate(Vector3.forward * 25 * Time.deltaTime, Space.Self);
		
	}
}