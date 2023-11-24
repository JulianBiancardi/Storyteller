using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Garden : Frame
{
    public Container gardenContainerLeft;
    public Container gardenContainerRight;
    public GameObject expression;
    private AudioSource audioSource;
    private bool beforeMatch = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        }

        if(resultRight != null){
            results.Add(resultRight);
        }

        return results;
    }
}