using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EightPuzzle
{
	public partial class ResultsWindow : ChildWindow
	{
        private Stats stat;
        private EightSquares eightPuzzle;

        public ResultsWindow(Stats stat, List<Node> results, EightSquares eightPuzzle, int expanded, int total)
		{
			InitializeComponent();
            this.eightPuzzle = eightPuzzle;
            this.stat = stat;
            expandedCount.Text += expanded;
            genCount.Text += total;
            movesMade.Text += results.Last().gValue;
            avgBranchingFactor.Text += Math.Round((Double)total / expanded, 3);
            Double temp = (double)1 / (double)results.Last().gValue;
            effBranchingFactor.Text += Math.Round(Math.Pow(expanded, temp), 3);
            List<Data> source = new List<Data>();
            foreach (Node n in results)
            {
                source.Add(new Data()
                {
                    hValue = n.hValue,
                    gValue = n.gValue,
                    fValue = n.fValue,
                    action = n.action,
                    state = stringifyState(n),
                    thisNode = n
                });

                actionsToGoal.Text += (n.action != String.Empty && n.gValue != results.Count - 1) ? String.Format("{0}, ", n.action) : n.action; 
            }
            grid.ItemsSource = source;
            //grid.DataContext = results[0];
		}

        private void setState(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            var game = button.DataContext as Data;
            eightPuzzle.setCurrentNode(game.thisNode);
            eightPuzzle.updateStats();
            stat.resetStats();
        }

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

        private String stringifyState(Node node)
        {
            String myState = "";
            int count = 1;
            foreach (List<Square> s in node.state.state)
            {
                foreach (Square sq in s)
                {
                    myState += sq.val + " ";
                    if (count == 3)
                    {
                        myState += "\r";
                        count = 0;
                    }
                    count++;
                }
            }
            return myState;
        }

	}



    public class Data
    {
        public int rowNum { get; set; }
        public int hValue {get; set;}
        public int gValue {get; set;}
        public int fValue {get; set;}
        public String action {get; set;}
        public String state { get; set; }
        public Node thisNode { get; set; }
    }
}

