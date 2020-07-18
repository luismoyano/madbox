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

    [SerializeField] protected Bezier[] curves;
    public float speed;

    protected void Start()
    {
        localProgress = 0.0f;
        globalProgress = 0.0f;
        curveIndex = 0;
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
            globalProgress = 0;
            curveIndex = 0;
            return;
        }

        transform.position = curves[curveIndex].findPointInCurve(localProgress);
        transform.LookAt(curves[curveIndex].findPointInCurve(localProgress + 0.05f));

        onMovedForward.Invoke();
    }

    public virtual void setPositionByGlobalProgress(float global)
    {
        int index = Mathf.Min(curves.Length - 1, Mathf.FloorToInt(curves.Length * global));
        float local = (global * curves.Length) - index;

        transform.position = curves[index].findPointInCurve(local);
    }
}
