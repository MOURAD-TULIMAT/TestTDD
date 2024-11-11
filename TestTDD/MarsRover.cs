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
            var res = MarsRover.Move();
        }
    }

    public class MarsRover
    {
        public static object Move()
        {
            throw new NotImplementedException();
        }
    }
}
