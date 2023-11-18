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

    public void AddCharacter(Container container, Character character){
        this.character = character;
    }

    public void RemoveCharacter(){
        character = null;
    } 
}
