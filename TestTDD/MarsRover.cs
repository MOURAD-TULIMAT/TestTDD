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
        [InlineData(0, 0, 'N', "rf", 1, 0, 'E')]
        [InlineData(0, 0, 'N', "rl", 0, 0, 'N')]
        [InlineData(0, 0, 'N', "lr", 0, 0, 'N')]
        [InlineData(0, 0, 'N', "ff", 0, 2, 'N')]
        [InlineData(0, 0, 'N', "bb", 0, 19, 'N')]
        [InlineData(0, 0, 'E', "ff", 2, 0, 'E')]
        [InlineData(0, 0, 'E', "bb", 9, 0, 'E')]
        [InlineData(0, 0, 'W', "bb", 2, 0, 'W')]
        [InlineData(0, 0, 'W', "ff", 9, 0, 'W')]
        [InlineData(0, 0, 'S', "bb", 0, 2, 'S')]
        [InlineData(0, 0, 'S', "ff", 0, 19, 'S')]
        public void TwoMovements(int startX, int startY, char direction, string movements, int endX, int endY, char endDirection)
        {
            var res = MarsRover.Move(new Point(startX, startY, direction), movements);
            res.Should().Be(new Point(endX, endY, endDirection));
        }
        [Theory]
        [InlineData(0, 0, 'W', "rfl", 0, 1, 'W')]
        [InlineData(0, 0, 'N', "rfl", 1, 0, 'N')]
        [InlineData(0, 0, 'N', "rlf", 0, 1, 'N')]
        [InlineData(0, 0, 'N', "lrf", 0, 1, 'N')]
        public void ThreeMovements(int startX, int startY, char direction, string movements, int endX, int endY, char endDirection)
        {
            var res = MarsRover.Move(new Point(startX, startY, direction), movements);
            res.Should().Be(new Point(endX, endY, endDirection));
        }
        [Theory]
        [InlineData(0, 0, 'w', "f")]
        [InlineData(0, 0, 'e', "f")]
        [InlineData(0, 0, 'n', "f")]
        [InlineData(0, 0, 's', "f")]
        public void WrongDirectionInput(int startX, int startY, char direction, string movements)
        {
            Assert.Throws<ArgumentException>(() => MarsRover.Move(new Point(startX, startY, direction), movements));
        }
        [Theory]
        [InlineData(0, 0, 'W', "x")]
        [InlineData(0, 0, 'W', "fx")]
        [InlineData(0, 0, 'W', "flF")]
        public void WrongMovementsInput(int startX, int startY, char direction, string movements)
        {
            Assert.Throws<ArgumentException>(() => MarsRover.Move(new Point(startX, startY, direction), movements));
        }
        [Theory]
        [InlineData(-1, 0, 'W', "x")]
        public void WrongStartXAxisInput(int startX, int startY, char direction, string movements)
        {
            Assert.Throws<ArgumentException>(() => MarsRover.Move(new Point(startX, startY, direction), movements));
        }
    }

    public class MarsRover
    {
        private static readonly List<char> Directions = ['N', 'E', 'S', 'W'];
        private static readonly List<char> AllowedMovements = ['f', 'b', 'l', 'r'];
        public static Point Move(Point startPoint, string movements)
        {
            if(startPoint.X == -1)
                throw new ArgumentException();
                
            if (!Directions.Contains(startPoint.Direction))
                throw new ArgumentException();
            Point res = startPoint;
            foreach (var move in movements)
            {
                if (!AllowedMovements.Contains(move))
                    throw new ArgumentException();
                if (move == 'r')
                {
                    var newDirection = Directions.ElementAt((Directions.IndexOf(res.Direction) + 1) % 4);
                    res = new Point(res.X, res.Y, newDirection);
                }
                else if (move == 'l')
                {
                    var newDirection = Directions.ElementAt((Directions.IndexOf(res.Direction) - 1 + 4) % 4);
                    res = new Point(res.X, res.Y, newDirection);
                }
                else
                {
                    res = res.MoveInDirection(move == 'b');
                }
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
