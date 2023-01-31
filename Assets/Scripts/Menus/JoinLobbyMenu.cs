using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField addressInput = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable() 
    {
        RtsNetworkManager.ClientOnConnected += HancleClientConnected;
        RtsNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }

    private void OnDisable() 
    {
        RtsNetworkManager.ClientOnConnected -= HancleClientConnected;
        RtsNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }

    public void Join() 
    {
        string address = addressInput.text;

        NetworkManager.singleton.networkAddress = address;
        NetworkManager.singleton.StartClient();

        joinButton.interactable = false;
    }

    private void HancleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
