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

namespace EightPuzzle
{
    public static class GoalValue
    {
        public delegate int goalValueToUse(Node node);
        public static goalValueToUse value = goalValue;

        public static int goalValue(Node node)
        {
            return node.parent.gValue + 1;
        }

        public static int zeroGoalValue(Node node)
        {
            return 0;
        }
    }
}
