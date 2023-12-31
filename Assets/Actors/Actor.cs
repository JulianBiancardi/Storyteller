using System.Diagnostics;
using UnityEngine;

public class Actor
{
    private ActorId actorId;
    protected ActorId inLoveWith;
    private bool needsLove;
    private bool isDead;
    private HeartbreakCause heartbreakCause;

    public Actor(ActorId actorId, bool needsLove){
        this.actorId = actorId;
        this.inLoveWith = ActorId.None;
        this.needsLove = needsLove;
        this.isDead = false;
    }

    public ActorId GetActorId(){
        return actorId;
    }

    public string GetName(){
        return actorId.ToString();
    }

    public bool NeedsLove(){
        return needsLove;
    }
    
    public bool IsLonely(){
        return inLoveWith == ActorId.None;
    }

    public bool IsInLoveWith(Actor actor){
        return inLoveWith == actor.actorId;
    }

    public bool IsDead(){
        return isDead;
    }


    public bool ComputeRomance(Actor actor){
        if(actor == null || actorId == actor.actorId){
            return false;
        }

        if(inLoveWith == ActorId.None && actor.inLoveWith == ActorId.None){
            inLoveWith = actor.actorId;
            actor.inLoveWith = actorId;
            return true;
        }

        if(IsInLoveWith(actor)){
            return true;
        }

        return false;
    }

    public void BreakHearth(HeartbreakCause cause){
        heartbreakCause = cause;
    }

    public void Reset() {
        inLoveWith = ActorId.None;
        isDead = false;
        heartbreakCause = HeartbreakCause.None;
    }

    public Event Romance(Actor other){
        ExpressionInfo expressionInfo = new ExpressionInfo();
        expressionInfo.SetExpressionType(ExpressionType.Idling);
        Event result = new Event(EventType.Idling).From(actorId).WithExpressionInfo(expressionInfo);

        if(IsDead()){
            expressionInfo.SetExpressionType(ExpressionType.Ghost_Idle);
            return result;
        }

        if(other == null){
            if(heartbreakCause == HeartbreakCause.DeathOfLovedOne){
                expressionInfo.SetExpressionType(ExpressionType.Sad_At_Self);
            }
            return result;
        }

        if(other.IsDead()){
            expressionInfo.SetExpressionType(ExpressionType.Shocked_Horrified);
            result.SetEventType(EventType.ShockedBy).To(other.actorId).WithShockCause(ShockCause.SawDeadBody);
            return result;
        }

        if(IsInLoveWith(other)){
            expressionInfo.SetExpressionType(ExpressionType.FallingInLove);
            result.SetEventType(EventType.FallsInLoveWith).To(other.actorId);
            return result;
        }

        
        if(inLoveWith == ActorId.None && other.inLoveWith == ActorId.None){
            inLoveWith = other.actorId;
            other.inLoveWith = actorId;
            expressionInfo.SetExpressionType(ExpressionType.FallingInLove);
            result.SetEventType(EventType.FallsInLoveWith).To(other.actorId);
            return result;
        }

        return result;
        //logic if we can romance
    }

    public Event Die(){
        if(IsDead()){
            return null;
        }

        isDead = true;
        ExpressionInfo expressionInfo = new ExpressionInfo();
        Event result = new Event(EventType.Died).From(actorId).WithDeathCause(DeathCause.Natural).WithExpressionInfo(expressionInfo);
        expressionInfo.SetExpressionType(ExpressionType.TombDeath);
        return result;
    }

    public Event SeeDeath(Actor dead){
        ExpressionInfo expressionInfo = new ExpressionInfo();
        Event result = new Event(EventType.Idling).From(actorId).WithExpressionInfo(expressionInfo);

        if(IsDead()){
            expressionInfo.SetExpressionType(ExpressionType.Ghost_Idle);
            return result;
        }

        if(dead == null){
            expressionInfo.SetExpressionType(ExpressionType.Idling);
            return result;
        }

        if(IsInLoveWith(dead)){
            result = result.WithHearthbreakCause(HeartbreakCause.DeathOfLovedOne);
            expressionInfo.SetExpressionType(ExpressionType.Mourning);
            heartbreakCause = HeartbreakCause.DeathOfLovedOne;
            return result;
        }

        expressionInfo.SetExpressionType(ExpressionType.Ke);
        return result;
    }
}