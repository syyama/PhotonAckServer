using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

using System.Collections.Generic;

class PhotonAckPeer : ClientPeer
{
    // note that we use long.MinValue rather than zero. Signed integer values have a negative
    // minimum and positive maximum. Starting at zero divides the possible range of IDs in half,
    // but starting at the lowest possible value gives us the largest possible range.
    private static long lastAssignedPlayerID = long.MinValue;
    private static object lockPlayerID = new object();

    public long PlayerID;

    public PhotonAckPeer(InitRequest initRequest) : base(initRequest)
    {
        lock(lockPlayerID)
        {
            this.PlayerID = lastAssignedPlayerID;
            lastAssignedPlayerID++;
        }
        PhotonAckGame.Instance.PeerConnected(this);

        EventData evt = new EventData((byte)PhotonAckEventType.AssingPlayerID);
        evt.Parameters = new Dictionary<byte, object>() { { 0, this.PlayerID } };
        this.SendEvent(evt, new SendParameters());
    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
        PhotonAckGame.Instance.PeerDisconnected(this);
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        PhotonAckGame.Instance.OnOperationRequest(this, operationRequest, sendParameters);
    }
}