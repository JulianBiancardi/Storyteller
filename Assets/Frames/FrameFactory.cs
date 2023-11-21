using System;
using System.Collections.Generic;
using UnityEngine;

public struct FrameInfo{
    public FrameSet frameSet;
    public Sprite sprite;

    public FrameInfo(FrameSet frameSet, Sprite sprite){
        this.frameSet = frameSet;
        this.sprite = sprite;
    }
}

public static class FrameFactory
{
    private static readonly Dictionary<FrameSet, GameObject> frames = new();

    static FrameFactory(){
        //create a array of FrameSets
        FrameSet[] setToLoad = new FrameSet[]{
            FrameSet.Cementery,
            FrameSet.Garden,
            FrameSet.God,
        };

        string path = "frames/";

        foreach(FrameSet set in setToLoad){
            GameObject framePrefab = Resources.Load<GameObject>(path + set.ToString().ToLower());
            frames.Add(set, framePrefab);
        }
    }

    public static GameObject CreateFrame(FrameSet set){
        GameObject framePrefab = frames.GetValueOrDefault(set);
        return UnityEngine.Object.Instantiate(framePrefab);
    }
}
