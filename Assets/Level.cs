using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    public LevelSpec levelSpec;
    private List<Actor> actors = new ();
    private List<FrameHolder> frameHolders = new ();
    public GameObject toolItemPrefab;
    public GameObject characterPrefab;
    public GameObject frameHolderPrefab;
    public float frameSpacing;
    public Solution solution;
    public TextMeshPro goalText;
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
        goalText.text = levelSpec.goal;
        foreach(ActorId actorId in levelSpec.actors){
           actors.Add(new Actor(actorId, true));
        }
        CrateToolBox();
        CreateFrameHolders();
    }

    public void CrateToolBox(){
        GameObject toolBox = this.transform.Find("ToolBox").gameObject;
        int currentCharacter = 0;
        int spacing = levelSpec.toolSpacing;

        foreach(FrameSet set in levelSpec.sets){
            GameObject toolItem = SelectableFactory.CreateSetToolItem(set);
            toolItem.transform.parent = toolBox.transform;
            toolItem.transform.localPosition = new Vector3(currentCharacter * spacing, 0, 0);
            currentCharacter++;
        }

        foreach(Actor actor in actors){
            GameObject selectableCharacter = SelectableFactory.CreateActorToolItem(actor);
            selectableCharacter.transform.parent = toolBox.transform;
            selectableCharacter.transform.localPosition = new Vector3(currentCharacter * spacing, 0, 0);
            currentCharacter++;
        }

        int items = levelSpec.sets.Count + levelSpec.actors.Count;
        toolBox.transform.localPosition += new Vector3(- 1 * (items - 1) , 0, 0);
    }

    public void CreateFrameHolders(){
        GameObject frames = this.transform.Find("Frames").gameObject;
        for(int i = 0; i < levelSpec.frames; i++){
            GameObject frameHolder = GameObject.Instantiate(frameHolderPrefab);
            float frameWidth = frameHolder.GetComponent<SpriteRenderer>().bounds.size.x;
            frameHolder.transform.parent = frames.transform;
            frameHolder.transform.localPosition = new Vector3(i * frameSpacing, 0, 0);
            frameHolder.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            this.frameHolders.Add(frameHolder.GetComponent<FrameHolder>());
        }
    }

    public void ComputeAll(){
        List<FrameResult> frameResults = new();

        foreach(Actor actor in actors){
            actor.Reset();
        }
        foreach(FrameHolder frameHolder in frameHolders){
            frameResults.Add(frameHolder.GetFrameResult());
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
