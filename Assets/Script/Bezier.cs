using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [SerializeField] private Transform point_0;
    [SerializeField] private Transform point_1;
    [SerializeField] private Transform handle_0;
    [SerializeField] private Transform handle_1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 pointSize = Vector3.one;

        for (float i = 0; i < 1.0f; i += 0.025f)
        {
            Gizmos.DrawWireCube(findPointInCurve(i), pointSize);
        }

        Gizmos.DrawSphere(point_0.position, 2.0f);
        Gizmos.DrawSphere(point_1.position, 2.0f);
        Gizmos.DrawSphere(handle_0.position, 2.0f);
        Gizmos.DrawSphere(handle_0.position, 2.0f);

        Gizmos.DrawLine(point_0.position, handle_0.position);
        Gizmos.DrawLine(point_1.position, handle_1.position);
    }

    public Vector3 findPointInCurve(float progress)
    {
        return (point_0.position * Mathf.Pow(1 - progress, 3)) +
            (3 * handle_0.position * progress * Mathf.Pow(1 - progress, 2)) +
            (3 * handle_1.position * Mathf.Pow(progress, 2) *
            (1 - progress)) + (point_1.position * Mathf.Pow(progress, 3));
    }
}
