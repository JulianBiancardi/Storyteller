using System.Collections.Generic;
using UnityEngine;

public class Garden : Frame
{
    public Container gardenContainerLeft;
    public Container gardenContainerRight;
    public GameObject expression;
    private bool beforeMatch = false;

    void Start()
    {
        gardenContainerLeft.connectedContainers.Add(gardenContainerRight);
        gardenContainerRight.connectedContainers.Add(gardenContainerLeft);
    }

    public override List<Event> Compute(){
        List<Event> results = new();

        if(gardenContainerLeft.IsEmpty() && gardenContainerRight.IsEmpty()){
            return results;
        }

        Actor actorLeft = gardenContainerLeft.GetActor();
        Actor actorRight = gardenContainerRight.GetActor();

        Event resultLeft = actorLeft?.Romance(actorRight);
        Event resultRight = actorRight?.Romance(actorLeft);

        gardenContainerLeft.UpdateCharacter(resultLeft);
        gardenContainerRight.UpdateCharacter(resultRight);

        if(resultLeft != null){
            results.Add(resultLeft);
            CheckMatch(resultLeft);
        }

        if(resultRight != null){
            results.Add(resultRight);
            CheckMatch(resultRight);
        }

        return results;
    }

    private void CheckMatch(Event e){
        if(e.eventType != EventType.FallsInLoveWith){
            beforeMatch = false;
            expression.SetActive(false);
            return;
        }

        if(e.eventType == EventType.FallsInLoveWith && beforeMatch){
            return;
        }

        beforeMatch = true;
        expression.SetActive(true);
    }
}