using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Garden : Frame
{
    public GardenContainer gardenContainerLeft;
    public GardenContainer gardenContainerRight;
    public GameObject expression;

    void Start()
    {
    }

    public override void Compute(){
        if(gardenContainerLeft.IsEmpty() && gardenContainerRight.IsEmpty()){
            return;
        }

        Actor actorLeft = gardenContainerLeft.GetActor();
        Actor actorRight = gardenContainerRight.GetActor();

        CompabilityResult result = Compability.Match(actorLeft, actorRight);

        expression.SetActive(result.isMatch);
        gardenContainerLeft.UpdateCharactersState(result);
        gardenContainerRight.UpdateCharactersState(result);
    }
}