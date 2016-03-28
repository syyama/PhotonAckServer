using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

public class PhotonAckGame
{
    public static PhotonAckGame Instance;

    public List<ClientPeer> Connections;

    public void Startup()
    {
        Connections = new List<ClientPeer>();
    }

    public void Shutdown()
    {
        // kick out any players still on the server before shutting down
        foreach(ClientPeer peer in Connections)
        {
            peer.Disconnect();
        }
    }

    public void PeerConnected(ClientPeer peer)
    {
        lock(Connections)
        {
            Connections.Add(peer);
        }
    }

    public void PeerDisconnected(ClientPeer peer)
    {
        lock(Connections)
        {
            Connections.Remove(peer);
        }
    }

    public void OnOperationRequest(ClientPeer src, OperationRequest request, SendParameters sendParams)
    {
        // send ack to peer
        src.SendOperationResponse(new OperationResponse((byte)PhotonAckResponseTypes.Ack), sendParams);
    }
}