using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private Transform body;

    [Header("Transforms")]
    [SerializeField] private Transform[] targets;
    [SerializeField] private Transform[] orbits;
    [SerializeField] private Vector3[] targetPosOld;
    [SerializeField] private Transform[] legGroup1;
    [SerializeField] private Transform[] legGroup2;

    [SerializeField] private bool[] targetGrounded;
    [SerializeField] private LayerMask layer;

    [Header("Step Settings")]
    [SerializeField] private AnimationCurve stepCurve;
    [SerializeField] private float stepDuration = 0.5f;
    [SerializeField] private float stepLength = 0.5f;
    [SerializeField] private float stepHeight = 0.2f;
    [SerializeField] private float stepOffset = 0.1f;

    [Header("Adjustment Settings")]
    [SerializeField] private float positionAdjustSpeed = 5;
    [SerializeField] private float rotationAdjustSpeed = 5;
    [SerializeField] private float targetHeight;

    private RaycastHit hit;
    private Vector3 distance;


    private void Start()
    {
        targetPosOld = new Vector3[targets.Length];
        targetGrounded = new bool[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targetPosOld[i] = targets[i].position;
            targetGrounded[i] = true;
        }
    }

    private void Update()
    {
        for (int i = 0; i < orbits.Length; i++)
        {
            targets[i].position = targetPosOld[i];

            if (Physics.Raycast(orbits[i].position, -transform.up, out hit, 0.1f, layer))
            {
                if (Vector3.Distance(targets[i].position, hit.point) > stepLength)
                {
                    if (CanStep(i))
                    {
                        StartCoroutine(Step(i, hit.point, hit.normal));
                    }
                }
            }
        }


        for (int i = 0; i < orbits.Length; i++)
        {
            Debug.DrawLine(orbits[i].position, orbits[i].position + -transform.up * 0.1f, Color.red);
        }

        AdjustBodyPosition();
        AdjustBodyRotation();
    }

    private bool CanStep(int index)
    {
        bool isGroup1 = Array.IndexOf(legGroup1, targets[index]) >= 0;
        Transform[] oppositeGroup = isGroup1 ? legGroup2 : legGroup1;

        foreach (Transform leg in oppositeGroup)
        {
            int oppositeIndex = Array.IndexOf(targets, leg);
            if (oppositeIndex == -1)
            {
                //Debug.LogWarning($"Leg '{leg.name}' not found in targets array.");
                continue;
            }

            if (!targetGrounded[oppositeIndex])
                return false;
        }


        return true;
    }

    private IEnumerator Step(int legIndex, Vector3 targetPosition, Vector3 targetNormal)
    {
        targetGrounded[legIndex] = false;

        Vector3 endPos = targetPosition + (targetPosition - targetPosOld[legIndex]).normalized * stepOffset;

        float time = 0;

        while (time < stepDuration)
        {
            time += Time.deltaTime;
            float t = time / stepDuration;
            float s = stepCurve.Evaluate(t);

            Vector3 pos = Vector3.Lerp(targetPosOld[legIndex], endPos, t);
            pos.y += s * stepHeight;

            targetPosOld[legIndex] = pos;

            yield return null;
        }

        targetPosOld[legIndex] = endPos;
        targetGrounded[legIndex] = true;
    }

    private void AdjustBodyPosition(bool gizmo = false)
    {
        float averageOffset = 0f;

        for (int i = 0; i < targets.Length; i++)
        {
            averageOffset += Vector3.Dot(targets[i].position - body.position, transform.up);
        }

        averageOffset /= targets.Length;

        Vector3 targetPosition = body.position + transform.up * (averageOffset + targetHeight);

        if (gizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPosition, 0.1f);
        }
        else
        {
            body.position = Vector3.Lerp(body.position, targetPosition, Time.deltaTime * positionAdjustSpeed);
        }
    }

    private void AdjustBodyRotation()
    {
        Vector3 normal = Vector3.zero;

        for (int i = 0; i < targets.Length; i++)
        {
            if (targetGrounded[i])
            {
                if (Physics.Raycast(orbits[i].position, -transform.up, out RaycastHit hitInfo, 0.1f, layer))
                {
                    normal += hitInfo.normal;
                }
            }
        }

        normal.Normalize();

        Quaternion rot = Quaternion.FromToRotation(transform.up, normal) * body.rotation;

        body.rotation = Quaternion.Lerp(body.rotation, rot, Time.deltaTime * rotationAdjustSpeed);
    }
}
