using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine;

namespace Amheklerior.Core.EventSystem.Tests {
    
    internal abstract class EventWithArgumentTests<TData> {

        private IEvent<TData> _event;
        private Action<TData> _actionWithArgs;

        protected readonly System.Random _argRandomizer = new System.Random();

        #region SetUp/TearDown operations

        [OneTimeSetUp]
        public void BeforeAll() => _actionWithArgs = Substitute.For<Action<TData>>();
            
        [SetUp]
        public void BeforeEach() => _event = new Event<TData>();

        [TearDown]
        public void AfterEach() {
            _event = null;
            _actionWithArgs.ClearReceivedCalls();
        }

        [OneTimeTearDown]
        public void AfterAll() => _actionWithArgs = null;

        #endregion

        #region tests

        [Test]
        [TestCase(0, TestName = "case 1: no subscribers")]
        [TestCase(25, TestName = "case 2: x subscribers (x > 0)")]
        public void Test_for_When_the_event_occurs_then_all_subscribed_actions_are_performed_with_the_given_argument(int numberOfSubscribedActions) {

            // Arrange
            var argument = GetRandomEventArgument();
            for (int i = 0; i < numberOfSubscribedActions; i++) _event.Subscribe(_actionWithArgs);

            // Act
            _event.Raise(argument);

            // Assert
            _actionWithArgs.Received(numberOfSubscribedActions).Invoke(argument);

        }

        [Test]
        [TestCase(0, 0, TestName = "case 1: no subscribers")]
        [TestCase(10, 0, TestName = "case 2: x subscribers, none unsibscribed (x > 0)")]
        [TestCase(5, 5, TestName = "case 3: x subscribers, x unsibscribed (x > 0)")]
        [TestCase(25, 15, TestName = "case 4: x subscribers, y unsibscribed (x > y > 0)")]
        public void Test_for_When_the_event_occurs_then_unsubscribed_actions_are_not_performed_with_the_given_argument(int numberOfPreviouslySubscribedActions, int numberOfUnsubscribedActions) {

            // Arrange
            var argument = GetRandomEventArgument();
            int numberOfRemainingSubscribedActions = numberOfPreviouslySubscribedActions - numberOfUnsubscribedActions;
            for (int i = 0; i < numberOfPreviouslySubscribedActions; i++) _event.Subscribe(_actionWithArgs);
            for (int i = 0; i < numberOfUnsubscribedActions; i++) _event.Unsubscribe(_actionWithArgs);

            // Act
            _event.Raise(argument);

            // Assert
            _actionWithArgs.Received(numberOfRemainingSubscribedActions).Invoke(argument);

        }

        [Test]
        [TestCase(0, TestName = "case 1: no subscribers")]
        [TestCase(10, TestName = "case 2: x subscribers (x > 0)")]
        public void Test_for_When_the_event_occurs_and_all_actions_are_unsubscribed_then_no_action_is_performed_with_the_given_argument(int numberOfSubscribedActions) {

            // Arrange
            var argument = GetRandomEventArgument();
            for (int i = 0; i < numberOfSubscribedActions; i++) _event.Subscribe(_actionWithArgs);
            _event.UnsibscribeAll();

            // Act
            _event.Raise(argument);

            // Assert
            _actionWithArgs.DidNotReceive().Invoke(argument);

        }

        #endregion

        protected abstract TData GetRandomEventArgument();

    }


    internal class EventWithArgument_Int : EventWithArgumentTests<int> {
        protected override int GetRandomEventArgument() => _argRandomizer.Next();
    }


    internal class EventWithArgument_Float : EventWithArgumentTests<float> {
        protected override float GetRandomEventArgument() => _argRandomizer.Next();
    }


    internal class EventWithArgument_String : EventWithArgumentTests<string> {
        private const string TEST_ARG = "TEST STRING";
        protected override string GetRandomEventArgument() => TEST_ARG;
    }


    internal class EventWithArgument_Vector2D : EventWithArgumentTests<Vector2> {
        protected override Vector2 GetRandomEventArgument() => new Vector2(_argRandomizer.Next(), _argRandomizer.Next());
    }


    internal class EventWithArgument_Vector2DInt : EventWithArgumentTests<Vector2Int> {
        protected override Vector2Int GetRandomEventArgument() => new Vector2Int(_argRandomizer.Next(), _argRandomizer.Next());
    }


    internal class EventWithArgument_Vector3D : EventWithArgumentTests<Vector3> {
        protected override Vector3 GetRandomEventArgument() => new Vector3(_argRandomizer.Next(), _argRandomizer.Next(), _argRandomizer.Next());
    }


    internal class EventWithArgument_Vector3DInt : EventWithArgumentTests<Vector3Int> {
        protected override Vector3Int GetRandomEventArgument() => new Vector3Int(_argRandomizer.Next(), _argRandomizer.Next(), _argRandomizer.Next());
    }
    
}
