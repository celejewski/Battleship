using Battleship.Core;
using FluentAssertions;
using Xunit;

namespace Battleship.Tests
{
    public class BotTests
    {
        [Fact]
        public void MakeStartingBoard_should_return_board_with_all_ships_placed()
        {
            var bot = new Bot();

            var sut = bot.MakeStartingBoard();

            sut.ShipsToAddCount[ClassOfShip.Carrier].Should().Be(0);
            sut.ShipsToAddCount[ClassOfShip.Battleship].Should().Be(0);
            sut.ShipsToAddCount[ClassOfShip.Cruiser].Should().Be(0);
            sut.ShipsToAddCount[ClassOfShip.Submarine].Should().Be(0);
            sut.ShipsToAddCount[ClassOfShip.Destroyer].Should().Be(0);
        }

        [Fact]
        public void GetPositionToFireAt_should_return_position()
        {
            var bot = new Bot();

            var position = bot.GetPositionToFireAt();

            position.Should().NotBeNull();
        }
    }
}