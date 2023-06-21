using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        player.SetDisplayName($"Player {numPlayers}");
        player.SetDisplayColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f));
    }
}
