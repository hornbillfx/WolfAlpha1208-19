using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UserNameSync : MonoBehaviour,IPunObservable
{
    public PhotonView pv;
    public string userName;

    // Start is called before the first frame update
    void Start()
    {
        pv.GetComponent<PhotonView>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
            pv.RPC("NameSetRPC", RpcTarget.AllBuffered, null);
    }
    public void SetUserName(string temp)
    {
        userName = temp;
        //  pv.RPC("Namefunc", RpcTarget.AllBuffered, temp);

    }
    [PunRPC]
    public void NameSetRPC()
    {
        //Manager.manage.totalPlayerName.Add(userName);
        if (!rest.Contains(temp))
        {
            rest.Add(temp);
        }
    }
        public void PlayerNameAdd()
    {
      
    }
    public List<string> rest=new List<string>();
    public string temp;
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting )
        {
            //We own this player: send the others our data
            stream.SendNext(userName);
          
        }
        else if (stream.IsReading)
        {
             temp = (string)stream.ReceiveNext();
          
           
          
        }

    }
}
