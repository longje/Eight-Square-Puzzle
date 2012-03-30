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
    public static class Heuristics
    {
        private static String[][] goalState = { new String[] { "1", "2", "3" }, new String[] { "4", "5", "6" }, new String[] { "7", "8", "0" } };
        public delegate int heuristicToUse(Node node);
        public static heuristicToUse heuristic = manhattanDistanceHeuristicPlusReversalPenalty;

        public static int zeroHeuristic(Node node)
        {
            return 0;
        }

        public static int misplacedTileHeuristic(Node node)
        {
            int tileCount = 0;
            for (int i = 0; i < goalState.Length; i++)
            {
                for (int j = 0; j < goalState[i].Length; j++)
                {
                    if (!node.state.state[i][j].val.Equals(goalState[i][j]) && !node.state.state[i][j].val.Equals("0"))
                    {
                        tileCount++;
                    }
                }
            }
            return tileCount;
        }

        private static int[] determineGoalStateIndex(String v)
        {
            for (int i = 0; i < goalState.Length; i++)
            {
                for (int j = 0; j < goalState[i].Length; j++)
                {
                    if (v.Equals(goalState[i][j]))
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        public static int manhattanDistanceHeuristic(Node node)
        {
            int count = 0;
            int tileCount = 0;
            for (int i = 0; i < node.state.state.Count; i++)
            {
                for (int j = 0; j < node.state.state[i].Count; j++)
                {
                    if (!node.state.state[i][j].val.Equals(goalState[i][j]) && !node.state.state[i][j].val.Equals("0"))
                    {
                        int[] goalStateIndex = determineGoalStateIndex(node.state.state[i][j].val);
                        tileCount += (Math.Abs(goalStateIndex[0] - i)) + (Math.Abs(goalStateIndex[1] - j));
                    }
                    count++;
                }
            }
            return tileCount;
        }

        public static int manhattanDistanceHeuristicPlusReversalPenalty(Node node)
        {
            int tileCount = 0;
            for (int i = 0; i < node.state.state.Count; i++)
            {
                for (int j = 0; j < node.state.state[i].Count; j++)
                {
                    if (!node.state.state[i][j].val.Equals(goalState[i][j]) && !node.state.state[i][j].val.Equals("0"))
                    {
                        int[] goalStateIndex = determineGoalStateIndex(node.state.state[i][j].val);
                        tileCount += (Math.Abs(goalStateIndex[0] - i)) + (Math.Abs(goalStateIndex[1] - j));

                        if (j < 2)
                        {
                            if ((node.state.state[i][j].val.Equals(goalState[i][j + 1]) && node.state.state[i][j + 1].val.Equals(goalState[i][j])))
                            {
                                tileCount = tileCount + 2;
                            }
                        }

                        if (i < 2)
                        {
                            if ((node.state.state[i][j].val.Equals(goalState[i + 1][j]) && node.state.state[i + 1][j].val.Equals(goalState[i][j])))
                            {
                                tileCount = tileCount + 2;
                            }
                        }
                    }
                }
            }
            return tileCount;
        }
    }
}
