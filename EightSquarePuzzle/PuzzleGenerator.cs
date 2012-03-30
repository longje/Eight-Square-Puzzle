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

using System.IO;
using System.Collections.Generic;  //List

namespace EightPuzzle
{

	public class PuzzleGenerator
	{
        public List<Node>[] exploredSet = new List<Node>[30];

        public PuzzleGenerator()
        {
            //Initialize explored set
            for (int i = 0; i < exploredSet.Length; i++)
            {
                exploredSet[i] = new List<Node>();

            }
            //parentNode = randomPuzzle(10);
        }

        private List<Node> randomPermute(List<Node> list)
        {
            Random random = new Random();
            int randomAssignment = random.Next(0, list.Count - 1);
            Node temp = list[randomAssignment];
            list.RemoveAt(randomAssignment);
            list.Insert(0, temp);
            return list;

        }

        private Node giveMeAGoalNode()
        {
            String[][] goalState = { new String[] { "1", "2", "3" }, new String[] { "4", "5", "6" }, new String[] { "7", "8", "0" } };

            List<List<Square>> sq = new List<List<Square>>();

            for (int j = 0; j < goalState.Length; j++)
            {
                sq.Add(new List<Square>());
                for (int k = 0; k < 3; k++)
                {
                    sq[j].Add(new Square(j, k, goalState[j][k]));
                }
            }

            return new Node(new State(sq, ""));
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

        public Node randomPuzzle(int distance)
        {
            List<Node> listOfNodes = new List<Node>();
            listOfNodes.Add(giveMeAGoalNode());

            for (int i = 0; i <= distance; i++)
            {
                List<List<Node>> children = new List<List<Node>>();
                foreach (Node n in randomPermute(listOfNodes))
                {
                    children.Add(n.generateChildNodes());
                }

                listOfNodes.Clear();

                foreach (List<Node> list in children)
                {
                    foreach (Node node in list)
                    {
                        if (!(isGoalState(node.state)) && !(isItInSet(node, exploredSet[node.hValue])))
                        {
                            if (Heuristics.manhattanDistanceHeuristicPlusReversalPenalty(node) >= distance)
                            {
                                node.parent = null;
                                node.gValue = 0;
                                node.fValue = node.hValue;
                                node.action = "";
                                return node;
                            }
                            exploredSet[node.hValue].Add(node);
                            listOfNodes.Add(node);
                        }
                    }
                }
            }
            Random rand = new Random();
            return listOfNodes[rand.Next(0, listOfNodes.Count - 1)];
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





          private void printChild(State st)
          {
              if (st != null)
              {
                  foreach (List<Square> list in st.state)
                  {
                      foreach (Square block in list)
                      {
                          Console.Write(block.val + " ");
                      }
                  }
                  Console.WriteLine();
              }
          }


    }
}

