using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDisconnected(DisconnectCause cause);

namespace Lessons.Architecture
{
    public class PhotonInteractor : Interactor, IConnectionCallbacks, IMatchmakingCallbacks
    {
        public delegate void OnRoomList(List<RoomInfo> roomList);

        public event OnDisconnected OnDisconnectedEvent_Cause;

        public event Action OnConnectedEvent;
        public event Action OnDisconnectedEvent;
        public event Action OnCreatedRoomEvent;
        public event Action OnFailedCreateRoomEvent;
        public event Action OnJoinedRoomEvent;
        public event Action OnFailedJoinRoomEvent;
        public event Action OnFailedJoinRandomRoomEvent;
        public event Action OnJoinLobbyEvent;
        public event Action OnLeftLobbyEvent;
        public event Action OnLeftRoomEvent;
        public event OnRoomList OnRoomListUpdateEvent;

        private readonly RoomOptions roomOptions = new();
        //private readonly Hashtable hashtable = new();

        public override void Initialize()
        {
            base.Initialize();

            PhotonNetwork.AddCallbackTarget(this);
        }

        #region Voids
        public void LeaveRoom()
        {
            Debug.Log("����� �� �������");
            PhotonNetwork.LeaveRoom();
        }
        public void LoadLevel(int scene)
        {
            Debug.Log("�������� ����� " + scene);
            PhotonNetwork.LoadLevel(scene);
        }

        public void ConnectUsingSettings()
        {
            Debug.Log("����������� � ������� � ������������� ��������");
            PhotonNetwork.ConnectUsingSettings();
        }

        public void ConnectToBestServer()
        {
            Debug.Log("����������� � ������� �������");
            PhotonNetwork.ConnectToBestCloudServer();
        }

        public void ConnectToRegion(string region)
        {
            Debug.Log("����������� � " + region);
            PhotonNetwork.ConnectToRegion(region);
        }

        public void DisconnectServer()
        {
            Debug.Log("���������� �� �������");
            PhotonNetwork.Disconnect();
        }

        public void CreateRoom(string name, byte maxCountPlayers = 5)
        {
            roomOptions.MaxPlayers = maxCountPlayers;
            Debug.Log($"�������� ������� - {name}");
            PhotonNetwork.CreateRoom(name, roomOptions);
        }

        public void JoinOrCreateRoom(string name, byte maxCountPlayer)
        {
            roomOptions.MaxPlayers = maxCountPlayer;
            Debug.Log($"������������� ��� �������� ������� - {name}");
            PhotonNetwork.JoinOrCreateRoom(name, roomOptions, null);
        }

        public void JoinRandomRoom()
        {
            Debug.Log("������������� � ��������� �������");
            PhotonNetwork.JoinRandomRoom();
        }

        public void JoinRandomOrCreateRoom(byte maxCountPlayers = 5)
        {
            roomOptions.MaxPlayers = maxCountPlayers;
            //hashtable.Add("RoomName", "��������� �����");
            Debug.Log("������������� � ��������� ������� ��� � �����");
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: roomOptions);
        }

        public void JoinRoom(string name)
        {
            PhotonNetwork.JoinRoom(name);
        }
        #endregion

        #region Events

        public void OnConnectedToMaster()
        {
            OnConnectedEvent?.Invoke();
        }

        public void OnConnected()
        {

        }

        public void OnDisconnected(DisconnectCause cause)
        {
            OnDisconnectedEvent?.Invoke();
            OnDisconnectedEvent_Cause?.Invoke(cause);
        }

        public void OnCreatedRoom()
        {
            OnCreatedRoomEvent?.Invoke();
        }

        public void OnJoinedRoom()
        {
            OnJoinedRoomEvent?.Invoke();
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            OnFailedCreateRoomEvent?.Invoke();
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            OnFailedJoinRandomRoomEvent?.Invoke();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            OnJoinedRoomEvent?.Invoke();
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            OnRoomListUpdateEvent?.Invoke(roomList);
        }

        public void OnJoinedLobby()
        {
            OnJoinLobbyEvent?.Invoke();
        }

        public void OnLeftLobby()
        {
            OnLeftLobbyEvent?.Invoke();
        }

        public void OnLeftRoom()
        {
            OnLeftRoomEvent?.Invoke();
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {

        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {

        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {

        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {

        }


        #endregion

        #region Status

        public bool IsConnected
        {
            get
            {
                return PhotonNetwork.IsConnected;
            }
        }
        #endregion
    }
}
