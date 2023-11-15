using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class God : Frame
{
    private Character character = null;

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
        this.character = character;
    } 
}
