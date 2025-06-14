using System;
using System.Collections.Generic;
using TsAPI.Types;
using TsSDK;
using UnityEngine;

public class TsHumanAnimator : MonoBehaviour
{
    [SerializeField]
    private TsMotionProvider m_motionProvider;

    [SerializeField]
    private TsAvatarSettings m_avatarSettings;

    private Dictionary<TsHumanBoneIndex, Transform> m_bonesTransforms = new Dictionary<TsHumanBoneIndex, Transform>();
    private TsHumanBoneIndex m_rootBone = TsHumanBoneIndex.Hips;

    private void Start()
    {
        if (m_avatarSettings == null)
        {
            Debug.LogError("Missing avatar settings for this character.");
            enabled = false;
            return;
        }

        if(!m_avatarSettings.IsValid)
        {
            Debug.LogError("Invalid avatar settings for this character. Check that all required bones is configured correctly.");
            enabled = false;
            return;
        }

        SetupAvatarBones();
    }

    private void SetupAvatarBones()
    {
        foreach (var reqBoneIndex in TsHumanBones.SuitBones)
        {
            var transformName = m_avatarSettings.GetTransformName(reqBoneIndex);
            var boneTransform = TransformUtils.FindChildRecursive(transform, transformName);
            if (boneTransform != null && !m_bonesTransforms.ContainsKey(reqBoneIndex))
            {
                m_bonesTransforms.Add(reqBoneIndex, boneTransform);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var skeleton = m_motionProvider.GetSkeleton(Time.time);
        Update(skeleton);
    }

    public bool IPose = false;
    private void Update(ISkeleton skeleton)
    {
        if (skeleton == null)
        {
            return;
        }
        foreach (var boneIndex in TsHumanBones.SuitBones)
        {
            var poseRotation = m_avatarSettings.GetIPoseRotation(boneIndex);
            var targetRotation = Conversion.TsRotationToUnityRotation(skeleton.GetBoneTransform(boneIndex).rotation);

            TryDoWithBone(boneIndex, (boneTransform) =>
            {
                boneTransform.rotation = targetRotation * poseRotation;
            });
        }

        TryDoWithBone(m_rootBone, (boneTransform) =>
        {
            var hipsPos = skeleton.GetBoneTransform(TsHumanBoneIndex.Hips).position;
            boneTransform.transform.position = Conversion.TsVector3ToUnityVector3(hipsPos);
        });

        if (IPose)
        {
            m_motionProvider.Calibrate();
            IPose = false;
        }
    }

    public void Calibrate()
    {
        m_motionProvider?.Calibrate();
    }

    private void TryDoWithBone(TsHumanBoneIndex boneIndex, Action<Transform> action)
    {
        if (!m_bonesTransforms.TryGetValue(boneIndex, out var boneTransform))
        {
            return;
        }

        action(boneTransform);
    }
    
}
