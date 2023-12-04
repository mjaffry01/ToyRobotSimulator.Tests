using NUnit.Framework;
using ToyRobotSimulator;
using System;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    public class ToyRobotTests
    {
        private ToyRobot _toyRobot;

        [SetUp]
        public void Setup()
        {
            _toyRobot = new ToyRobot();
        }

        [Test]
        public void Place_ValidCoordinates_SetsPositionAndFacing()
        {
            _toyRobot.Place(1, 2, Direction.North);
            Assert.AreEqual(1, _toyRobot.X);
            Assert.AreEqual(2, _toyRobot.Y);
            Assert.AreEqual(Direction.North, _toyRobot.Facing);
        }

        [Test]
        public void Move_FacingNorth_IncrementsY()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _toyRobot.Move();
            Assert.AreEqual(0, _toyRobot.X);
            Assert.AreEqual(1, _toyRobot.Y);
        }

        [Test]
        public void Move_InvalidMove_DoesNotChangePosition()
        {
            _toyRobot.Place(0, 0, Direction.South);
            _toyRobot.Move();
            Assert.AreEqual(0, _toyRobot.X);
            Assert.AreEqual(0, _toyRobot.Y);
        }

        [Test]
        public void Left_ChangesFacingCorrectly()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _toyRobot.Left();
            Assert.AreEqual(Direction.West, _toyRobot.Facing);
        }

        [Test]
        public void Right_ChangesFacingCorrectly()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _toyRobot.Right();
            Assert.AreEqual(Direction.East, _toyRobot.Facing);
        }

        [Test]
        public void Report_ReturnsCurrentPositionAndFacing()
        {
            _toyRobot.Place(1, 2, Direction.East);
            var report = _toyRobot.Report();
            Assert.AreEqual("1,2,East", report);
        }
    }

    [TestFixture]
    public class CommandExecutorTests
    {
        private ToyRobot _toyRobot;
        private CommandExecutor _executor;

        [SetUp]
        public void Setup()
        {
            _toyRobot = new ToyRobot();
            _executor = new CommandExecutor(_toyRobot);
        }

        [Test]
        public void Execute_PlaceCommand_PlacesRobotCorrectly()
        {
            _executor.Execute("PLACE 1,2,NORTH");
            Assert.AreEqual(1, _toyRobot.X);
            Assert.AreEqual(2, _toyRobot.Y);
            Assert.AreEqual(Direction.North, _toyRobot.Facing);
        }

        [Test]
        public void Execute_InvalidCommand_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _executor.Execute("INVALID"));
        }

        [Test]
        public void Execute_MoveCommand_MovesRobotCorrectly()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _executor.Execute("MOVE");
            Assert.AreEqual(0, _toyRobot.X);
            Assert.AreEqual(1, _toyRobot.Y);
        }

        [Test]
        public void Execute_LeftCommand_RotatesRobotLeft()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _executor.Execute("LEFT");
            Assert.AreEqual(Direction.West, _toyRobot.Facing);
        }

        [Test]
        public void Execute_RightCommand_RotatesRobotRight()
        {
            _toyRobot.Place(0, 0, Direction.North);
            _executor.Execute("RIGHT");
            Assert.AreEqual(Direction.East, _toyRobot.Facing);
        }

        [Test]
        public void Execute_ReportCommand_ReturnsCorrectPositionAndFacing()
        {
            _toyRobot.Place(1, 2, Direction.South);
            _executor.Execute("REPORT");
            // The actual report is printed to the console, which is not captured here.
            // This test ensures that no exceptions are thrown.
            Assert.Pass();
        }
    }
}
