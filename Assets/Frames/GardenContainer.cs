using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GardenContainer : Container
{
    public GardenContainer otherContainer = null;

    public void UpdateCharactersState(CompabilityResult result){
        if(actor == null){
            return;
        }

        Feeling newFeeling = result.feelings[actor.GetActorId()];
        currentCharacter.GetComponent<Character>().ChangeFeeling(newFeeling);
    }
}

