using Battleship.Core;
using FluentAssertions;
using Microsoft.VisualBasic.CompilerServices;
using System;
using Xunit;

namespace Battleship.Tests
{
    public class BoardTests
    {
        [Fact]
        public void AddShip_should_place_carrier_on_board()
        {
            var ship = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            var position = new Position(0, 0);
            var sut = new Board();

            sut.AddShip(ship, position);

            sut.GetField(0, 0).Should().Be(Field.Ship);
            sut.GetField(1, 0).Should().Be(Field.Ship);
            sut.GetField(2, 0).Should().Be(Field.Ship);
            sut.GetField(3, 0).Should().Be(Field.Ship);
            sut.GetField(4, 0).Should().Be(Field.Ship);
        }

        [Fact]
        public void AddShip_should_place_battleship_on_board()
        {
            var ship = Ship.MakeShip(ClassOfShip.Battleship, Orientation.Vertical);
            var position = new Position(0, 0);
            var sut = new Board();

            sut.AddShip(ship, position);

            sut.GetField(0, 0).Should().Be(Field.Ship);
            sut.GetField(0, 1).Should().Be(Field.Ship);
            sut.GetField(0, 2).Should().Be(Field.Ship);
            sut.GetField(0, 3).Should().Be(Field.Ship);
        }

        [Fact]
        public void AddShip_should_throw_when_can_not_add_ship()
        {
            var ship = Ship.MakeShip(ClassOfShip.Battleship, Orientation.Vertical);
            var position = new Position(0, 0);
            var board = new Board();
            board.AddShip(ship, position);

            Action act = () => board.AddShip(ship, position);
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(9, 9, Orientation.Horizontal)]
        [InlineData(7, 0, Orientation.Horizontal)]
        [InlineData(9, 9, Orientation.Vertical)]
        [InlineData(0, 6, Orientation.Vertical)]
        public void CanAddShip_should_return_false_when_ship_is_out_of_board(int x, int y, Orientation orientation)
        {
            var ship = Ship.MakeShip(ClassOfShip.Carrier, orientation);
            var position = new Position(x, y);
            var board = new Board();

            var sut = board.CanAddShip(ship, position);

            sut.Should().BeFalse();
        }

        [Fact]
        public void CanAddShip_should_return_true_for_empty_board_and_position_within_board()
        {
            var ship = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            var position = new Position(0, 0);
            var board = new Board();

            var sut = board.CanAddShip(ship, position);
            sut.Should().BeTrue();
        }

        [Fact]
        public void CanAddShip_should_return_false_when_new_horizontal_ship_would_collide_with_existing_one()
        {
            var board = new Board();
            var existingShip = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            board.AddShip(existingShip, new Position(3, 3));
            var newShip = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            var position = new Position(0, 3);

            var sut = board.CanAddShip(newShip, position);
            sut.Should().BeFalse();
        }

        [Fact]
        public void CanAddShip_should_return_false_when_new_vertical_ship_would_collide_with_existing_one()
        {
            var board = new Board();
            var existingShip = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            board.AddShip(existingShip, new Position(3, 3));
            var newShip = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Vertical);
            var position = new Position(5, 0);

            var sut = board.CanAddShip(newShip, position);
            sut.Should().BeFalse();
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        [InlineData(5, 4)]
        [InlineData(5, 2)]
        [InlineData(5, 3)]
        public void CanAddShip_should_return_false_when_new_ship_would_touch_existing_one(int x, int y)
        {
            var board = new Board();
            var existingShip = Ship.MakeShip(ClassOfShip.Destroyer, Orientation.Horizontal);
            board.AddShip(existingShip, new Position(3, 3));
            var newShip = Ship.MakeShip(ClassOfShip.Destroyer, Orientation.Vertical);
            var position = new Position(x, y);

            var sut = board.CanAddShip(newShip, position);

            sut.Should().BeFalse();
        }

        [Fact]
        public void AddShip_should_throw_exception_when_too_many_carriers_are_added()
        {
            var board = new Board();
            var ship = Ship.MakeShip(ClassOfShip.Carrier, Orientation.Horizontal);
            board.AddShip(ship, new Position(0, 0));

            Action act = () => board.AddShip(ship, new Position(2, 0));
            act.Should().Throw<InvalidOperationException>();
        }
    }
}