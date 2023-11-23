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

    public override List<FrameResult> Compute(){
        List<FrameResult> results = new();

        if(gardenContainerLeft.IsEmpty() && gardenContainerRight.IsEmpty()){
            return results;
        }

        Actor actorLeft = gardenContainerLeft.GetActor();
        Actor actorRight = gardenContainerRight.GetActor();

        CompabilityResult result = Compability.Match(actorLeft, actorRight);

        expression.SetActive(result.isMatch);
        if(!beforeMatch && result.isMatch){
            audioSource.clip = Resources.Load<AudioClip>("Sound/Sfx/thumbs_up_in_love");
            audioSource.Play();
        }

        if(actorLeft != null)
            gardenContainerLeft.ChangeCharacterState(result.feelings[actorLeft.GetActorId()]);
        
        if(actorRight != null)
            gardenContainerRight.ChangeCharacterState(result.feelings[actorRight.GetActorId()]);

        beforeMatch = result.isMatch;

        if(result.isMatch){
            results.Add(new FrameResult(EventType.FallsOutOfLoveWith).From(actorLeft.GetActorId()).To(actorRight.GetActorId()));
            results.Add(new FrameResult(EventType.FallsOutOfLoveWith).From(actorRight.GetActorId()).To(actorLeft.GetActorId()));
        }

        return results;
    }
}