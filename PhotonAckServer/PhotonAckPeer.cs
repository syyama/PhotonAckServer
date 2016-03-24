using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

class PhotonAckPeer : PeerBase
{
    public PhotonAckPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer) : base(protocol, unmanagedPeer)
    {

    }

    protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
    {
        // クライアントに ack を送り返す
        OperationResponse response = new OperationResponse((byte)PhotonAckResponseTypes.Ack);
        this.SendOperationResponse(response, sendParameters);
    }
}