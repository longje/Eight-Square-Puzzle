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

using System.ComponentModel; //INotifyProperty Changed

namespace EightPuzzle
{
	public class Square : INotifyPropertyChanged
	{
		private int x;
		private int y;
		private String Val;

		public int X { get { return this.x; } set { this.x = value; OnPropertyChanged(new PropertyChangedEventArgs("X")); } }
		public int Y { get { return y; } set { y = value; OnPropertyChanged(new PropertyChangedEventArgs("Y")); } }
		public String val { get { return Val; } set { Val = value; OnPropertyChanged(new PropertyChangedEventArgs("val")); } }
		public int valueNum { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		public Square(int x, int y, String val)
		{
			this.x = x;
			this.Y = y;
			this.val = val;
			this.valueNum = Int32.Parse(val);
		}

		public Square(String val)
		{
			this.val = val;
			this.valueNum = Int32.Parse(val);
		}
	}
}
