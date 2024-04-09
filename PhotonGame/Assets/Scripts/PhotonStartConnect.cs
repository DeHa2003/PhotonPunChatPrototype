using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonStartConnect : PhotonConnect
{
    private string nameServer;
    public override void Initialize()
    {
        base.Initialize();

        nameServer = PlayerPrefs.GetString("RegionName", "eu");

        if (nameServer == "SingleMode")
        {
            photonInteractor.LoadLevel(2);
            return;
        }

        photonInteractor.OnConnectedEvent += ConnectChat;
        photonInteractor.OnDisconnectedEvent += LoadSingle;

        chatPhotonInteractor.Events.OnConnectedEvent += JoinRandomRoom;
        chatPhotonInteractor.Events.OnDisconnectedEvent += LoadSingle;

        photonInteractor.OnJoinedRoomEvent += LoadMultiplayer;
        photonInteractor.OnFailedJoinRoomEvent += LoadSingle;

        photonInteractor.ConnectUsingSettings();
    }

    private void Update()
    {
        if (chatPhotonInteractor != null)
        {
            chatPhotonInteractor.Service();
        }
    }

    private void LoadSingle()
    {
        photonInteractor.LoadLevel(2);
        Debug.Log("�������� ������� �����");
    }

    private void LoadMultiplayer()
    {
        photonInteractor.LoadLevel(3);
        Debug.Log("�������� ������ �����");
    }

    private void ConnectChat()
    {
        Debug.Log("����������� � ������� ����");
        chatPhotonInteractor.Connect();
    }

    private void JoinRandomRoom()
    {
        Debug.Log("����� ��������� �������");
        photonInteractor.JoinRandomOrCreateRoom(5);
    }

    private void OnDestroy()
    {
        photonInteractor.OnConnectedEvent -= ConnectChat;
        photonInteractor.OnDisconnectedEvent -= LoadSingle;

        chatPhotonInteractor.Events.OnConnectedEvent -= JoinRandomRoom;
        chatPhotonInteractor.Events.OnDisconnectedEvent -= LoadSingle;

        photonInteractor.OnJoinedRoomEvent -= LoadMultiplayer;
        photonInteractor.OnFailedJoinRoomEvent -= LoadSingle;
    }
}
