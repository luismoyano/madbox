using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;
    private bool receivesInput;

    private void Start()
    {
        receivesInput = true;

        player.onHit.AddListener(() => 
        {
            receivesInput = false;
        });

        player.onRecoverFromHit.AddListener(() => 
        {
            receivesInput = true;
        });

        player.onArrived.AddListener(() => 
        {
            receivesInput = false;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!receivesInput) return;

        if(Input.GetMouseButton(0))
        {
            player.moveForward();
        }
    }
}
