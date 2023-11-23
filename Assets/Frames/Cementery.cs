using System.Collections.Generic;
using UnityEngine;

public class Cementery : Frame
{
    public CementeryContainer leftContainer;
    public CementeryContainer tombContainer;

    void Start()
    {
        leftContainer.connectedContainers.Add(tombContainer);
        tombContainer.connectedContainers.Add(leftContainer);
    }

    public override List<FrameResult> Compute()
    {
        List<FrameResult> results = new();
        Actor witness = leftContainer.GetActor();
        Actor dead = tombContainer.GetActor();

        DeathResult result = Death(dead, witness);
        leftContainer.UpdateCharactersState(result);
        tombContainer.DeathCharacter();

        if(dead != null){
            results.Add(new FrameResult(EventType.Died).From(dead.GetActorId()).WithDeathCause(DeathCause.Natural));

            if(witness != null){
                if(witness.IsInLoveWith(dead)){
                    results.Add(new FrameResult(EventType.Idling).From(witness.GetActorId()).WithHearthbreakCause(HearthbreakCause.DeathOfLovedOne));
                }
            }
        }

        return results;
    }


    private DeathResult Death(Actor actor, Actor witness){
        DeathResult result = new()
        {
            witnessFeeling = Feeling.Neutral
        };

        if (witness == null || actor == null){
            return result;
        }

        if(witness.IsInLoveWith(actor)){
            result.witnessFeeling = Feeling.SadTomb;
        } else {
            result.witnessFeeling = Feeling.Neutral;
        }

        return result;
    }
}

public struct DeathResult {
    public Feeling witnessFeeling;
}
