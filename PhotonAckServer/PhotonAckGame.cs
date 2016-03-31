using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

public class PhotonAckGame
{
    public static PhotonAckGame Instance;

    public List<PeerBase> Connections;

    public void Startup()
    {
        Connections = new List<PeerBase>();
    }

    public void Shutdown()
    {
        // kick out any players still on the server before shutting down
        foreach(PeerBase peer in Connections)
        {
            peer.Disconnect();
        }
    }

    public void PeerConnected(PeerBase peer)
    {
        lock(Connections)
        {
            Connections.Add(peer);
        }
    }

    public void PeerDisconnected(PeerBase peer)
    {
        lock(Connections)
        {
            Connections.Remove(peer);
        }
    }

    public void OnOperationRequest(PeerBase src, OperationRequest request, SendParameters sendParams)
    {
        // send ack to peer
        src.SendOperationResponse(new OperationResponse((byte)PhotonAckResponseTypes.Ack), sendParams);
    }
}