using System;

[Serializable]
public class FrameResult{
    public EventType eventType = EventType.None;
    public ActorId source = ActorId.None;
    public ActorId target = ActorId.None;
    public DeathCause deathCause = DeathCause.None;
    public HearthbreakCause hearthbreakCause = HearthbreakCause.None;

    public FrameResult(){}

    public FrameResult(EventType et){
        this.eventType = et;
    }

    public FrameResult From(ActorId source){
        this.source = source;
        return this;
    }

    public FrameResult To(ActorId target){
        this.target = target;
        return this;
    }

    public FrameResult WithDeathCause(DeathCause deathCause){
        this.deathCause = deathCause;
        return this;
    }

    public FrameResult WithHearthbreakCause(HearthbreakCause hearthbreakCause){
        this.hearthbreakCause = hearthbreakCause;
        return this;
    }

    public bool SameAs(FrameResult other){
        return this.eventType == other.eventType && this.source == other.source && this.target == other.target && this.deathCause == other.deathCause;
    }

    public string ToText(){
        return eventType.ToString() + " " + source.ToString() + " " + target.ToString() + " " + deathCause.ToString();
    }
}
