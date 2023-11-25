using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "LevelSpec", menuName = "")]
public class LevelSpec: ScriptableObject
{
    public LevelId levelId;
    public AudioClip levelMusic;
    public string goal;
    public int frames;
    public List<FrameSet> sets;
    public List<ActorId> actors;
    public List<FrameSet> initialFrames;
    public int toolSpacing = 2;
}
