using System.Collections.Generic;
using UnityEngine;
public class GoalsEvaluator
{
    private bool EvaluateOrdered(List<Event> frameResults, List<Event> expectedResults){
        List<int> goalsIndexes = new();
        for(int i = 0; i < expectedResults.Count; i++){
            int index = frameResults.FindIndex(0, (Event result) => result.SameAs(expectedResults[i]));
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

    public bool EvaluateGoals(List<Event> frameResults, LevelId levelId){
        return levelId switch
        {
            LevelId.Love => EvaluateLevel1(frameResults),
            LevelId.Hearthbreak => EvaluateLevel2(frameResults),
            LevelId.Afterlife => EvaluateLevel3(frameResults),
            _ => false,
        };
    }
    
    public bool EvaluateLevel1(List<Event> frameResults){
        Event first = new Event(EventType.Sad_At_Self).From(ActorId.Adam);
        Event second = new Event(EventType.FallsInLoveWith).From(ActorId.Adam).To(ActorId.Eve);
        Event third = new Event(EventType.Died).From(ActorId.Adam).WithDeathCause(DeathCause.Natural);

        List<Event> expectedResults = new(){first, second, third};

        return EvaluateOrdered(frameResults, expectedResults);
    } 

    public bool EvaluateLevel2(List<Event> frameResults){
        Event first = new Event(EventType.Idling).From(ActorId.Eve).WithHearthbreakCause(HearthbreakCause.DeathOfLovedOne);
        Event second = new Event(EventType.Died).From(ActorId.Eve).WithDeathCause(DeathCause.Natural);

        List<Event> expectedResults = new(){first, second};

        return EvaluateOrdered(frameResults, expectedResults);
    }

    public bool EvaluateLevel3(List<Event> frameResults){
        //int index = frameResults.FindIndex(0, (Event result) =>  result.hearthbreakCause == HearthbreakCause.DeathOfLovedOne && result.shockCause == ShockCause.SawDeadBody);
        return false;
    }
}
