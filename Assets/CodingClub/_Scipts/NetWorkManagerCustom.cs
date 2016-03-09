﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

////////////////////////////////////////////////////////////////////////////////////////////
// Script perméttant la connection en réseau des devices
// UNET Unity5
////////////////////////////////////////////////////////////////////////////////////////////

public class NetWorkManagerCustom : NetworkManager{
	
	public Text errorMessage;
	public Text IpText;

    public void StartServer()
    {
        SetPort();
        NetworkManager.singleton.StartServer();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
	// Afficher un message d'erreur pendant un temp donné
    ////////////////////////////////////////////////////////////////////////////////////////////
	IEnumerator displayErrorMessage(string message, float time)
	{
			errorMessage.text = message;
			yield return new WaitForSeconds(time);
	}
	
    ////////////////////////////////////////////////////////////////////////////////////////////
	// Démarrer le serveur et s'y connecte comme client sur la page de gestion administrateur
    // Se connecter en tant que Host : Server+Client
    ////////////////////////////////////////////////////////////////////////////////////////////
	public void StartupHost()
	{
            SetPort();
            NetworkManager.singleton.StartHost();
	}

    ////////////////////////////////////////////////////////////////////////////////////////////
	// Se connecter au serveur comme client
    ////////////////////////////////////////////////////////////////////////////////////////////
	public void Join()
	{
		SetIPAdress();
		SetPort();
		NetworkManager.singleton.StartClient();
	}

    ////////////////////////////////////////////////////////////////////////////////////////////
	//Addresse Ip du Pc distant
    ////////////////////////////////////////////////////////////////////////////////////////////
	void SetIPAdress()
	{
        //NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.networkAddress = IpText.text;
	}

    ////////////////////////////////////////////////////////////////////////////////////////////
	// Port utilisé par défaut
    ////////////////////////////////////////////////////////////////////////////////////////////
	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}
	
	
	////////////////////////////////////////////////////////////////////////////////////////////
	// Permet de changer de scene
	////////////////////////////////////////////////////////////////////////////////////////////
	public void ChangeSceneTo(string scene)
	{
		Application.LoadLevel(scene);
	}
}