using System.IO;
using TsAPI.Types;
using UnityEngine;

public class SimplifiedChannelsGenerator : MonoBehaviour
{
    #if UNITY_EDITOR
    [UnityEditor.MenuItem("Teslasuit/Generate Simplified Haptic Channels")]
    #endif
    static void Generate()
    {
        #if UNITY_EDITOR
        var path = $"Assets/TS/Assets/SimplifiedHapticChannels/";
        Directory.CreateDirectory(path);

        foreach (var bone in TsHapticBones.RequiredBones)
        {
            TsHapticSimplifiedChannel.Create(bone, TsBone2dSide.Front, path);
            TsHapticSimplifiedChannel.Create(bone, TsBone2dSide.Back, path);
        }
        
        UnityEditor.AssetDatabase.SaveAssets();
        #endif
    }

}
