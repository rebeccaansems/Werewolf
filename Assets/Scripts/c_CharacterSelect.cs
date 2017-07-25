using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class c_CharacterSelect : MonoBehaviour
{
    public InputField playerName;
    public GameObject submitCharPanel, startGamePanel, nameBlockHeader, nameBlock, startGameButton;
    public Image characterImage;

    private int previousNumPlayers = 1, currentCharIndex = 0;
    private List<bool> hasBeenAdded = new List<bool>() { false, false, false, false, false, false };

    public void SubmitPlayerInfo()
    {
        if (!playerName.text.Equals(""))
        {
            PhotonNetwork.playerName = playerName.text;
            submitCharPanel.SetActive(false);
            startGamePanel.SetActive(true);

            ExitGames.Client.Photon.Hashtable playerInfo = new ExitGames.Client.Photon.Hashtable();
            playerInfo.Add("CharacterArtNum", currentCharIndex);
            PhotonNetwork.player.SetCustomProperties(playerInfo);

            if (PhotonNetwork.player.ID == 2)
            {
                Debug.Log("[PHOTON] Player is first to join");
                startGameButton.SetActive(true);
            }
            else
            {
                Debug.Log("[PHOTON] Player is not first to join");
                startGameButton.SetActive(false);
            }
        }
    }

    public void CharacterButtonPressed(int direction)
    {
        currentCharIndex += direction;

        if (currentCharIndex < 0)
        {
            currentCharIndex = GetComponent<c_PossibleCharacterInfo>().characters.Length - 1;
        }
        else if (currentCharIndex > GetComponent<c_PossibleCharacterInfo>().characters.Length - 1)
        {
            currentCharIndex = 0;
        }

        characterImage.sprite = GetComponent<c_PossibleCharacterInfo>().characters[currentCharIndex];
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (startGamePanel.GetActive())
        {
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
                {
                    int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                    if (playerNum != -1 && !hasBeenAdded[playerNum] && !PhotonNetwork.playerList[i].NickName.Equals(""))
                    {
                        hasBeenAdded[playerNum] = true;
                        GameObject newNameBlock = Instantiate(nameBlock, nameBlockHeader.transform);
                        int charIndex = int.Parse(PhotonNetwork.playerList[i].CustomProperties["CharacterArtNum"].ToString());

                        newNameBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                        newNameBlock.transform.GetChild(1).GetComponent<Text>().text = GetComponent<c_PossibleCharacterInfo>().characterSubs[charIndex];
                        newNameBlock.transform.GetChild(2).GetComponent<Image>().sprite = GetComponent<c_PossibleCharacterInfo>().characterHeadshot[charIndex];
                        previousNumPlayers = PhotonNetwork.room.PlayerCount;
                    }
                }
            }
        }
    }
}
