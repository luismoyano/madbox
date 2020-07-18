using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayer : Player
{
    [SerializeField] private Player player;

    public override void setPositionByGlobalProgress(float global)
    {
        base.setPositionByGlobalProgress(global);
        transform.LookAt(player.transform);
    }
}
