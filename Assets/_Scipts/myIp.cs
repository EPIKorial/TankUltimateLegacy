using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class myIp : MonoBehaviour {

    public Text _text;
    private float rate;

    public GameObject[] players;
    public Text[] _playerText;
    private int i = 0;

	void Update()
    {
        if (GameObject.Find("NetworkManager"))
            _text.text = GameObject.Find("NetworkManager").GetComponent<NetWorkManagerCustom>().myIp;

        players = GameObject.FindGameObjectsWithTag("Player");
        for (int x = 0; x < 12;x++ )
        {
            _playerText[i].text = "";
        }
        i = 0;
        foreach (GameObject player in players)
        {
            _playerText[i].text = player.name + " : " + player.GetComponent<PlayerManager>().life.ToString();
            ++i;
        }
    }
}
