using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private float localProgress;
    public float globalProgress;
    protected int curveIndex;

    public UnityEvent onMovedForward;
    public UnityEvent onHit;
    public UnityEvent onRecoverFromHit;
    public UnityEvent onArrived;

    [SerializeField] protected Bezier[] curves;
    public float speed;

    private float lastReachedCheckpoint;

    protected void Start()
    {
        localProgress = 0.0f;
        globalProgress = 0.0f;
        curveIndex = 0;

        lastReachedCheckpoint = 0.0f;

        setPositionByGlobalProgress(0);
        onMovedForward.Invoke();
    }

    public void moveForward()
    {
        float step = speed * Time.deltaTime;
        localProgress += step;
        globalProgress += step / curves.Length;

        if (localProgress >= 1)
        {
            curveIndex += 1;
            localProgress = 0;
        }

        if (curveIndex >= curves.Length)
        {
            onArrived.Invoke();
            return;
        }

        transform.position = curves[curveIndex].findPointInCurve(localProgress);
        transform.LookAt(curves[curveIndex].findPointInCurve(localProgress + 0.15f));

        onMovedForward.Invoke();
    }

    public virtual void setPositionByGlobalProgress(float global)
    {
        int index = Mathf.Min(curves.Length - 1, Mathf.FloorToInt(curves.Length * global));
        float local = (global * curves.Length) - index;

        globalProgress = global;
        localProgress = local;
        curveIndex = index;

        transform.position = curves[index].findPointInCurve(local);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Traps")
        {
            onHit.Invoke();
            moveToLastCheckPoint();
        }

        else if (collision.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Alcanzo checkpoint " + globalProgress);
            lastReachedCheckpoint = globalProgress;
        }
    }

    private void moveToLastCheckPoint()
    {
        resetRigidBody();
        setPositionByGlobalProgress(lastReachedCheckpoint);
        onMovedForward.Invoke();
        onRecoverFromHit.Invoke();
    }

    private void resetRigidBody()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.Sleep();
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.centerOfMass = Vector3.zero;
        body.angularDrag = 0.0f;

        body.WakeUp();
    }
}
