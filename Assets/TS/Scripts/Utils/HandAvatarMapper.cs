using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TsAPI.Types;
using UnityEngine;

public class HandAvatarMapper
{
    private static Dictionary<string, string[]> PhalanxKeyWords = new Dictionary<string, string[]>()
    {
        { "thumb", new[] {"thumb"}},
        { "index", new[] {"index"}},
        { "middle", new[] {"middle"}},
        { "ring", new[] {"ring"}},
        { "little", new[] {"little", "pinky"}}
    };

    public static Dictionary<string, string[]> PhalanxPartKeywords = new Dictionary<string, string[]>()
    {
        { "proximal", new[] {"prox", "1"}},
        { "intermediate", new[] {"inter","2"}},
        { "distal", new[] {"dist","3"}},
    };

    public static Dictionary<TsHumanBoneIndex, string[]> ExtraKeywords = new Dictionary<TsHumanBoneIndex, string[]>()
    {
        {TsHumanBoneIndex.LeftThumbProximal, new []{"prox", "0" }},
        {TsHumanBoneIndex.RightThumbProximal, new []{"prox", "0" }},
        //{TsHumanBoneIndex.LeftLittleProximal, new []{"prox", "0" }},
        //{TsHumanBoneIndex.RightLittleProximal, new []{"prox", "0" }},
    };

    private static string[] WristKeywords = {"wrist"};

    private static bool IsDistal(string name)
    {
        return PhalanxPartKeywords["distal"].Any((item) => name.Contains(item));
    }

    public static Transform FindPhalanxTransform(TsHumanBoneIndex key, Transform root)
    {
        Transform result = null;
        var keyFormatted = key.ToString().ToLowerInvariant();
        var phalanxKeywords = PhalanxKeyWords.Where((item) => keyFormatted.Contains(item.Key));
        
        var phalanxPartKeywordsExpr = PhalanxPartKeywords.Where((item) => keyFormatted.Contains(item.Key));
        string[] phalanxPartKeywords = new string[0];
        if(phalanxPartKeywordsExpr.Any())
        {
            phalanxPartKeywords = phalanxPartKeywordsExpr.First().Value;
        }
        if(ExtraKeywords.ContainsKey(key))
        {
            phalanxPartKeywords = ExtraKeywords[key];
        }
        var distal = IsDistal(keyFormatted);
        TransformUtils.IterateChildsRecursive(root, (child) =>
        {
            var name = child.name.ToLowerInvariant();
            if (!distal && child.childCount == 0) return false;
            if (phalanxKeywords.Any() && !MatchKeywords(name, phalanxKeywords.First().Value)) return false;
            if (phalanxPartKeywords.Any() && !MatchKeywords(name, phalanxPartKeywords)) return false;

            result = child;

            return true;

        });
        return result;
    }

    public static Transform FindWristTransform(Transform root)
    {
        Transform result = null;
        TransformUtils.IterateChildsRecursive(root, (child) =>
        {
            var name = child.name.ToLowerInvariant();
            if (child.childCount == 0) return false;
            if (!MatchKeywords(name, WristKeywords)) return false;

            result = child;

            return true;

        });
        return result;
    }

    private static bool MatchKeywords(string name, string[] keywords)
    {
        return keywords.Any(kw => name.Contains(kw));
    }
}
