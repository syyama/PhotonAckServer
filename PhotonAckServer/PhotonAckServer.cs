using Photon.SocketServer;

public class PhotonAckServer : ApplicationBase
{
    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        return new PhotonAckPeer(initRequest);
    }

    protected override void Setup()
    {
        PhotonAckGame.Instance = new PhotonAckGame();
        PhotonAckGame.Instance.Startup();
    }

    protected override void TearDown()
    {
        PhotonAckGame.Instance.Shutdown();
    }
}