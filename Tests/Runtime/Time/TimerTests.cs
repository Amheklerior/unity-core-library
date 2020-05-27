using NUnit.Framework;
using NSubstitute;
using System;

namespace Amheklerior.Core.Time.Test {

    class TimerTests {

        private const float EXPIRY_TIME = 5.7f;
        private const float ELAPSED = 2.5f;

        private Timer _timer;

        #region SetUp/TearDown operations
        
        [TearDown] public void AfterEach() => _timer = null;

        #endregion

        #region Tests

        [Test]
        public void After_being_started_the_timer_is_running() {

            // Arrange
            _timer = new Timer();

            // Act
            _timer.Start();

            // Assert
            Assert.That(_timer.IsRunning);

        }

        [Test]
        public void After_being_paused_the_timer_is_not_running_and_the_current_time_is_preserved() {

            // Arrange 
            _timer = new Timer(); 
            _timer.Start();
            _timer.Tick(ELAPSED);

            // Act
            _timer.Pause();

            // Assert
            Assert.That(!_timer.IsRunning);
            Assert.AreEqual(ELAPSED, _timer.Current);

        }

        [Test]
        public void After_being_resumed_the_timer_is_running_with_the_current_time_continuing_from_where_it_was() {

            // Arrange 
            _timer = new Timer();
            _timer.Start();
            _timer.Tick(ELAPSED);
            _timer.Pause();

            // Act
            _timer.Resume();

            // Assert
            Assert.That(_timer.IsRunning);
            Assert.AreEqual(ELAPSED, _timer.Current);

        }

        [Test]
        public void After_being_stopped_the_timer_is_not_running_and_the_current_time_is_reset_to_zero() {

            // Arrange 
            _timer = new Timer();
            _timer.Start();
            _timer.Tick(ELAPSED);
            Assert.AreEqual(ELAPSED, _timer.Current);

            // Act
            _timer.Stop();

            // Assert
            Assert.That(!_timer.IsRunning);
            Assert.AreEqual(0f, _timer.Current);

        }

        [Test]
        public void After_being_restarted_the_timer_is_running_and_the_current_time_is_reset_to_zero() {

            // Arrange 
            _timer = new Timer();
            _timer.Start();
            _timer.Tick(ELAPSED);
            Assert.AreEqual(ELAPSED, _timer.Current);

            // Act
            _timer.Restart();

            // Assert
            Assert.That(_timer.IsRunning);
            Assert.AreEqual(0f, _timer.Current);

        }

        [Test]
        public void When_timer_is_not_running_then_a_tick_does_not_have_any_effect() {

            // Arrange 
            _timer = new Timer();
            _timer.Start();
            _timer.Tick(ELAPSED);
            _timer.Pause();

            // Act
            _timer.Tick(10f);

            // Assert
            Assert.That(!_timer.IsRunning);
            Assert.AreEqual(ELAPSED, _timer.Current);

        }

        [Test]
        public void When_timer_is_running_a_tick_pushes_the_current_time_forward_by_the_specified_amount() {

            // Arrange 
            var delta = 10f;
            _timer = new Timer();
            _timer.Start();
            _timer.Tick(ELAPSED);

            // Act
            _timer.Tick(delta);

            // Assert
            Assert.AreEqual(ELAPSED + delta, _timer.Current);

        }


        [Test]
        [TestCase(0f, TestName = "case 1: expiryTime = 0", Category = "Negative")]
        [TestCase(-2.5f, TestName = "case 2: expiryTime < 0", Category = "Negative")]
        public void If_the_provided_expiry_time_is_not_positive_the_proper_exception_is_thrown_at_construction_time(float invalidExpiryTime) {

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _timer = new Timer(invalidExpiryTime));
            
        }


        [Test]
        public void Given_an_expiry_time_when_the_timer_reach_it_the_given_action_is_triggered() {

            // Arrange 
            var action = Substitute.For<Action>();
            _timer = new Timer(EXPIRY_TIME, action);
            _timer.Start();

            // Act & Assert
            _timer.Tick(EXPIRY_TIME);

            // Assert
            action.Received(1).Invoke();

        }

        [Test]
        public void When_the_expiry_time_is_surpassed_then_a_tick_does_not_have_any_effect() {

            // Arrange 
            _timer = new Timer(EXPIRY_TIME);
            _timer.Start();
            _timer.Tick(EXPIRY_TIME);


            // Act & Assert
            _timer.Tick(ELAPSED);

            // Assert
            Assert.AreEqual(EXPIRY_TIME, _timer.Current);

        }


        #endregion

    }
}
