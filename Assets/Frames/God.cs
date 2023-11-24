using System.Collections.Generic;
using UnityEngine;

public class God : Frame
{
    public Container container;

    public override List<Event> Compute()
    {
        List<Event> results = new();

        Actor actor = container.GetActor();
        if(actor == null){
            return results;
        }

        ExpressionInfo expressionInfo = new ExpressionInfo();
        Event result = new Event(EventType.None).From(actor.GetActorId());

        if(actor.IsDead()){
            expressionInfo.SetExpressionType(ExpressionType.Ghost_Idle);
            result = result.SetEventType(EventType.Idling).WithExpressionInfo(expressionInfo);
        }
        else if(actor.IsLonely()){
            if(actor.NeedsLove()){
                expressionInfo.SetExpressionType(ExpressionType.Sad_At_Self);
                result = result.SetEventType(EventType.Sad_At_Self).WithExpressionInfo(expressionInfo);
            }else{
                expressionInfo.SetExpressionType(ExpressionType.Idling);
                result = result.SetEventType(EventType.Idling).WithExpressionInfo(expressionInfo);
            }
        }else {
            expressionInfo.SetExpressionType(ExpressionType.Idling);
            result = result.SetEventType(EventType.Idling).WithExpressionInfo(expressionInfo);
        }

        results.Add(result);
        container.UpdateCharacter(result);
        return results;
    }
}
