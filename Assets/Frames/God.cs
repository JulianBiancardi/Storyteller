using System.Collections.Generic;
using UnityEngine;

public class God : Frame
{
    public Container container;

    public override List<FrameResult> Compute()
    {
        List<FrameResult> results = new();

        Actor actor = container.GetActor();
        if(actor == null){
            return results;
        }

        Feeling newFeeling;
        if(actor.IsLonely()){
            newFeeling = actor.NeedsLove() ? Feeling.Lonely : Feeling.Neutral;
        }else {
            newFeeling = Feeling.Neutral;
        }
        
        container.ChangeCharacterState(newFeeling);
        FrameResult result = new FrameResult((newFeeling == Feeling.Lonely) ? EventType.Sad_At_Self : EventType.Idling).From(actor.GetActorId());
        results.Add(result);
        return results;
    }
}
