public class Actor
{
    private ActorId actorId;
    protected ActorId inLoveWith;

    public Actor(ActorId actorId){
        this.actorId = actorId;
        this.inLoveWith = ActorId.None;
    }

    public ActorId GetActorId(){
        return actorId;
    }
    
    public bool IsInLoveWith(Actor actor){
        return inLoveWith == actor.actorId;
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

    public void Reset() {
        inLoveWith = ActorId.None;
    }
}