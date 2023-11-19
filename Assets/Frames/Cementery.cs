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

    public override FrameResult Compute()
    {
        Actor witness = leftContainer.GetActor();
        Actor dead = tombContainer.GetActor();

        DeathResult result = Death(dead, witness);
        leftContainer.UpdateCharactersState(result);
        tombContainer.DeathCharacter();
        return new FrameResult(dead != null ? dead.GetActorId() : ActorId.None, Feeling.Neutral, dead != null);
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
