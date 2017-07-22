using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_CharacterSelect : MonoBehaviour {

    public InputField playerName;

	public void submitPlayerInfo()
    {
        PhotonNetwork.playerName = playerName.text;
    }
}
