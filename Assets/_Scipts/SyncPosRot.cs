using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPosRot : NetworkBehaviour {
	
	[SyncVar]
	private Vector3 syncPos;
	
	[SyncVar]
	private float syncRot;
	
	private Transform myTransform;
	private Vector3 lastPos;
	private float lastRot;
	
	void Start () {
		
		myTransform = GetComponent<Transform>();
		lastPos = myTransform.position;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isLocalPlayer)
		{
			if (Vector3.Distance(lastPos, myTransform.position) > 0.1f)
			{
				TransmitPosition();
				lastPos = myTransform.position;
			}
			
			if (lastRot - myTransform.rotation.y != 0)
			{
				TransmitRotation();
				lastRot = myTransform.rotation.y;
			}
			
		}
		else
		{
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * 15);
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.Euler(0, syncRot, 0), Time.deltaTime * 15);
		}
		
	}
	
	void TransmitPosition()
	{
		CmdSendMyPosToServer(myTransform.position);
	}
	
	void TransmitRotation()
	{
		CmdSendMyRotToServer (myTransform.eulerAngles.y);
	}
	
	[Command]
	void CmdSendMyPosToServer(Vector3 posReceived)
	{
		syncPos = posReceived;
	}
	
	[Command]
	void CmdSendMyRotToServer(float rotReceived)
	{
		syncRot = rotReceived;
	}
	
	
}

