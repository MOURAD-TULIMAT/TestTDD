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
            var res = MarsRover.Move(new Point(0,0),'N', "f");
            res.Should().Be(new Point(0, 1));
        }
    }

    public class MarsRover
    {
        public static object Move(Point start,char direction, string movements)
        {
            return new Point(0,1);
        }
    }
    public record Point(int x, int y) { }
}
