using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Mouvements : NetworkBehaviour {
	
	
	public bool dead;
	
	public float speed = 15.00f;
	public float rotSpeed = 50.00f;
	
	public GameObject shotSpawn;
	public GameObject Explosion;
	public GameObject Obu;	
	
	[SyncVar]
	private Vector3 syncPos;
	
	[SyncVar]
	private float syncRot;
	
	private Transform myTransform;
	private Vector3 lastPos;
	private float lastRot;
	
	public AudioSource _audioS;
	public AudioSource _audioS2;
	public AudioClip _shot;
	public AudioClip idleSound;
	public AudioClip drivingSound;
	private bool idle;
	private bool driving;
	
	void Start () {
	
		dead = false;
		myTransform = GetComponent<Transform>();
		myTransform.FindChild("Camera").GetComponent<Camera>().enabled = false;		
		myTransform.FindChild("Camera").GetComponent<AudioListener>().enabled = false;
		lastPos = myTransform.position;	
		if (isLocalPlayer)
		{
			idle = true;
			driving = false;
			_audioS2 = shotSpawn.GetComponent<AudioSource>();
			_audioS = GetComponent<AudioSource>();
			myTransform.FindChild("Camera").GetComponent<Camera>().enabled = true;
			myTransform.FindChild("Camera").GetComponent<AudioListener>().enabled = true;
			Color _color = new Color (1, 0, 0);
			transform.FindChild("TankRenderers").transform.FindChild("TankChassis").GetComponent<MeshRenderer>().materials[0].color = _color;
			transform.FindChild("TankRenderers").transform.FindChild("TankTurret").GetComponent<MeshRenderer>().materials[0].color = _color;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

			
		if (isLocalPlayer && dead == false)
		{
			if (Input.GetAxis ("Vertical") != 0 && idle == true)
			{
				driving = true;
				idle = false;
				_audioS.clip = drivingSound;
				_audioS.Play ();
			}
			else if(Input.GetAxis ("Vertical") == 0 && driving == true)
			{
				driving = false;
				idle = true;
				_audioS.clip = idleSound;
				_audioS.Play ();
			}
			
			myTransform.Translate (Vector3.forward * Input.GetAxis ("Vertical") * Time.deltaTime * speed, Space.Self);
			myTransform.Rotate (0, Input.GetAxis ("Horizontal") * Time.deltaTime * rotSpeed, 0);
			
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
			
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_audioS2.clip = _shot;
				_audioS2.Play();
				CmdPlayerShootOnServer(shotSpawn.transform.position, myTransform.rotation);
			}
			
		}
		else
		{
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * 5);
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.Euler(0, syncRot, 0), Time.deltaTime * 5);
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
	
	[Command]
	void CmdPlayerShootOnServer(Vector3 pos, Quaternion rot)
	{
		GameObject obu = (GameObject)Instantiate(Obu, pos, rot);
		obu.GetComponent<ObuBehaviour>().direction = Quaternion.Euler(0, syncRot, 0);
		NetworkServer.Spawn(obu);
	}
}
