public class Actor
{
    private ActorId actorId;
    protected ActorId inLoveWith;
    private bool needsLove;

    public Actor(ActorId actorId, bool needsLove){
        this.actorId = actorId;
        this.inLoveWith = ActorId.None;
        this.needsLove = needsLove;
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