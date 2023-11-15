using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //create a static singleton patter
    public static Level Instance { get; private set; }

    public string levelName;
    public string levelDescription;
    public int numberOfFrames;
    public List<CharacterType> characters = new ();
    public List<FrameSet> sets = new ();
    public GameObject selectableCharacterPrefab;
    public GameObject framePrefab;
    public Solution solution;
    public LevelContext context = new LevelContext();

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
        CrateToolBox();
        CreateFrames();
    }

    public void CrateToolBox(){
        GameObject toolBox = this.transform.Find("ToolBox").gameObject;
        int currentCharacter = 0;
        int spacing = 2;
        foreach(CharacterType character in characters){
            GameObject selectableCharacter = SelectableFactory.CreateSelectable(character);
            selectableCharacter.transform.parent = toolBox.transform;
            selectableCharacter.GetComponent<CharacterSelectable>().character = character;
            selectableCharacter.transform.localPosition = new Vector3(currentCharacter * spacing, 0, 0);
            currentCharacter++;
        }
    }

    public void CreateFrames(){
        GameObject frames = this.transform.Find("Frames").gameObject;
        int currentFrame = 0;
        int spacing = 5;
        foreach(FrameSet set in sets){
            GameObject frame = FrameFactory.CreateFrame(set);
            frame.transform.parent = frames.transform;
            frame.transform.localPosition = new Vector3(currentFrame * spacing, 0, 0);
            currentFrame++;
        }
    }

    public void AddOperation(CharacterType character, Emotion emotion){
        context.AddOperation(character, emotion);
        solution.CheckSolution(context);
    }
}

public class LevelContext{
    //List of tuple of character and emotion
    public List<Tuple<CharacterType, Emotion>> operations = new List<Tuple<CharacterType, Emotion>>();

    public void AddOperation(CharacterType character, Emotion emotion){
        operations.Add(new Tuple<CharacterType, Emotion>(character, emotion));
    }
}

[Serializable]
public class Solution {
    public CharacterType character;
    public Emotion emotion;


    public Solution(CharacterType character, Emotion emotion){
        this.character = character;
        this.emotion = emotion;
    }

    public void CheckSolution(LevelContext context){
        foreach(Tuple<CharacterType, Emotion> operation in context.operations){
            if(operation.Item1 == character && operation.Item2 == emotion){
                Debug.Log("Correct");
            }else{
                Debug.Log("Incorrect");
            }
        }
    }
}
