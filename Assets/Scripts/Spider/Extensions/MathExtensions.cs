using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtensions
{
    // "rotation * Vector3.up" this rotates the vector (0, 1, 0) by the rotation quaternion and gives a new Vector3
    // "FromToRotation(rotation * Vector3.up, up)" takes the new Vector3 and returns the
    // quaternion that represents the rotation from new Vector3, to up
    // at the last step we first apply the rotation quaternion and to that
    // we apply the quaternion we found earlier
    // A * B means we apply B and then A
    // Let rotation be our own rotation by doing rotation * Vector3.up we find our object space up,
    // let up parameter be the surface normal of a raycast hit
    // FromToRotation gives the quaternion that rotates our object space up to parameter up
    // By applying rotation and then FromToRotation(rotation * Vector3.up, up)
    // we rotate the object to match the surface hit normal
    public static Quaternion RotationMatchUp(Quaternion rotation, Vector3 up) => Quaternion.FromToRotation(rotation * Vector3.up, up) * rotation;
    public static void MatchUp(this ref Quaternion rotation, Vector3 up) => rotation = RotationMatchUp(rotation, up);
    public static void MatchUp(this Transform transform, Vector3 up) => transform.rotation = RotationMatchUp(transform.rotation, up);

    public static Vector4 Quat2Vec(Quaternion q) => new Vector4(q.x, q.y, q.z, q.w);

    public static Quaternion QuaternionAverage(Quaternion[] quats, float[] weights = null)
    {
        if (quats.Length == 0)
            return Quaternion.identity;

        if (weights != null && quats.Length != weights.Length)
            return Quaternion.identity;

        Vector4[] vects = new Vector4[quats.Length];
        for (int i = 0; i < vects.Length; i++)
            vects[i] = Quat2Vec(quats[i]);

        Vector4 vectsAvg = Vector4.zero;
        
        for (int i = 0; i < vects.Length; i++)
        {
            Vector4 v = vects[i];
            float w = weights == null ? 1 : weights[i];
            
            // This is to make sure the quaternions that we include in the average are not opposites or inverses
            // as q and -q represent the same rotation including both as they are, will make us lose information
            // We compare each vec4 with the first vec4 in the array and if they aren't "close" we invert the sign
            if (i > 0 && Vector4.Dot(v, vects[0]) < 0)
                w *= -1;

            vectsAvg += v * w;
        }

        vectsAvg.Normalize();

        return new Quaternion(vectsAvg.x, vectsAvg.y, vectsAvg.z, vectsAvg.w);
    }
}
