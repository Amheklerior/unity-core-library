using NUnit.Framework;
using NSubstitute;
using System;

namespace Amheklerior.Core.Command.Tests {

    internal class CommandTests {

        private ICommand _cmd;

        private Action _action, _undo;


        #region SetUp/TearDown operations

        [OneTimeSetUp] public void BeforeAll() {
            _action = Substitute.For<Action>();
            _undo = Substitute.For<Action>();
        }

        [TearDown]
        public void AfterEach() {
            _cmd = null;
            _action.ClearReceivedCalls();
            _undo.ClearReceivedCalls();
        }

        [OneTimeTearDown] public void AfterAll() => _action = _undo = null;

        #endregion


        #region Tests

        [Test]
        public void When_a_command_is_performed_its_main_action_is_invoked() {

            // Arrange
            _cmd = new Command(_action);

            // Act
            _cmd.Perform();

            // Assert
            _action.Received(1).Invoke();

        }
        
        [Test]
        public void When_the_command_has_an_undo_action_then_the_command_is_reversible() {

            // Arrange
            _cmd = new Command(_action, _undo);

            // Assert
            Assert.That(_cmd.Reversible);

        }
        
        [Test]
        public void When_the_command_does_not_have_an_undo_action_then_the_command_is_not_reversible() {

            // Arrange
            _cmd = new Command(_action);

            // Assert
            Assert.IsFalse(_cmd.Reversible);

        }
        
        [Test]
        public void If_the_command_is_reversible_then_the_undo_can_be_performed() {

            // Arrange
            _cmd = new Command(_action, _undo);

            // Act
            _cmd.Undo();

            // Assert
            _undo.Received(1).Invoke();

        }
        
        [Test]
        public void If_the_command_is_not_reversible_then_performing_the_undo_results_in_no_operation() {

            // Arrange
            _cmd = new Command(_action);

            // Act
            _cmd.Undo();

            // Assert
            _undo.DidNotReceive().Invoke();

        }

        #endregion

    }
}
