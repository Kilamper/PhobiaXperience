
using UnityEngine;

public static class PhysicsExtensions
{
    static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit, bool drawGizmo = false)
    {
        // Make sure the arc spans the full length of the angle by rotating the start by -angle/2
        rotation *= Quaternion.Euler(-angle/2, 0, 0);

        // To increment the start of each ray, if angle = 180 and resolution = 6 we increment by 30 degrees at each resolution step
        float dAngle = angle / resolution;
        Vector3 forwardRadius = Vector3.forward * radius;

        // A is the start of the arc segment, B is the end of the arc segment, AB is the ray between
        // This is to set up the first step of the arc
        Vector3 A, B, AB;
        A = forwardRadius;
        B = Quaternion.Euler(dAngle, 0, 0) * forwardRadius;
        AB = B - A;
        float AB_magnitude = AB.magnitude * 1.001f;

        for (int i = 0; i < resolution; i++)
        {
            A = center + rotation * forwardRadius;
            rotation *= Quaternion.Euler(dAngle, 0, 0);
            B = center + rotation * forwardRadius;
            AB = B - A;

            if (Physics.Raycast(A, AB, out hit, AB_magnitude, layer))
            {
                if (drawGizmo)
                    Gizmos.DrawLine(A, hit.point);

                return true;
            }

            if (drawGizmo)
                Gizmos.DrawLine(A, B);
        }

        hit = new RaycastHit();
        return false;
    }
}