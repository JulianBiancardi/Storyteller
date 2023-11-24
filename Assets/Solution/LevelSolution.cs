using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelSolution", menuName = "")]
public class LevelSolution : ScriptableObject
{
    public List<Event> goals = new List<Event>();

    public void CheckSolution(List<Event> frameResults){

        List<int> goalsIndexes = new();
        for(int i = 0; i < goals.Count; i++){
            int index = frameResults.FindIndex(0, (Event result) => result.SameAs(goals[i]));
            goalsIndexes.Add(index);
        }

        for(int i = 0; i < goalsIndexes.Count; i++){
            int current = goalsIndexes[i];

            if(current == -1){
                Debug.Log("Missing goal");
                Level.Instance.goalCheckmark.SetActive(false);
                return;
            }

            if(i + 1 == goalsIndexes.Count){
                continue;
            }

            if(current > goalsIndexes[i + 1]){
                Debug.Log("Wrong order");
                Level.Instance.goalCheckmark.SetActive(false);
                return;
            }
        }

        Debug.Log("Solution is correct");
        Level.Instance.goalCheckmark.SetActive(true);
    }
}
