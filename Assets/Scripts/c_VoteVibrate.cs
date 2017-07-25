using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class c_VoteVibrate : MonoBehaviour
{
    public GameObject voteBlock, voteBlockHeader;

    void Start()
    {
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].CustomProperties["JoinNumber"] != null)
            {
                //int playerNum = int.Parse(PhotonNetwork.playerList[i].CustomProperties["JoinNumber"].ToString());
                //if (playerNum != -1 && !PhotonNetwork.playerList[i].NickName.Equals(""))
                //{
                    GameObject newVoteBlock = Instantiate(voteBlock, voteBlockHeader.transform);
                    int charIndex = int.Parse(PhotonNetwork.playerList[i].CustomProperties["CharacterArtNum"].ToString());

                    voteBlock.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.playerList[i].NickName;
                    voteBlock.transform.GetChild(1).GetComponent<Text>().text = GetComponent<c_PossibleCharacterInfo>().characterSubs[charIndex];
                    voteBlock.transform.GetChild(2).GetComponent<Image>().sprite = GetComponent<c_PossibleCharacterInfo>().characterHeadshot[charIndex];
               // }
            }
        }
    }
}
