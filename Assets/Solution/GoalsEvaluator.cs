using System.Collections.Generic;
using UnityEngine;
public class GoalsEvaluator
{
    private bool EvaluateOrdered(List<FrameResult> frameResults, List<FrameResult> expectedResults){
        List<int> goalsIndexes = new();
        for(int i = 0; i < expectedResults.Count; i++){
            int index = frameResults.FindIndex(0, (FrameResult result) => result.SameAs(expectedResults[i]));
            goalsIndexes.Add(index);
        }

        for(int i = 0; i < goalsIndexes.Count; i++){
            int current = goalsIndexes[i];

            if(current == -1){
                Debug.Log("Missing goal");
                return false;
            }

            if(i + 1 == goalsIndexes.Count){
                continue;
            }

            if(current > goalsIndexes[i + 1]){
                Debug.Log("Wrong order");
                return false;
            }
        }

        Debug.Log("Solution is correct");
        return true;
    }

    public bool EvaluateGoals(List<FrameResult> frameResults, LevelId levelId){
        return levelId switch
        {
            LevelId.Love => EvaluateLevel1(frameResults),
            LevelId.Hearthbreak => EvaluateLevel2(frameResults),
            _ => false,
        };
    }
    
    public bool EvaluateLevel1(List<FrameResult> frameResults){
        FrameResult first = new FrameResult(EventType.Sad_At_Self).From(ActorId.Adam);
        FrameResult second = new FrameResult(EventType.FallsOutOfLoveWith).From(ActorId.Adam).To(ActorId.Eve);
        FrameResult third = new FrameResult(EventType.Died).From(ActorId.Adam).WithDeathCause(DeathCause.Natural);

        List<FrameResult> expectedResults = new(){first, second, third};

        return EvaluateOrdered(frameResults, expectedResults);
    } 

    public bool EvaluateLevel2(List<FrameResult> frameResults){
        FrameResult first = new FrameResult(EventType.Idling).From(ActorId.Eve).WithHearthbreakCause(HearthbreakCause.DeathOfLovedOne);
        FrameResult second = new FrameResult(EventType.Died).From(ActorId.Eve).WithDeathCause(DeathCause.Natural);

        List<FrameResult> expectedResults = new(){first, second};

        return EvaluateOrdered(frameResults, expectedResults);
    }
}
