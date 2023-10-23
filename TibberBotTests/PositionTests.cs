using TibberBot.Dto;
using TibberBot.Helpers;

namespace TibberBotTests
{
    public class PositionTests
    {
        private readonly Position _home = new(0, 0);

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Calculate_New_Position_North(int steps)
        {
            var newPos = _home.CalculateNext("north", steps);
            Assert.Multiple(() =>
            {
                Assert.That(newPos, Is.Not.EqualTo(_home));
                Assert.That(newPos.Y, Is.EqualTo(_home.Y + steps));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Calculate_New_Position_South(int steps)
        {
            var newPos = _home.CalculateNext("south", steps);
            Assert.Multiple(() =>
            {
                Assert.That(newPos, Is.Not.EqualTo(_home));
                Assert.That(newPos.Y, Is.EqualTo(_home.Y - steps));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Calculate_New_Position_East(int steps)
        {
            var newPos = _home.CalculateNext("east", steps);
            Assert.Multiple(() =>
            {
                Assert.That(newPos, Is.Not.EqualTo(_home));
                Assert.That(newPos.X, Is.EqualTo(_home.X + steps));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Calculate_New_Position_West(int steps)
        {
            var newPos = _home.CalculateNext("west", steps);
            Assert.Multiple(() =>
            {
                Assert.That(newPos, Is.Not.EqualTo(_home));
                Assert.That(newPos.X, Is.EqualTo(_home.X - steps));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Enumerate_New_Positions_North(int steps)
        {
            var newPosArr = _home.EnumeratePositions("north", steps).ToArray();
            Assert.Multiple(() =>
            {
                Assert.That(newPosArr, Is.Not.Empty);
                Assert.That(newPosArr, Has.Length.EqualTo(steps));
                Assert.That(newPosArr.All(p => _home.Y < p.Y));
                Assert.That(newPosArr.Max(p => p.Y), Is.EqualTo(_home.Y + steps));
                Assert.That(newPosArr.Min(p => p.Y), Is.EqualTo(_home.Y + 1));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Enumerate_New_Positions_South(int steps)
        {
            var newPosArr = _home.EnumeratePositions("south", steps).ToArray();
            Assert.Multiple(() =>
            {
                Assert.That(newPosArr, Is.Not.Empty);
                Assert.That(newPosArr, Has.Length.EqualTo(steps));
                Assert.That(newPosArr.All(p => _home.Y > p.Y));
                Assert.That(newPosArr.Max(p => p.Y), Is.EqualTo(_home.Y - 1));
                Assert.That(newPosArr.Min(p => p.Y), Is.EqualTo(_home.Y - steps));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Enumerate_New_Positions_East(int steps)
        {
            var newPosArr = _home.EnumeratePositions("east", steps).ToArray();
            Assert.Multiple(() =>
            {
                Assert.That(newPosArr, Is.Not.Empty);
                Assert.That(newPosArr, Has.Length.EqualTo(steps));
                Assert.That(newPosArr.All(p => _home.X < p.X));
                Assert.That(newPosArr.Max(p => p.X), Is.EqualTo(_home.X + steps));
                Assert.That(newPosArr.Min(p => p.X), Is.EqualTo(_home.X + 1));
            });
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(100000000)]
        public void Test_Enumerate_New_Positions_West(int steps)
        {
            var newPosArr = _home.EnumeratePositions("west", steps).ToArray();
            Assert.Multiple(() =>
            {
                Assert.That(newPosArr, Is.Not.Empty);
                Assert.That(newPosArr, Has.Length.EqualTo(steps));
                Assert.That(newPosArr.All(p => _home.X > p.X));
                Assert.That(newPosArr.Max(p => p.X), Is.EqualTo(_home.X - 1));
                Assert.That(newPosArr.Min(p => p.X), Is.EqualTo(_home.X - steps));
            });
        }
    }
}