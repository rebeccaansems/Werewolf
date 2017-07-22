using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_CharacterSelect : MonoBehaviour {

    public InputField playerName;
    public GameObject submitCharPanel, startGamePanel;

	public void submitPlayerInfo()
    {
        PhotonNetwork.playerName = playerName.text;
        submitCharPanel.SetActive(false);

        if (PhotonNetwork.player.ID == 2)
        {
            Debug.Log("[PHOTON] Player is first to join");
            startGamePanel.SetActive(true);
        }
    }
}
