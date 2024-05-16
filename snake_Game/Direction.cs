using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_Game
{
    public class Direction
    {
        public static Direction Left = new Direction(0, -1);
        public static Direction Right = new Direction(0, 1);
        public static Direction Down = new Direction(1, 0);
        public static Direction Up = new Direction(-1, 0);
        public int RowPosition {  get; set; }
        public int ColumnPosition { get; set; }

        private Direction(int rowPosition, int columnPosition)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }

        public Direction NegativePosition()
        {
            return new Direction(-RowPosition, -ColumnPosition);
        }

        public override bool Equals(object? obj)
        {
            return obj is Direction direction &&
                   RowPosition == direction.RowPosition &&
                   ColumnPosition == direction.ColumnPosition;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowPosition, ColumnPosition);
        }

        public static bool operator ==(Direction? left, Direction? right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction? left, Direction? right)
        {
            return !(left == right);
        }
    }
}
