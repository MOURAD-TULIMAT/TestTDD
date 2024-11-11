﻿using FluentAssertions;
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
        public void MoveNorthOnce(int startX, int startY, int endX, int endY)
        {
            var res = MarsRover.Move(new Point(startX, startY), 'N', "f");
            res.Should().Be(new Point(endX, endY));
        }
    }

    public class MarsRover
    {
        public static object Move(Point startPoint, char direction, string movements)
        {
            //if ((startPoint, direction, movements) == (new Point(3, 2), 'N', "f"))
            //    return new Point(3, 3);
            //if ((startPoint, direction, movements) == (new Point(4, 4), 'N', "f"))
            //    return new Point(4, 5);
            //if ((startPoint, direction, movements) == (new Point(7, 0), 'N', "f"))
            //    return new Point(7, 1);
            var res = startPoint.MoveNorth();
            return res;
        }
    }
    public record Point(int x, int y)
    {
        internal Point MoveNorth()
        {
            return new Point(this.x, this.y+1);
        }
    }
}
