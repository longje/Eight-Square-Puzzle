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
    public class AStar
    {
        public List<Node>[] exploredSet = new List<Node>[30];
        public List<Node> allNodesList = new List<Node>();
        public int nodeCount = 0;

        public AStar()
        {
            for (int i = 0; i < exploredSet.Length; i++)
            {
                exploredSet[i] = new List<Node>();

            }
        }

        private void clearExploredSet()
        {
            for (int i = 0; i < exploredSet.Length; i++)
            {
                exploredSet[i] = new List<Node>();

            }
        }

        public Node aStarSearch(List<Node> parentNode)
        {
            //exploredSet = new List<Node>();
            allNodesList.Clear();
            clearExploredSet();
            while (true)
            {
                if (isGoalState(parentNode[0].state))
                {
                    return parentNode[0];
                }
                else if (parentNode[0].state != null)
                {

                    List<Node> temp = parentNode[0].generateChildNodes();
                    exploredSet[parentNode[0].hValue].Add(parentNode[0]);

                    foreach (Node n in temp)
                    {
                        allNodesList.Add(n);
                        if (!(isItInSet(n, exploredSet[n.hValue])))
                        {
                            parentNode.Add(n);
                            nodeCount++;
                        }
                    }

                    parentNode.Remove(parentNode[0]);
                    parentNode.Sort(CustomCompare);

                    Console.Write("\r{0}", parentNode.Count);

                }
            }

            //return null;
        }

        public Boolean isGoalState(State values)
        {
            if (values != null)
            {
                String[] goalState = { "1", "2", "3", "4", "5", "6", "7", "8", "0" };
                int count = 0;
                foreach (List<Square> list in values.state)
                {
                    foreach (Square block in list)
                    {
                        if (!block.val.Equals(goalState[count]))
                        {
                            return false;
                        }
                        count++;
                    }
                }
                return true;
            }
            return false;
        }

        private Boolean isItInSet(Node node, List<Node> set)
        {
            if (set.Count == 0)
                return false;
            foreach (Node n in set)
            {
                if (nodeCompare(n, node))
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean nodeCompare(Node node, Node node1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (node.state.state[i][j].val != node1.state.state[i][j].val)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int CustomCompare(Node one, Node two)
        {
            return one.fValue.CompareTo(two.fValue);
        }
    }
}
