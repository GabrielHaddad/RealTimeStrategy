using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class UnitBase : NetworkBehaviour
{
    [SerializeField] private Health health = null;

    public static event Action<int> ServerOnPlayerDie;
    public static event Action<UnitBase> ServerOnBaseSpawned = null;
    public static event Action<UnitBase> ServerOnBaseDespawned = null;

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += HandleServerOnDie;

        ServerOnBaseSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        health.ServerOnDie -= HandleServerOnDie;

        ServerOnBaseDespawned?.Invoke(this);
    }

    [Server]
    private void HandleServerOnDie()
    {
        ServerOnPlayerDie?.Invoke(connectionToClient.connectionId);

        NetworkServer.Destroy(gameObject);
    }

    #endregion

    #region Client

    #endregion
}
