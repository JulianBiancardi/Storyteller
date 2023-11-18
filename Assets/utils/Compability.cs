using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public struct CompabilityResult
{
    public bool isMatch;
    public Dictionary<ActorId, Feeling> feelings;
}

public class Compability
{
    
    public static CompabilityResult Match(Actor me, Actor other){
        CompabilityResult result = new()
        {
            isMatch = false,
            feelings = new Dictionary<ActorId, Feeling>()
        };


        if(me == null){
            result.feelings[other.GetActorId()] = Feeling.Neutral;
        }else if(other == null) {
            result.feelings[me.GetActorId()] = Feeling.Neutral;
        }else{
            result.isMatch = me.ComputeRomance(other);
            if(result.isMatch){
                result.feelings[me.GetActorId()] = Feeling.Love;
                result.feelings[other.GetActorId()] = Feeling.Love;
            }else{
                result.feelings[me.GetActorId()] = Feeling.Neutral;
                result.feelings[other.GetActorId()] = Feeling.Neutral;
            }
        }
        return result;
    }
}
