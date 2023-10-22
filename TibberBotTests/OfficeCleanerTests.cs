using Microsoft.Extensions.Logging;
using Moq;
using TibberBot.Cleaners;
using TibberBot.Dto;

namespace TibberBotTests
{
    public class OfficeCleanerTests
    {
        private ICleaner _officeCleaner;
        private readonly Position _home = new(0, 0);

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<OfficeCleaner>>();
            _officeCleaner = new OfficeCleaner(mockLogger.Object);
        }

        [Test]
        public void Test_Clean_Four_Corners()
        {
            var commandsList = new List<Command>()
            {
                { new Command("north", 100_000) },
                { new Command("east", 100_000) },
                { new Command("south", 100_000) },
                { new Command("south", 100_000) },
                { new Command("west", 100_000) },
                { new Command("west", 100_000) },
                { new Command("north", 100_000) },
                { new Command("north", 100_000) },
            };

            var expected = new ExecutionRecord()
            {
                StartingPosition = _home,
                Commands = commandsList.Count,
                Result = 800001,
                EndingPosition = new Position(-100_000, 100_000)
            };

            var record = _officeCleaner.Clean(_home, commandsList);
            Assert.That(record, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(record.Commands, Is.EqualTo(expected.Commands));
                Assert.That(record.Result, Is.EqualTo(expected.Result));
                Assert.That(record.StartingPosition, Is.EqualTo(expected.StartingPosition));
                Assert.That(record.EndingPosition, Is.EqualTo(expected.EndingPosition));
            });
        }

        [Test]
        public void Test_Clean_Small_Area()
        {
            var commandsList = new List<Command>()
            {
                { new Command("north", 100) },
                { new Command("east", 1000) },
                { new Command("west", 1500) },
                { new Command("south", 30) },
                { new Command("south", 2000) },
            };

            var expected = new ExecutionRecord()
            {
                StartingPosition = _home,
                Commands = commandsList.Count,
                Result = 3631,
                EndingPosition = new Position(-500, -1930)
            };

            var record = _officeCleaner.Clean(_home, commandsList);
            Assert.That(record, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(record.Commands, Is.EqualTo(expected.Commands));
                Assert.That(record.Result, Is.EqualTo(expected.Result));
                Assert.That(record.StartingPosition, Is.EqualTo(expected.StartingPosition));
                Assert.That(record.EndingPosition, Is.EqualTo(expected.EndingPosition));
            });
        }

        [TestCase("north")]
        [TestCase("south")]
        [TestCase("east")]
        [TestCase("west")]
        public void Test_Clean_Out_Of_Bounds(string direction)
        {
            var commandsList = new List<Command>()
            {
                { new Command(direction, 50_000) },
                { new Command(direction, 50_000) },
                { new Command(direction, 1) },
            };
            var record = _officeCleaner.Clean(_home, commandsList);
            Assert.That(record, Is.Null);
        }

        [Test]
        public void Test_Clean_No_Commands()
        {
            var record = _officeCleaner.Clean(_home, Array.Empty<Command>());
            Assert.That(record, Is.Null);
        }
    }
}
