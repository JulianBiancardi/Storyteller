using UnityEngine;

public class Cementery : Frame
{
    public CementeryContainer leftContainer;
    public Container tombContainer;

    public override void Compute()
    {
        DeathResult result = Death(tombContainer.GetActor(), leftContainer.GetActor());
        leftContainer.UpdateCharactersState(result);
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
