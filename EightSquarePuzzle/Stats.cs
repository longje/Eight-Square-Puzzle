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

using System.Collections.Generic;  //List
using System.ComponentModel; //INotifyProperty Changed

namespace EightPuzzle
{
public class Stats : INotifyPropertyChanged
	{
		private int _moves = 0;
		private String _gameState = "";
		private int _time = 0;
		private int _misplacedTile = 0;
		private int _manhattanDistance = 0;
        private int _manhattanDistancePlusReversal = 0;
		private System.Windows.Threading.DispatcherTimer myDispatcherTimer;

        public DateTime timeTaken { get; set; }
		public int moves { get { return this._moves; } set { this._moves = value; OnPropertyChanged(new PropertyChangedEventArgs("moves")); } }
		public String gameState { get { return this._gameState; } set { this._gameState = value; OnPropertyChanged(new PropertyChangedEventArgs("gameState")); } }
		public int time { get { return this._time; } set { this._time = value; OnPropertyChanged(new PropertyChangedEventArgs("time")); } }
		public int misplacedTile { get { return this._misplacedTile; } set { this._misplacedTile = value; OnPropertyChanged(new PropertyChangedEventArgs("misplacedTile")); } }
		public int manhattanDistance { get { return this._manhattanDistance; } set { this._manhattanDistance = value; OnPropertyChanged(new PropertyChangedEventArgs("manhattanDistance")); } }
        public int manhattanDistancePlusReversal { get { return this._manhattanDistancePlusReversal; } set { this._manhattanDistancePlusReversal = value; OnPropertyChanged(new PropertyChangedEventArgs("manhattanDistancePlusReversal")); } }
		public Stats()
		{
			myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
			myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); // 100 Milliseconds
			myDispatcherTimer.Tick += new EventHandler(updateTime);
		}

		public void startStatTimer()
		{
			myDispatcherTimer.Start();
		}

		public void stopStatTimer()
		{
			myDispatcherTimer.Stop();
		}

		private void updateTime(object o, EventArgs e)
		{
			time++;
		}

		public void resetStats()
		{
			moves = 0;
			gameState = "";
			time = 0;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}
	}
}
