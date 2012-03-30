using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace EightPuzzle
{
    public class Node
    {
        public State state;
        public Node parent;
        public String action;
        public int gValue, hValue, fValue;

        public Node(State state)
        {
            this.state = state;
            this.parent = null;
            this.action = null;
            gValue = GoalValue.zeroGoalValue(this);
            hValue = (state != null) ? Heuristics.heuristic(this) : 0;
            fValue = gValue + hValue;
        }

        public void setAsRootNode()
        {
            this.parent = null;
            this.action = "";
            this.gValue = 0;
        }

        public Node(State state, Node parent, String action)
        {
            this.state = state;
            this.parent = parent;
            this.action = action;
            gValue = GoalValue.value(this);
            hValue = (state != null) ? Heuristics.heuristic(this) : 0;
            fValue = gValue + hValue;
        }

        public List<Node> generateChildNodes()
        {
            List<Node> childrenNodes = new List<Node>();
            List<State> children = this.state.generateChildren();
            foreach (State state in children)
            {
                if (state != null)
                {
                    childrenNodes.Add(new Node(state, this, state.action));
                }
            }
            return childrenNodes;
        }

        
    }
}