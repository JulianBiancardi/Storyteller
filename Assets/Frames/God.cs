using UnityEngine;

public class God : Frame
{
    public Container container;

    public override FrameResult Compute()
    {
        Actor actor = container.GetActor();
        if(actor == null){
            return new FrameResult();
        }

        Feeling newFeeling;
        if(actor.IsLonely()){
            newFeeling = actor.NeedsLove() ? Feeling.Lonely : Feeling.Neutral;
        }else {
            newFeeling = Feeling.Neutral;
        }
        
        container.ChangeCharacterState(newFeeling);
        return new FrameResult(container.GetActor().GetActorId(), newFeeling ,false);
    }
}
