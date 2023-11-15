using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class God : Frame
{
    public List<Container> containers;
    private Character character = null;
    private Dictionary<CharacterType, Container> containers2 = new();

    void Start()
    {
    }

    public override void ReceiveDragOperation(GameObject dragObject, Container container){
        Selection selection = dragObject.GetComponent<Selection>();
        if(selection == null) {
            Debug.Log("Selection is null");
            return;
        }
    }

    public void AddCharacter(Container container, Character character){
        //if(!containers.ContainsKey(character ))
        this.character = character;
    }

    public void RemoveCharacter(){
        character = null;
    } 
}
