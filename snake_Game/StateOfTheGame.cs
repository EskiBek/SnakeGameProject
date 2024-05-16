using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_Game
{
    public class StateOfTheGame
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public AreaValue[,] Area { get; set; }
        public Direction direction { get; set; }
        public int Score { get; set; }
        public bool isGameOver { get; set; }
        private LinkedList<Position> positionsSnake = new LinkedList<Position>();
        private Random random = new Random();

        private LinkedList<Direction> directionsChanges = new LinkedList<Direction>();

        public StateOfTheGame(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Area = new AreaValue[rows, columns];
            direction = Direction.Up;
            Score = 0;
            isGameOver = false;

            CreateSnake();
            FoodAppear();
        }

        private void CreateSnake() 
        {
            int s = Rows / 3;
            for(int e = 1;  e <= 3; e++)
            {
                Area[s, e] = AreaValue.BodySnake;
                positionsSnake.AddFirst(new Position(s, e));
            }
        }

        private IEnumerable<Position> FreePositions()
        {
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Area[i,j] == AreaValue.EmptyArea)
                    {
                        yield return new Position(i, j);
                    }
                }   
            }
        }

        private void FoodAppear()
        {
            List<Position> emptyPos = new List<Position>(FreePositions());

            if(emptyPos.Count == 0)
            {
                return;
            }

            Position pos = emptyPos[random.Next(emptyPos.Count)];
            Area[pos.Row, pos.Column] = AreaValue.Food;
        }

        public Position HeadPos()
        {
            return positionsSnake.First.Value;
        }

        public Position EndPos()
        {
            return positionsSnake.Last.Value;
        }

        public IEnumerable<Position> SnakePos() 
        {
            return positionsSnake;
        }

        private void NewHead(Position pos)
        {
            positionsSnake.AddFirst(pos);
            Area[pos.Row, pos.Column] = AreaValue.BodySnake;
        }

        private void DeleteEnd()
        {
            Position tail = positionsSnake.Last.Value;
            Area[tail.Row, tail.Column] = AreaValue.EmptyArea;
            positionsSnake.RemoveLast();
        }

        private Direction GetLastDir()
        {
            if(directionsChanges.Count == 0)
            {
                return direction;
            }

            return directionsChanges.Last.Value;
        }

        private bool CanChangeDir(Direction newD)
        {
            if (directionsChanges.Count == 2) 
            {
                return false;
            }

            Direction lastD = GetLastDir();
            return newD != lastD && newD != lastD.NegativePosition();
            
        }

        public void ChangeDir(Direction dir)
        {
            if(CanChangeDir(dir))
            {
                directionsChanges.AddLast(dir);
            }
        }

        private bool isOffside(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;
        }

        private AreaValue isGoingToCrack(Position newHeadPosition)
        {
            if (isOffside(newHeadPosition))
            {
                return AreaValue.OutsideGrid;
            }

            if(newHeadPosition == EndPos())
            {
                return AreaValue.EmptyArea;
            }
            
            return Area[newHeadPosition.Row, newHeadPosition.Column];
        }

        public void Run()
        {
            if(directionsChanges.Count > 0)
            {
                direction = directionsChanges.First.Value;
                directionsChanges.RemoveFirst();
            }

            Position positionNewHead = HeadPos().GetPosition(direction);
            AreaValue crack = isGoingToCrack(positionNewHead);

            if (crack == AreaValue.OutsideGrid || crack == AreaValue.BodySnake)
            {
                isGameOver = true;
            }

            else if(crack == AreaValue.Food)
            {
                NewHead(positionNewHead);
                Score += 1;
                FoodAppear();
            }

            else
            {
                isGameOver = false;
                DeleteEnd();
                NewHead(positionNewHead);
            }
        }

    }
}
