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
using System.Windows.Navigation;
using System.Threading; //multithreading

using System.ComponentModel;

namespace EightPuzzle
{
    public partial class EightSquares : UserControl
	{
        private ResultsWindow win;
        public Node currentNode;
        public Node solutionNode;
		public Stats stats;
		public Boolean inProgress = false;
        AStar astar = new AStar();
        PuzzleGenerator pz;
   
        public List<Node> results = new List<Node>();

		//public List<State> sz;
		
		public EightSquares()
		{
			InitializeComponent();
			initializePuzzleState();
		}

		private void initializePuzzleState()
		{
            List<List<Square>> values = new List<List<Square>>();
			int count = 0;
			for(int i = 0; i < 3; i++)
			{
				List<Square> temp = new List<Square>();
				for (int j = 0; j < 3; j++)
				{
					if (count > 7)
					{
						Square tempSquare = new Square(i, j, (0).ToString());
						temp.Add(tempSquare);
					}
					else
					{
						Square tempSquare = new Square(i, j, (count + 1).ToString());
						temp.Add(tempSquare);
						((Button)blockContainer.Children[count++]).DataContext = tempSquare;
					}
					
				}
				values.Add(temp);
			}
            currentNode = new Node(new State(values, ""));
			initializeStats();
		}

		private void initializeStats()
		{
			stats = new Stats();
			optionsContainer.DataContext = stats;
			stats.misplacedTile = Heuristics.misplacedTileHeuristic(currentNode);
            stats.manhattanDistance = Heuristics.manhattanDistanceHeuristic(currentNode);
            stats.manhattanDistancePlusReversal = Heuristics.manhattanDistanceHeuristicPlusReversalPenalty(currentNode);
		}

		#region: Click Handlers
		private void squareClicked(object sender, RoutedEventArgs e)
		{
			if (!inProgress)
			{
				inProgress = true;
				stats.startStatTimer();
			}
			Button block = (Button)sender;
			Square sq = (Square)block.DataContext;

			if (!stats.gameState.Equals(""))
			{
				stats.resetStats();
			}

			if (!sq.val.Equals("0"))
			{
				moveIt(sq);
				if (goalCheck())
				{
					gameWon();
				}
			}
		}

		private void gameWon()
		{
			stats.gameState = "You Win!";
			stats.stopStatTimer();
            updateStats();
            stats.moves = currentNode.gValue;
			inProgress = false;
		}

        private void reallyGeneratePuzzle(object o, DoWorkEventArgs e)
        {
            PuzzleGenerator pz = new PuzzleGenerator();
            e.Result = pz.randomPuzzle((int)e.Argument);
        }

        private void finishGeneratePuzzle(object o, RunWorkerCompletedEventArgs e)
        {
            setCurrentNode((Node)e.Result);
            updateStats();
            stats.resetStats();
            buttonControl(true);
            randomProgress.Visibility = Visibility.Collapsed;        
        }

		private void generateNewPuzzle(object o, EventArgs e)
		{
            buttonControl(false);
            randomProgress.Visibility = Visibility.Visible;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += reallyGeneratePuzzle;
            worker.RunWorkerCompleted += finishGeneratePuzzle;
            //worker.RunWorkerCompleted += randomDone;
            worker.RunWorkerAsync((int)slider1.Value);
            
			//generateRandomPuzzle();
            
		}


		#endregion
		
		public void updateStats()
		{
            stats.misplacedTile = Heuristics.misplacedTileHeuristic(currentNode);
            stats.manhattanDistance = Heuristics.manhattanDistanceHeuristic(currentNode);
            stats.manhattanDistancePlusReversal = Heuristics.manhattanDistanceHeuristicPlusReversalPenalty(currentNode);
			stats.moves++;
		}

		private void moveIt(Square activeSquare)
		{
			Square zeroBlock = getBlankPosition();
			if (canBeMoved(activeSquare, zeroBlock))
			{
				swapSquares(activeSquare, zeroBlock);
				updateStats();
			}
		}

		private Boolean canBeMoved(Square activeSquare, Square zeroBlock)
		{
			if ((activeSquare.X == zeroBlock.X && ((Math.Abs(activeSquare.Y - zeroBlock.Y)) == 1))
				|| (activeSquare.Y == zeroBlock.Y && ((Math.Abs(activeSquare.X - zeroBlock.X)) == 1)))
			{
				return true;
			}
			return false;
		}

		private void swapSquares(Square activeSquare, Square zeroBlock)
		{
			String tempContent = activeSquare.val;
			int[] tempCoord = { activeSquare.X, activeSquare.Y };
			
			Square temp = currentNode.state.state[activeSquare.X][activeSquare.Y];
            currentNode.state.state[activeSquare.X][activeSquare.Y] = currentNode.state.state[zeroBlock.X][zeroBlock.Y];
            currentNode.state.state[zeroBlock.X][zeroBlock.Y] = temp;
			
			activeSquare.X = zeroBlock.X;
			activeSquare.Y = zeroBlock.Y;

			zeroBlock.X = tempCoord[0];
			zeroBlock.Y = tempCoord[1];

            currentNode.hValue = Heuristics.manhattanDistanceHeuristicPlusReversalPenalty(currentNode);
            currentNode.setAsRootNode();

		}

