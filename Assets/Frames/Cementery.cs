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

    public override List<Event> Compute()
    {
        List<Event> results = new();
        Actor witness = leftContainer.GetActor();
        Actor dead = tombContainer.GetActor();

        Event deadResult = dead?.Die();
        tombContainer.UpdateCharacter(deadResult);

        Event witnessResult = witness?.SeeDeath(dead);
        leftContainer.UpdateCharacter(witnessResult);
        
        results.Add(deadResult);
        results.Add(witnessResult);
        return results;
    }


    private DeathResult Death(Actor dead, Actor witness){
        DeathResult result = new()
        {
            witnessFeeling = Feeling.Neutral
        };

        if(dead != null){
            dead.Die();
        }

        if (witness == null || dead == null){
            return result;
        }

        if(witness.IsInLoveWith(dead)){
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
