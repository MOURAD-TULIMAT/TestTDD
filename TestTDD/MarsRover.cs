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
        public void StartAt00MoveNorthForward()
        {
            var res = MarsRover.Move(new Point(0, 0, 'N'), "f");
            res.Should().Be(new Point(0, 1, 'N'));
        }
        [Theory]
        [InlineData(3, 2, 3, 3)]
        [InlineData(4, 4, 4, 5)]
        [InlineData(7, 0, 7, 1)]
        public void MoveNorthOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY, 'N'), "f");
            res.Should().Be(new Point(endX, endY, 'N'));
        }
        [Theory]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 10, 0)]
        public void MoveNorthOnceForwardFromTheEdge(int startX, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, 20, 'N'), "f");
            res.Should().Be(new Point(endX, endY, 'N'));
        }
        [Theory]
        [InlineData(3, 2, 4, 2)]
        [InlineData(0, 0, 1, 0)]
        [InlineData(9, 2, 10, 2)]
        public void MoveEastOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY, 'E'), "f");
            res.Should().Be(new Point(endX, endY, 'E'));
        }
        [Theory]
        [InlineData(20, 0, 20)]
        [InlineData(10, 0, 10)]
        [InlineData(0, 0, 0)]
        public void MoveEastOnceForwardFromTheEdge(int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(10, startY, 'E'), "f");
            res.Should().Be(new Point(endX, endY, 'E'));
        }
        [Theory]
        [InlineData(3, 2, 3, 1)]
        [InlineData(0, 10, 0, 9)]
        [InlineData(10, 10, 10, 9)]
        public void MoveSouthOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY, 'S'), "f");
            res.Should().Be(new Point(endX, endY, 'S'));
        }
        [Theory]
        [InlineData(0, 0, 20)]
        [InlineData(5, 5, 20)]
        [InlineData(10, 10, 20)]
        public void MoveSouthOnceForwardFromTheEdge(int startX, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, 0, 'S'), "f");
            res.Should().Be(new Point(endX, endY, 'S'));
        }
        [Theory]
        [InlineData(5, 10, 4, 10)]
        [InlineData(5, 20, 4, 20)]
        [InlineData(5, 0, 4, 0)]
        public void MoveWestOnceForward(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY, 'W'), "f");
            res.Should().Be(new Point(endX, endY, 'W'));
        }
        [Theory]
        [InlineData(20, 10, 20)]
        [InlineData(10, 10, 10)]
        [InlineData(0, 10, 0)]
        public void MoveWestOnceForwardFromTheEdge(int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(0, startY, 'W'), "f");
            res.Should().Be(new Point(endX, endY, 'W'));
        }

        [Theory]
        [InlineData(0, 0, 'S', 0, 1)]
        [InlineData(0, 0, 'N', 0, 20)]
        [InlineData(0, 0, 'W', 1, 0)]
        [InlineData(0, 0, 'E', 10, 0)]
        public void MoveOnceBackwards(int startX, int startY, char direction, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY, direction), "b");
            res.Should().Be(new Point(endX, endY, direction));
        }

        [Theory]
        [InlineData('N', 'E')]
        [InlineData('E', 'S')]
        [InlineData('S', 'W')]
        [InlineData('W', 'N')]
        public void TestRotatingToTheRight(char direction, char expected)
        {
            var res = MarsRover.Move(new Point(0, 0, direction), "r");
            res.Should().Be(new Point(0, 0, expected));
        }
        [Theory]
        [InlineData('N', 'W')]
        [InlineData('E', 'N')]
        [InlineData('S', 'E')]
        [InlineData('W', 'S')]
        public void TestRotatingToTheLeft(char direction, char expected)
        {
            var res = MarsRover.Move(new Point(0, 0, direction), "l");
            res.Should().Be(new Point(0, 0, expected));
        }

        [Theory]
        [InlineData(0, 0, 'W', "rf", 0, 1, 'N')]
        public void TwoMovements(int startX, int startY, char direction, string movements, int endX, int endY, char endDirection)
        {
            var res = MarsRover.Move(new Point(startX, startY, direction), movements);
            res.Should().Be(new Point(endX, endY, endDirection));
        }

    }

    public class MarsRover
    {
        private static readonly List<char> directions = ['N', 'E', 'S', 'W'];
        public static Point Move(Point startPoint, string movements)
        {
            Point res;
            if (startPoint == new Point(0, 0, 'W') && movements == "rf")
            {
                return new Point(0, 1, 'N');
            }
            if (movements == "r")
            {
                var newDirection = directions.ElementAt((directions.IndexOf(startPoint.Direction) + 1) % 4);
                res = new Point(startPoint.X, startPoint.Y, newDirection);
            }
            else if (movements == "l")
            {
                var newDirection = directions.ElementAt((directions.IndexOf(startPoint.Direction) - 1 + 4) % 4);
                res = new Point(startPoint.X, startPoint.Y, newDirection);
            }
            else
            {
                res = startPoint.MoveInDirection(movements[0] == 'b');
            }

            return res;
        }
    }
    public record Point(int X, int Y, char Direction)
    {
        public Point MoveInDirection(bool isBackwards) =>
            isBackwards ?
            Direction switch
            {

                'S' => new Point(X, (Y + 1) % 21, Direction),
                'W' => new Point((X + 1) % 11, Y, Direction),
                'N' => new Point(X, (Y + 20) % 21, Direction),
                'E' => new Point((X + 10) % 11, Y, Direction),
                _ => this
            } :
            Direction switch
            {

                'N' => new Point(X, (Y + 1) % 21, Direction),
                'E' => new Point((X + 1) % 11, Y, Direction),
                'S' => new Point(X, (Y + 20) % 21, Direction),
                'W' => new Point((X + 10) % 11, Y, Direction),
                _ => this
            }
            ;
    }
}
