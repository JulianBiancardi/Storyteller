using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public LevelSolution solution;
    private GoalsEvaluator goalsEvaluator = new();
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
        List<Event> frameResults = new();

        foreach(Actor actor in actors){
            actor.Reset();
        }
        foreach(FrameHolder frameHolder in frameHolders){
            frameResults.AddRange(frameHolder.GetFrameResults());
        }

        bool completed = goalsEvaluator.EvaluateGoals(frameResults, levelSpec.levelId);
        if(completed){
            goalCheckmark.SetActive(true);
        }else{
            goalCheckmark.SetActive(false);
        }
    }
}