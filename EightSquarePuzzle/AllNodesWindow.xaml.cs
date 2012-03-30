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
using System.Windows.Threading;

namespace EightPuzzle
{
    public partial class AllNodesWindow : ChildWindow
    {
        public AllNodesWindow()
        {
            InitializeComponent();
        }

        public AllNodesWindow(List<Node> results, int expanded, int total)
		{
			InitializeComponent();
            expandedCount.Text += expanded.ToString();
            genCount.Text += total.ToString();
            List<Data> source = new List<Data>();
            int count = 0;
            foreach (Node n in results)
            {
                source.Add(new Data()
                {
                    rowNum = ++count,
                    hValue = n.hValue,
                    gValue = n.gValue,
                    fValue = n.fValue,
                    action = n.action,
                    state = stringifyState(n),
                    thisNode = n
                });
                    
            }
            grid.ItemsSource = source.Reverse<Data>();
            //grid.DataContext = results[0];
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

}
