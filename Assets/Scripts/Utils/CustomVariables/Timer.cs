using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FirerusUtilities
{
    [Serializable]
    public class Timer
    {
        public float StartTime { get; set; } = 1;
        public float Multiplier { get; set; } = 1f;

        public float CurrentTime { get; protected set; }
        public float TimePassed => StartTime - CurrentTime;

        public bool Paused { get; private set; } = false;

        public Timer(float startTime)
        {
            StartTime = startTime;
            Reset();
        }

        /// <summary>
        /// Invokes when the timer reachs zero.
        /// </summary>
        public event UnityAction Finished;

        /// <summary>
        /// Invokes every frame while timer is going.
        /// </summary>
        public event UnityAction Ticked;

        /// <summary>
        /// Set Current Time to start. Don't stops the timer.
        /// </summary>
        public void Reset() => CurrentTime = StartTime;

        /// <summary>
        /// Pause the timer. Saves the progress.
        /// </summary>
        public void Pause() => Paused = true;

        /// <summary>
        /// Continue the timer after pause.
        /// </summary>
        public void Continue() => Paused = false;

        /// <summary>
        /// Pause and reset the timer.
        /// </summary>
        public void Stop()
        {
            Pause();
            Reset();
        }

        /// <summary>
        /// Use StartCouroutine to start timer routine. Uses Time.deltaTime as tick.
        /// </summary>
        public virtual IEnumerator Start()
        {
            Continue();

            while (true)
            {
                if (Paused)
                    yield return null;

                CurrentTime -= Time.deltaTime * Multiplier;
                Ticked?.Invoke();
                if (this <= 0)
                {
                    Finished?.Invoke();
                    break;
                }

                yield return null;
            }
        }

        #region OperatorsOverriding
        public static Timer operator +(Timer timer, float value)
        {
            timer.CurrentTime += value;
            return timer;
        }

        public static Timer operator -(Timer timer, float value)
        {
            if (timer <= 0)
                return timer;

            timer.CurrentTime -= value;
            if (timer <= 0)
            {
                timer.Finished.Invoke();
            }
            return timer;
        }

        public static bool operator >(Timer timer, float value)
        {
            return timer.CurrentTime > value;
        }

        public static bool operator <(Timer timer, float value)
        {
            return timer.CurrentTime < value;
        }
        public static bool operator >=(Timer timer, float value)
        {
            return timer.CurrentTime >= value;
        }

        public static bool operator <=(Timer timer, float value)
        {
            return timer.CurrentTime <= value;
        }

        public static bool operator ==(Timer timer, float value)
        {
            return timer.CurrentTime == value;
        }

        public static bool operator !=(Timer timer, float value)
        {
            return timer.CurrentTime != value;
        }
        #endregion
    }
}
