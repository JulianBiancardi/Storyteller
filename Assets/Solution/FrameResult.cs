using System;

[Serializable]
public class Event{
    public EventType eventType = EventType.None;
    public ActorId source = ActorId.None;
    public ActorId target = ActorId.None;
    public DeathCause deathCause = DeathCause.None;
    public HearthbreakCause hearthbreakCause = HearthbreakCause.None;
    public ShockCause shockCause = ShockCause.None;
    public ExpressionInfo expressionInfo = new ExpressionInfo();

    public Event(){}

    public Event(EventType et){
        this.eventType = et;
    }

    public Event SetEventType(EventType eventType){
        this.eventType = eventType;
        return this;
    }

    public Event From(ActorId source){
        this.source = source;
        return this;
    }

    public Event To(ActorId target){
        this.target = target;
        return this;
    }

    public Event WithDeathCause(DeathCause deathCause){
        this.deathCause = deathCause;
        return this;
    }

    public Event WithHearthbreakCause(HearthbreakCause hearthbreakCause){
        this.hearthbreakCause = hearthbreakCause;
        return this;
    }

    public Event WithShockCause(ShockCause shockCause){
        this.shockCause = shockCause;
        return this;
    }

    public Event WithExpressionInfo(ExpressionInfo expressionInfo){
        this.expressionInfo = expressionInfo;
        return this;
    }
    public bool SameAs(Event other){
        return this.eventType == other.eventType && this.source == other.source && this.target == other.target && this.deathCause == other.deathCause && this.hearthbreakCause == other.hearthbreakCause && this.shockCause == other.shockCause;
    }

    public string ToText(){
        return eventType.ToString() + " " + source.ToString() + " " + target.ToString() + " " + deathCause.ToString();
    }
}
