using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    private NavMeshAgent agent = null;
    private Camera mainCamera = null;

    #region Server

    [Command]
    private void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;

        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
        base.OnStartAuthority();
    }

    [ClientCallback]
    private void Update()
    {
        if (!isOwned) return;
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        CmdMove(hit.point);
    }

    #endregion
}
