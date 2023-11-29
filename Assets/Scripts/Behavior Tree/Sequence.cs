using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    
    public Sequence(): base() { }

    public Sequence(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach(Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:

                    state = NodeState.Failure;
                    return state;

                case NodeState.Sucess:

                    continue;

                case NodeState.Running:

                    anyChildIsRunning = true;
                    continue;

                default:

                    state = NodeState.Sucess;
                    return state;
            }
        }
        state = anyChildIsRunning ? NodeState.Running : NodeState.Sucess;
        return state;
    }


}
