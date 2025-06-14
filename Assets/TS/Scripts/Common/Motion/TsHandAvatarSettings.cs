using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TsAPI.Types;
using TsSDK;
using UnityEngine;

[CreateAssetMenu(menuName = "Teslasuit/Motion/Hand avatar settings")]
public class TsHandAvatarSettings : ScriptableObject
{
    public Transform HandModel;
    public Avatar HandAvatar;
    [SerializeField]
    public HandFinger[] HandFingers;
    public TsDeviceSide Side;

    [SerializeField]
    private bool useWrist = true;

    [Serializable]
    public struct HandFinger
    {
        public Transform transform;
        public TsHumanBoneIndex boneIndex;
    }

    private void OnValidate()
    {
        if (HandModel == null)
        {
            return;
        }

        List<HandFinger> fingers = new List<HandFinger>();

        List<TsHumanBoneIndex> phalanxes = new List<TsHumanBoneIndex>();
        //Transform wrist = HandModel;
        //if(!RootAsWrist)
        //{
        //    HandAvatarMapper.FindWristTransform(HandModel);
        //}

        if (Side == TsDeviceSide.Left)
        {
            phalanxes = TsHumanBones.LeftHandBones.ToList();
            if(!useWrist)
            {
                phalanxes.Remove(TsHumanBoneIndex.LeftHand);
            }
            //fingers.Add(new HandFinger(){boneIndex = TsHumanBoneIndex.LeftHand, transform = wrist});
        }
        else if (Side == TsDeviceSide.Right)
        {
            phalanxes = TsHumanBones.RightHandBones.ToList();
            if(!useWrist)
            {
                phalanxes.Remove(TsHumanBoneIndex.RightHand);
            }
            //fingers.Add(new HandFinger(){boneIndex = TsHumanBoneIndex.RightHand, transform = wrist});
        }

        foreach (var p in phalanxes)
        {
            var transform = HandAvatarMapper.FindPhalanxTransform(p, HandModel);
            
            fingers.Add( new HandFinger(){boneIndex = p, transform = transform});
        }

        HandFingers = fingers.ToArray();
    }

   
}
