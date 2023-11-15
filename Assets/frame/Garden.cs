using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Garden : Frame
{
    private readonly List<Character> characters = new();

    void Start()
    {
    }

    public override void ReceiveDragOperation(GameObject dragObject, Container container){
        Selection selection = dragObject.GetComponent<Selection>();
        if(selection == null) {
            Debug.Log("Selection is null");
            return;
        }
        ReceiveCharacter(container.ReceiveSelection(selection));
    }

    public void ReceiveCharacter(Character character){
        if(character == null){
            Debug.Log("Character is null");
            return;
        }
        characters.Add(character);
        if(characters.Count == 1){
            character.ChangeFeeling(Feeling.Sad);
        }
    }
}