		private Boolean goalCheck()
		{
			String[] goalState = { "1", "2", "3", "4", "5", "6", "7", "8", "0" };
			int count = 0;
            foreach (List<Square> list in currentNode.state.state)
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

		private Square getBlankPosition()
		{
            foreach (List<Square> list in currentNode.state.state) 
			{
				foreach(Square block in list)
				{
					if (block.val.Equals("0"))
					{
						return block;
					}
				}
			}
			return null;
		}

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider1 != null)
            {
                slider1.Value = (int)slider1.Value;
                lblMessage.Text = "At least " + slider1.Value.ToString() + " moves required";
            }
        }

        private void buttonControl(Boolean value)
        {
            genRandom.IsEnabled = value;
            solvePuz.IsEnabled = value;
            showRes.IsEnabled = value;
            showAll.IsEnabled = value;
        }

        private void solvePuzzle(object sender, RoutedEventArgs e)
        {
            randomProgress.Visibility = Visibility.Visible;
            BackgroundWorker worker = new BackgroundWorker();
            pz = new PuzzleGenerator();
            buttonControl(false);
            worker.DoWork += backgroundWorkerDoWork;
            worker.RunWorkerCompleted += backGroundWorkerCompleted;
            worker.RunWorkerAsync();

            results.Clear();
        }

        private void backgroundWorkerDoWork(object obj, DoWorkEventArgs e)
        {
            List<Node> childrenNodes = new List<Node>();
            childrenNodes.Add(currentNode);
            e.Result = astar.aStarSearch(childrenNodes);
        }

        private void backGroundWorkerCompleted(object obj, RunWorkerCompletedEventArgs e)
        {
            setCurrentNode((Node) e.Result);
            printMoves(currentNode);
            buttonControl(true);
            showResults(new object(), new RoutedEventArgs());
            gameWon();
            randomProgress.Visibility = Visibility.Collapsed;
        }

        public void setCurrentNode(Node newNode)
        {
            int count = 0;

            for (int i = 0; i < newNode.state.state.Count; i++)
            {
                for (int j = 0; j < newNode.state.state[i].Count; j++)
                {
                    if (!newNode.state.state[i][j].val.Equals("0"))
                    {
                        ((Button)blockContainer.Children[count++]).DataContext = newNode.state.state[i][j];
                    }
                }
            }

            currentNode = newNode;
        }

        private void showResults(object sender, RoutedEventArgs e)
        {
            List<Node> temp = new List<Node>();
            for (int i = 0; i < astar.exploredSet.Length; i++)
            {
                foreach (Node n in astar.exploredSet[i])
                {
                    temp.Add(n);
                }
            }

            win = new ResultsWindow(stats, results, this, temp.Count, astar.allNodesList.Count);
            win.Show();
        }

        private void showAllNodes(object sender, RoutedEventArgs e)
        {
            List<Node> temp = new List<Node>();
            for (int i = 0; i < astar.exploredSet.Length; i++)
            {
                foreach (Node n in astar.exploredSet[i])
                {
                    temp.Add(n);
                }
            }

            AllNodesWindow allWin = new AllNodesWindow(temp, temp.Count, astar.allNodesList.Count);
            allWin.Show();
        }

        public void printMoves(Node goal)
        {
            if (goal.parent != null)
            {
                printMoves(goal.parent);
            }
            results.Add(goal);

        }

        private void zero_Checked(object sender, RoutedEventArgs e)
        {
            Heuristics.heuristic = Heuristics.zeroHeuristic;
        }

        private void misplaced_Checked(object sender, RoutedEventArgs e)
        {
            Heuristics.heuristic = Heuristics.misplacedTileHeuristic;
        }

        private void manhattan_Checked(object sender, RoutedEventArgs e)
        {
            Heuristics.heuristic = Heuristics.manhattanDistanceHeuristic;
        }

        private void manPlus_Checked(object sender, RoutedEventArgs e)
        {
            Heuristics.heuristic = Heuristics.manhattanDistanceHeuristicPlusReversalPenalty;
        }

        private void greedy_Checked(object sender, RoutedEventArgs e)
        {
            GoalValue.value = GoalValue.zeroGoalValue;
            manPlus.IsChecked = true;
            enableHeuristical(true);
        }

        private void uniform_Checked(object sender, RoutedEventArgs e)
        {
            GoalValue.value = GoalValue.goalValue;
            //Heuristics.heuristic = Heuristics.zeroHeuristic;
            zero.IsChecked = true;
            enableHeuristical(false);
            
        }

        private void astar_Checked(object sender, RoutedEventArgs e)
        {
            GoalValue.value = GoalValue.goalValue;
            if (manPlus != null)
            {
                manPlus.IsChecked = true;
                enableHeuristical(true);
            }
                
        }

        private void enableHeuristical(Boolean b)
        {
            misplaced.IsEnabled = b;
            manhattan.IsEnabled = b;
            manPlus.IsEnabled = b;
        }



	}
}
