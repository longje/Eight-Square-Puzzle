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
    public class State
    {
        public List<List<Square>> state = new List<List<Square>>();
        public String action { get; set; }

        public State(List<List<Square>> state, String action)
        {
            this.state = state;
            this.action = action;
        }

        public void printState()
        {
            foreach (List<Square> data in state)
            {
                foreach (Square sq in data)
                {
                    Console.Write(sq.val + " ");
                }
            }
        }

        public Square getBlankPosition()
        {
            foreach (List<Square> list in state)
            {
                foreach (Square block in list)
                {
                    if (block.val.Equals("0"))
                    {
                        return block;
                    }
                }
            }
            return null;
        }


        public State clone(String action)
        {
            List<List<Square>> newState = new List<List<Square>>();
            foreach (List<Square> list in this.state)
            {
                List<Square> lsq = new List<Square>();
                newState.Add(lsq);
                foreach (Square sq in list)
                {
                    lsq.Add(new Square(sq.X, sq.Y, sq.val));
                }
            }
            return new State(newState, action);
        }

        private State leftChild()
        {
            if (this.getBlankPosition().Y > 0)
            {
                State leftChildState = this.clone("Left");
                Square blankSquare = leftChildState.getBlankPosition();
                Square leftSquare = leftChildState.state[blankSquare.X][blankSquare.Y - 1];
                return swapSquares(leftChildState, leftSquare, blankSquare);
            }
            return null;
        }

        private State rightChild()
        {
            if (this.getBlankPosition().Y < 2)
            {
                State rightChildState = this.clone("Right");
                Square blankSquare = rightChildState.getBlankPosition();
                Square rightSquare = rightChildState.state[blankSquare.X][blankSquare.Y + 1];
                return swapSquares(rightChildState, rightSquare, blankSquare);
            }
            return null;
        }

        private State upChild()
        {
            if (this.getBlankPosition().X > 0)
            {
                State upChildState = this.clone("Up");
                Square blankSquare = upChildState.getBlankPosition();
                Square rightSquare = upChildState.state[blankSquare.X - 1][blankSquare.Y];
                return swapSquares(upChildState, rightSquare, blankSquare);
            }
            return null;
        }

        private State downChild()
        {
            if (this.getBlankPosition().X < 2)
            {
                State downChildState = this.clone("Down");
                Square blankSquare = downChildState.getBlankPosition();
                Square rightSquare = downChildState.state[blankSquare.X + 1][blankSquare.Y];
                return swapSquares(downChildState, rightSquare, blankSquare);
            }
            return null;
        }

        private State swapSquares(State st, Square activeSquare, Square zeroBlock)
        {
            String tempContent = activeSquare.val;
            int[] tempCoord = { activeSquare.X, activeSquare.Y };

            Square temp = st.state[activeSquare.X][activeSquare.Y];
            st.state[activeSquare.X][activeSquare.Y] = st.state[zeroBlock.X][zeroBlock.Y];
            st.state[zeroBlock.X][zeroBlock.Y] = temp;

            activeSquare.X = zeroBlock.X;
            activeSquare.Y = zeroBlock.Y;

            zeroBlock.X = tempCoord[0];
            zeroBlock.Y = tempCoord[1];

            return st;
        }

        public List<State> generateChildren()
        {
            List<State> children = new List<State>();
            children.Add(leftChild());
            children.Add(rightChild());
            children.Add(upChild());
            children.Add(downChild());
            return children;
        }
    }
}
