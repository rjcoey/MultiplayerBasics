using Mirror;
using UnityEngine;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor = Color.white;

    [SerializeField] private TextMeshProUGUI displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        this.displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color color)
    {
        this.displayColor = color;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (string.IsNullOrWhiteSpace(newDisplayName)) return;
        if (newDisplayName.Length < 2 || newDisplayName.Length > 20) return;

        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log("New name is " + newDisplayName);
    }

    #endregion

}