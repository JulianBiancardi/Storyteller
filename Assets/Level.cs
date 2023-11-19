using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    public string levelName;
    public string levelDescription;
    public int numberOfFrames;
    public List<FrameSet> sets = new ();
    private List<Actor> actors = new ();
    private List<Frame> frames = new ();
    public GameObject selectableCharacterPrefab;
    public GameObject framePrefab;
    public Solution solution;
    public GameObject goalCheckmark;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    void Start()
    {
        actors.Add(new Actor(ActorId.Adam, true));
        actors.Add(new Actor(ActorId.Eve, false));
        CrateToolBox();
        CreateFrames();
    }

    public void CrateToolBox(){
        GameObject toolBox = this.transform.Find("ToolBox").gameObject;
        int currentCharacter = 0;
        int spacing = 2;
        foreach(Actor actor in actors){
            GameObject selectableCharacter = SelectableFactory.CreateSelectable(actor);
            selectableCharacter.transform.parent = toolBox.transform;
            selectableCharacter.transform.localPosition = new Vector3(currentCharacter * spacing, 0, 0);
            currentCharacter++;
        }
    }

    public void CreateFrames(){
        GameObject frames = this.transform.Find("Frames").gameObject;
        int index = 0;
        int spacing = 5;
        foreach(FrameSet set in sets){
            GameObject frame = FrameFactory.CreateFrame(set);
            frame.transform.parent = frames.transform;
            frame.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            float frameWidth = frame.GetComponent<SpriteRenderer>().bounds.size.x;

            frame.transform.localPosition = new Vector3((index + (1f / 2f)) * frameWidth , 0, 0);
            this.frames.Add(frame.GetComponent<Frame>());
            index++;
        }
    }

    public void ComputeAll(){
        List<FrameResult> frameResults = new();

        foreach(Actor actor in actors){
            actor.Reset();
        }
        foreach(Frame frame in frames){
            frameResults.Add(frame.Compute());
        }

        solution.CheckSolution(frameResults);
    }
}

[Serializable]
public class Solution {
    public List<FrameResult> goals = new();

    public void CheckSolution(List<FrameResult> frameResults){

        List<int> goalsIndexes = new();
        for(int i = 0; i < goals.Count; i++){
            int index = frameResults.FindIndex(0, (FrameResult result) => result.SameAs(goals[i]));
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

[Serializable]
public struct FrameResult{
    public ActorId actorId;
    public Feeling feeling;
    public bool isDead;
    public RomanceResult romanceResult;

    public FrameResult(ActorId actorId, Feeling feeling, bool isDead){
        this.actorId = actorId;
        this.feeling = feeling;
        this.isDead = isDead;
        this.romanceResult = new RomanceResult();
    }

    public bool SameAs(FrameResult other){
        return actorId == other.actorId && feeling == other.feeling && isDead == other.isDead && romanceResult.SameAs(other.romanceResult);
    }

    public FrameResult WithRomance(ActorId firstActor, ActorId secondActor){
        this.romanceResult = new RomanceResult{
            firstActor = firstActor,
            secondActor = secondActor
        };
        return this;
    }
}

[Serializable]
public struct RomanceResult{
    public ActorId firstActor;
    public ActorId secondActor;

    public bool SameAs(RomanceResult other){
        return (firstActor == other.firstActor && secondActor == other.secondActor) || (firstActor == other.secondActor && secondActor == other.firstActor);
    }
}
