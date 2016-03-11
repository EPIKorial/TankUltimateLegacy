using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerNameScript : MonoBehaviour {

    public InputField _inputField;
    public string test;

	void Start () 
    {
        _inputField.GetComponent<InputField>().text = PlayerPrefs.GetString("playerName");
	}

    void Update()
    {
        test = PlayerPrefs.GetString("playerName");
    }
    public void setPlayerName()
    {
        PlayerPrefs.SetString("playerName", _inputField.GetComponent<InputField>().text);
        PlayerPrefs.Save();
    }

}
