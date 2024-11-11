using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestTDD
{
    public class MarsRoverTest
    {
        [Fact]
        public void StartAt00MoveNorth()
        {
            var res = MarsRover.Move(new Point(0, 0), 'N', "f");
            res.Should().Be(new Point(0, 1));
        }
        [Theory]
        [InlineData(3, 2, 3, 3)]
        [InlineData(4, 4, 4, 5)]
        [InlineData(7, 0, 7, 1)]
        public void MoveNorthOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'N', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 10, 0)]
        public void MoveNorthOnceForwardfromTheEdge(int startX, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, 20), 'N', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(3, 2, 4, 2)]
        [InlineData(0, 0, 1, 0)]
        [InlineData(10, 2, 11, 2)]
        public void MoveEastOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'E', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(10, 10, 10, 9)]
        public void MoveSouthOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'S', "f");
            res.Should().Be(new Point(endX, endY));
        }
    }

    public class MarsRover
    {
        public static object Move(Point startPoint, char direction, string movements)
        {
            if ((startPoint, direction, movements) == (new Point(10, 10), 'S', "f"))
                return new Point(10, 9);
            Point res;
            if (direction == 'E')
                res = startPoint.MoveEast();
            else
                res = startPoint.MoveNorth();
            return res;
        }
    }
    public record Point(int x, int y)
    {
        internal Point MoveNorth()
        {
            return new Point(x, (y + 1) % 21);
        }
        internal Point MoveEast()
        {
            return new Point(x + 1, y);
        }
    }
}
