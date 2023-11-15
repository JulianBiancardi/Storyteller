using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GardenContainer : Container
{
    public GardenContainer otherContainer = null;

    public override void ReceiveDragOperation(Selection selection){
        if(currentCharacterType == selection.character){
            Debug.Log("Same character");
            return;
        }

        //Check if otherContainer has the same character
        if(otherContainer.currentCharacterType == selection.character){
            Debug.Log("Other container has the same character");
            otherContainer.RemoveCharacter();
        }

        GameObject objectToDrop = selection.getObjectToDrop();
        if(objectToDrop != null){
            currentCharacterType = selection.character;
            SpawnCharacter(objectToDrop);
            UpdateCharactersState();
        }
    }

    public override void OnRemove(){
        UpdateCharactersState();
    }

    public void UpdateCharactersState(){
        //TEST VERSION
        if(this.IsEmpty()){
            otherContainer.currentCharacter.GetComponent<Character>().ChangeFeeling(Feeling.Neutral);
            return;
        }
        if(otherContainer.IsEmpty()){
            currentCharacter.GetComponent<Character>().ChangeFeeling(Feeling.Neutral);
        }else if(!otherContainer.IsEmpty()){
            currentCharacter.GetComponent<Character>().ChangeFeeling(Feeling.Love);
            otherContainer.currentCharacter.GetComponent<Character>().ChangeFeeling(Feeling.Love);
        }
    }
}

