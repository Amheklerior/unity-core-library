using NUnit.Framework;
using NSubstitute;
using System;

namespace Amheklerior.Core.Command.Tests {

    internal class CommandExecutorTests {

        private const int EXECUTED_COMMANDS_COUNT = 3;

        private ICommandExecutor _executor;

        //private ICommand _cmd1, _cmd2, _cmd3;


        #region SetUp/TearDown operations

        /*
        [OneTimeSetUp]
        public void BeforeAll() {
            _cmd1 = Substitute.For<ICommand>();
            _cmd2 = Substitute.For<ICommand>();
            _cmd3 = Substitute.For<ICommand>();
        }
        */

        [SetUp] public void BeforeEach() => _executor = new CommandExecutor();

        [TearDown] public void AfterEach() => _executor = null;

        // [OneTimeTearDown] public void AfterAll() => _cmd1 = _cmd2 = _cmd3 = null;

        #endregion


        #region Tests
        
        [Test]
        public void When_reversible_commands_are_executed_the_executor_maintains_a_stack_of_execution_records() {

            // Arrange
            for (int i = 0; i < EXECUTED_COMMANDS_COUNT; i++) {
                var cmd = Substitute.For<ICommand>();
                cmd.Reversible.Returns(true);
                _executor.Execute(cmd);
            }

            // Act & Assert
            Assert.AreEqual(EXECUTED_COMMANDS_COUNT, _executor.Count);

        }

        [Test]
        public void When_the_last_command_executed_is_reversible_then_that_command_can_be_accessed_via_the_executor() {

            // Arrange
            var cmd1 = Substitute.For<ICommand>();
            cmd1.Reversible.Returns(true);
            _executor.Execute(cmd1);

            var cmd2 = Substitute.For<ICommand>();
            cmd2.Reversible.Returns(true);
            _executor.Execute(cmd2);
            
            // Act & Assert
            Assert.AreSame(cmd2, _executor.LastExcecuted);

        }

        [Test]
        public void Given_that_all_the_executed_commands_are_reversible_the_undo_can_be_performed_until_reverting_all_the_executed_operations() {

            // Arrange
            for (int i = 0; i < EXECUTED_COMMANDS_COUNT; i++) {
                var cmd = Substitute.For<ICommand>();
                cmd.Reversible.Returns(true);
                _executor.Execute(cmd);
            }

            // Act & Assert
            for (int i = 0; i < EXECUTED_COMMANDS_COUNT; i++) {
                Assert.That(_executor.CanUndo);
                _executor.Undo();
            }
            Assert.AreEqual(0, _executor.Count);

        }

        [Test]
        public void All_commands_executed_before_a_non_reversible_command_execution_cannot_be_undone() {

            // Arrange
            ICommand cmd;
            for (int i = 0; i < EXECUTED_COMMANDS_COUNT; i++) {
                cmd = Substitute.For<ICommand>();
                cmd.Reversible.Returns(true);
                _executor.Execute(cmd);
            }
            
            // Act
            cmd = Substitute.For<ICommand>();
            cmd.Reversible.Returns(false);
            _executor.Execute(cmd);

            // Assert
            Assert.AreEqual(0, _executor.Count);

        }

        [Test]
        public void When_there_is_no_reversible_command_execution_to_revert_then_the_undo_is_an_invalid_operation() {

            // Arrange
            var cmd = Substitute.For<ICommand>();
            cmd.Reversible.Returns(false);
            _executor.Execute(cmd);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _executor.Undo());

        }

        #endregion

    }
}
