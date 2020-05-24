using NUnit.Framework;
using NSubstitute;
using System;
using Amheklerior.Core.Test.Utilities;

namespace Amheklerior.Core.ObjectPooling.Tests {

    internal class PoolTests {
        
        private static readonly int DEFAULT_CAPACITY = 100;

        private IPool<TestObject> _pool;

        private CreationFunc<TestObject> _mockedCreateFunc;

        #region SetUp/TearDown operations

        [OneTimeSetUp]
        public void BeforeAll() {
            _mockedCreateFunc = Substitute.For<CreationFunc<TestObject>>();
            _mockedCreateFunc.Invoke().Returns(new TestObject());
        }

        [TearDown]
        public void AfterEach() => _mockedCreateFunc.ClearReceivedCalls();

        [OneTimeTearDown]
        public void AfterAll() => _mockedCreateFunc = null;

        #endregion

        [Test]
        public void By_default_the_pool_is_created_and_initialized_with_capacity_of_100() {

            // Act
            _pool = new Pool<TestObject>(_mockedCreateFunc);

            // Assert
            _mockedCreateFunc.Received(DEFAULT_CAPACITY).Invoke();

        }

        [Test]
        public void By_default_the_pool_is_not_able_to_expand() {

            // Arrange
            _pool = new Pool<TestObject>(_mockedCreateFunc);
            for (int i = 0; i < DEFAULT_CAPACITY; i++) _pool.Get();
            _mockedCreateFunc.ClearReceivedCalls();

            // Act
            var resultAfterExceedingCapacity = _pool.Get();
            
            // Assert
            Assert.IsNull(resultAfterExceedingCapacity);
            _mockedCreateFunc.DidNotReceive().Invoke();

        }

        [Test]
        [TestCase(25, TestName = "capacity > 0", Category = "Positive")]
        public void When_an_initial_capacity_is_provided_than_the_pool_is_created_and_initialized_with_that_capacity(int capacity) {

            // Act
            _pool = new Pool<TestObject>(_mockedCreateFunc, capacity);

            // Assert
            _mockedCreateFunc.Received(capacity).Invoke();

        }


        [Test]
        [TestCase(0, TestName = "case 1: capacity = 0", Category = "Negative")]
        [TestCase(-25, TestName = "case 2: capacity < 0", Category = "Negative")]
        public void When_a_non_positive_initial_capacity_is_provided_than_the_pool_fail_to_initialize_and_throws_the_appropriate_exception(int capacity) {

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Pool<TestObject>(_mockedCreateFunc, capacity));

        }
        
        [Test]
        public void When_the_pool_is_not_allowed_to_expand_then_the_amount_of_objects_that_can_be_taken_from_it_cannot_exeed_the_initial_capacity() {

            // Arrange
            var capacity = 10;
            _pool = new Pool<TestObject>(_mockedCreateFunc, capacity);
            for (int i = 0; i < capacity; i++) _pool.Get();
            _mockedCreateFunc.ClearReceivedCalls();

            // Act
            var resultAfterExceedingCapacity = _pool.Get();

            // Assert
            Assert.IsNull(resultAfterExceedingCapacity);
            _mockedCreateFunc.DidNotReceive().Invoke();

        }

        [Test]
        public void When_the_pool_is_allowed_to_expand_then_the_amount_of_objects_that_can_be_taken_from_it_can_exeed_the_initial_capacity() {

            // Arrange
            var capacity = 10;
            _pool = new Pool<TestObject>(_mockedCreateFunc, capacity, true);
            _mockedCreateFunc.ClearReceivedCalls();
            for (int i = 0; i < capacity; i++) _pool.Get();

            // Act
            var resultAfterExceedingCapacity = _pool.Get();

            // Assert
            Assert.IsNotNull(resultAfterExceedingCapacity);
            _mockedCreateFunc.Received(capacity).Invoke();

        }

        [Test]
        public void The_pool_will_never_take_back_an_object_that_has_not_previously_taken_from_the_pool_itself() {

            // Arrange
            var capacity = 10;
            _pool = new Pool<TestObject>(_mockedCreateFunc, capacity);
            for (int i = 0; i < capacity; i++) _pool.Get();

            // Act
            _pool.Put(new TestObject());

            // Assert
            Assert.IsNull(_pool.Get());

        }

        [Test]
        public void Given_a_get_callback_When_taking_an_instance_from_the_pool_then_the_callback_is_called_on_that_instance_before_being_given() {

            // Arrange
            var IsCallbackPerformed = false;
            _pool = new Pool<TestObject>(_mockedCreateFunc) {
                OnGet = (TestObject instance) => IsCallbackPerformed = true
            };

            // Act
            _pool.Get();

            // Assert
            Assert.That(IsCallbackPerformed);

        }

        [Test]
        public void Given_a_put_callback_When_putting_an_instance_back_in_the_pool_then_the_callback_is_called_on_that_instance_before_being_put_back() {

            // Arrange
            var IsCallbackPerformed = false;
            _pool = new Pool<TestObject>(_mockedCreateFunc) {
                OnPut = (TestObject instance) => IsCallbackPerformed = true
            };

            // Act
            _pool.Put(_pool.Get());

            // Assert
            Assert.That(IsCallbackPerformed);

        }

    }

}
