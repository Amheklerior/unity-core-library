using NUnit.Framework;
using NSubstitute;
using System;

namespace Amheklerior.Core.EventSystem.Tests {
    
    internal class EventTests {

        private IEvent _event;
        private Action _action;

        #region SetUp/TearDown operations

        [OneTimeSetUp]
        public void BeforeAll() => _action = Substitute.For<Action>();

        [SetUp]
        public void BeforeEach() => _event = new Event();

        [TearDown]
        public void AfterEach() {
            _event = null;
            _action.ClearReceivedCalls();
        }

        [OneTimeTearDown]
        public void AfterAll() => _action = null;

        #endregion

        #region tests

        [Test]
        [TestCase(0, TestName = "case 1: no subscribers")]
        [TestCase(25, TestName = "case 2: x subscribers (x > 0)")]
        public void When_the_event_occurs_then_all_subscribed_actions_are_performed(int numberOfSubscribedActions) {

            // Arrange
            for (int i = 0; i < numberOfSubscribedActions; i++) _event.Subscribe(_action);

            // Act
            _event.Raise();

            // Assert
            _action.Received(numberOfSubscribedActions).Invoke();

        }

        [Test]
        [TestCase(0, 0, TestName = "case 1: no subscribers")]
        [TestCase(10, 0, TestName = "case 2: x subscribers, none unsibscribed (x > 0)")]
        [TestCase(5, 5, TestName = "case 3: x subscribers, x unsibscribed (x > 0)")]
        [TestCase(25, 15, TestName = "case 4: x subscribers, y unsibscribed (x > y > 0)")]
        public void When_the_event_occurs_then_unsubscribed_actions_are_not_performed(int numberOfPreviouslySubscribedActions, int numberOfUnsubscribedActions) {

            // Arrange
            int numberOfRemainingSubscribedActions = numberOfPreviouslySubscribedActions - numberOfUnsubscribedActions;
            for (int i = 0; i < numberOfPreviouslySubscribedActions; i++) _event.Subscribe(_action);
            for (int i = 0; i < numberOfUnsubscribedActions; i++) _event.Unsubscribe(_action);

            // Act
            _event.Raise();

            // Assert
            _action.Received(numberOfRemainingSubscribedActions).Invoke();

        }

        [Test]
        [TestCase(0, TestName = "case 1: no subscribers")]
        [TestCase(10, TestName = "case 2: x subscribers (x > 0)")]
        public void When_the_event_occurs_and_all_actions_are_unsubscribed_then_no_action_is_performed(int numberOfSubscribedActions) {

            // Arrange
            for (int i = 0; i < numberOfSubscribedActions; i++) _event.Subscribe(_action);
            _event.UnsibscribeAll();

            // Act
            _event.Raise();

            // Assert
            _action.DidNotReceive().Invoke();

        }

        #endregion
    }
}
