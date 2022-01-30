using Battleship.Core;
using System;
using Battleship.Core.Models;
using Xunit;

namespace Battleship.Tests
{
    public class PositionTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void Position_should_throw_exception_when_value_x_is_out_of_range(int value)
        {
            Func<Position> act = () => new Position(value, 0);

            var argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("x", argumentOutOfRangeException.ParamName);
            Assert.Equal(value, argumentOutOfRangeException.ActualValue);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        public void Position_should_throw_exception_when_value_y_is_out_of_range(int value)
        {
            Func<Position> act = () => new Position(0, value);

            var argumentOutOfRangeException = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("y", argumentOutOfRangeException.ParamName);
            Assert.Equal(value, argumentOutOfRangeException.ActualValue);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 9)]
        [InlineData(9, 0)]
        [InlineData(9, 9)]
        [InlineData(4, 5)]
        public void Position_should_be_constructed_for_valid_coords(int x, int y)
        {
            var sut = new Position(x, y);

            Assert.Equal(x, sut.X);
            Assert.Equal(y, sut.Y);
        }
    }
}