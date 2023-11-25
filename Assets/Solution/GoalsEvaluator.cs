using System.Collections.Generic;
using UnityEngine;
public class GoalsEvaluator
{
    private bool EvaluateOrdered(List<Event> frameResults, List<Event> expectedResults){
        List<int> goalsIndexes = new();
        for(int i = 0; i < expectedResults.Count; i++){
            int index = frameResults.FindIndex(0, (Event result) => {
                if(result == null){
                    return false;
                }
                return result.SameAs(expectedResults[i]);
            });
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
            LevelId.Heartbreak => EvaluateLevel2(frameResults),
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
        Event first = new Event(EventType.Idling).From(ActorId.Eve).WithHearthbreakCause(HeartbreakCause.DeathOfLovedOne);
        Event second = new Event(EventType.Died).From(ActorId.Eve).WithDeathCause(DeathCause.Natural);

        List<Event> expectedResults = new(){first, second};

        return EvaluateOrdered(frameResults, expectedResults);
    }

    public bool EvaluateLevel3(List<Event> frameResults){
        int fisrtGoal = frameResults.FindIndex(0, (Event result) =>  {
            if(result == null){
                return false;
            }

            return result.heartbreakCause == HeartbreakCause.DeathOfLovedOne;
        });

        if(fisrtGoal == -1){
            Debug.Log("Missing goal");
            return false;
        }

        ActorId actor = frameResults[fisrtGoal].source;
        Debug.Log(actor);
        int secondGoal = frameResults.FindIndex(0, (Event result) =>  {
            if(result == null){
                return false;
            }

            return result.source == actor && result.eventType == EventType.ShockedBy && result.shockCause == ShockCause.SawDeadBody;
        });

        if(secondGoal == -1){
            Debug.Log("Missing second goal");
            return false;
        }
        return true;
    }
}
