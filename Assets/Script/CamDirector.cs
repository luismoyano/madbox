using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDirector : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CamPlayer cam;

    public void moveCamera()
    {
        cam.setPositionByGlobalProgress(player.globalProgress);
    }
}
