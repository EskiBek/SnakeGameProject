using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_Game
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position GetPosition(Direction direct)
        {
            return new Position(Row + direct.RowPosition, Column + direct.ColumnPosition);
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !(left == right);
        }
    }
}
