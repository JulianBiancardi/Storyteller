using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Serializable]
public struct LevelSpec
{
    public LevelId levelId;
    public string goal;
    public int frames;
    public List<FrameSet> sets;
    public List<ActorId> actors;
    public List<FrameSet> initialFrames;
    public int toolSpacing;
}
