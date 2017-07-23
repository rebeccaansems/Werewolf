using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_CharacterSelect : MonoBehaviour
{

    public InputField playerName;
    public GameObject submitCharPanel, startGamePanel, nameBlockHeader, nameBlock, startGameButton;

    private int previousNumPlayers = 1;
    private List<bool> hasBeenAdded = new List<bool>() { false, false, false, false, false, false };

    public void submitPlayerInfo()
    {
        PhotonNetwork.playerName = playerName.text;
        submitCharPanel.SetActive(false);
        startGamePanel.SetActive(true);

        if (PhotonNetwork.player.ID == 2)
        {
            Debug.Log("[PHOTON] Player is first to join");
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (startGamePanel.GetActive())
        {
            if (PhotonNetwork.inRoom && PhotonNetwork.room.PlayerCount > previousNumPlayers)
            {
                for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
                {
                    if(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
                    {
                        int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                        if (playerNum != -1 && !hasBeenAdded[playerNum] && !PhotonNetwork.playerList[i].NickName.Equals(""))
                        {
                            hasBeenAdded[playerNum] = true;
                            GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                            newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                            previousNumPlayers = PhotonNetwork.room.PlayerCount;
                        }
                    }
                }
            }
        }
    }
}
