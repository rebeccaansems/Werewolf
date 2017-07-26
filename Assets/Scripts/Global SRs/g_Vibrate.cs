using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_Vibrate : MonoBehaviour
{
    public void SendVibrate()
    {
        this.GetComponent<PhotonView>().RPC("Vibrate", PhotonNetwork.masterClient);
    }

    [PunRPC]
    public void Vibrate()
    {
        Debug.Log("buzz buzz");
    }
}
