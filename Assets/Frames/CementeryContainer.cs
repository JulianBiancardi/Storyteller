using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class CementeryContainer : Container
{
    public void UpdateCharactersState(DeathResult result){
        if(actor == null){
            return;
        }

        Feeling newFeeling = result.witnessFeeling;
        currentCharacter.GetComponent<Character>().ChangeFeeling(newFeeling);
    }

    public void DeathCharacter(){
        if(actor == null){
            return;
        }
        currentCharacter.GetComponent<Character>().Death();
    }
}
