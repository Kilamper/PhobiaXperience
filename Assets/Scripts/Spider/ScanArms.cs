using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArms : MonoBehaviour
{
    [SerializeField] private int armCount = 11;
    [SerializeField] private float armLength = 2;
    [SerializeField] private int armPoints= 4;

    [SerializeField] private bool weightByDist = false;

    [SerializeField, Range(0, 360)] private float arcAngle = 270;
    [SerializeField] private int resolution = 4;
    [SerializeField] private LayerMask arcLayer;

    [SerializeField] private bool gizmoDrawPoint = true;
    [SerializeField] private bool gizmoDrawLink = true;

    private void OnDrawGizmos()
    {
        Scan(true);
    }

    public List<(Vector3 pos, Quaternion rot, float weight)> Points()
    {
        return Scan(false);
    }

    List<(Vector3, Quaternion, float)> Scan(bool gizmo = false)
    {
        List<(Vector3 pos, Quaternion rot, float weight)> points = new List<(Vector3, Quaternion, float)>();
        
        // Calculate distance between each arc cast
        float arcRadius = armLength / armPoints;

        for (int i = 0; i < armCount; i++)
        {
            float angle = 360 * i / armCount;

            Vector3 pos = transform.position;
            
            // Rotate by angle
            Quaternion rot = transform.rotation * Quaternion.Euler(0, angle, 0);

            for (int j = 0;
                 j < armPoints && PhysicsExtensions.ArcCast(pos, rot, arcAngle, arcRadius, resolution, arcLayer,
                     out RaycastHit hit);
                 j++)
            {
                float weight = weightByDist ? 1 - (float)j / armPoints : 1;

                if (gizmo)
                    Gizmos.color = new Color(1, 1, 1, weight);
                
                if (gizmo && gizmoDrawLink)
                    Gizmos.DrawLine(pos, hit.point);


                pos = hit.point;
                rot.MatchUp(hit.normal);
                
                points.Add((pos, rot * Quaternion.Euler(0, -angle, 0), weight));
                
                if (gizmo && gizmoDrawPoint)
                    Gizmos.DrawSphere(pos, 0.1f);
            }
        }

        return points;
    }
}
