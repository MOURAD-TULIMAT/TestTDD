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
        public void MoveNorthOnceForwardFromTheEdge(int startX, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, 20), 'N', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(3, 2, 4, 2)]
        [InlineData(0, 0, 1, 0)]
        [InlineData(9, 2, 10, 2)]
        public void MoveEastOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'E', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(20, 0, 20)]
        [InlineData(10, 0, 10)]
        [InlineData(0, 0, 0)]
        public void MoveEastOnceForwardFromTheEdge(int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(10, startY), 'E', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(3, 2, 3, 1)]
        [InlineData(0, 10, 0, 9)]
        [InlineData(10, 10, 10, 9)]
        public void MoveSouthOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'S', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(0, 0, 20)]
        [InlineData(5, 5, 20)]
        [InlineData(10, 10, 20)]
        public void MoveSouthOnceForwardFromTheEdge(int startX, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, 0), 'S', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(5, 10, 4, 10)]
        [InlineData(5, 20, 4, 20)]
        [InlineData(5, 0, 4, 0)]
        public void MoveWestOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'W', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(20, 10, 20)]
        [InlineData(10, 10, 10)]
        [InlineData(0, 10, 0)]
        public void MoveWestOnceForwardFromTheEdge(int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(0, startY), 'W', "f");
            res.Should().Be(new Point(endX, endY));
        }
        [Theory]
        [InlineData(0, 0, 'N', "ff", 0, 2)]
        [InlineData(0, 0, 'S', "fffff", 0, 16)]
        public void MoveMultipleTimesForward(int startX, int startY, char direction, string movements, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), direction, movements);
            res.Should().Be(new Point(endX, endY));
        }
    }

    public class MarsRover
    {
        public static object Move(Point startPoint, char direction, string movements)
        {
            Point res;
            switch (direction)
            {
                case 'E':
                    res = startPoint.MoveEast();
                    break;
                case 'S':
                    res = startPoint.MoveSouth(movements.Length);
                    break;
                case 'N':
                    res = startPoint.MoveNorth(movements.Length);
                    break;
                default:
                    res = startPoint.MoveWest();
                    break;
            }

            return res;
        }
    }
    public record Point(int x, int y)
    {
        internal Point MoveNorth(int count)
        {
            return new Point(x, (y + count) % 21);
        }
        internal Point MoveEast()
        {
            return new Point((x + 1) % 11, y);
        }
        internal Point MoveSouth(int count)
        {
            return new Point(x, ((y - count) + 21) % 21);
        }
        internal Point MoveWest()
        {
            return new Point((x - 1 + 11) % 11, y);
        }
    }
}
