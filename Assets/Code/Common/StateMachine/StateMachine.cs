using System;

namespace Code.Common.StateMachine
{
    // Inspired by GKStateMachine https://developer.apple.com/documentation/gameplaykit/gkstatemachine
    // Basic Sync implementation
    public interface IStateMachine<T> where T : IState
    {
        void Init(T[] states);
        T CurrentState { get; }
        bool CanEnterState(Type type);
        bool Enter(Type type);
        void Update(int deltaTime);
    }

    public class SyncStateMachine : IStateMachine<IState>, IDisposable
    {
        private IState[] _states;

        public IState CurrentState { get; private set; }

        public void Init(IState[] states)
        {
            if (states == null)
            {
                throw new ArgumentException("states");
            }
            if (states.Length == 0)
            {
                throw new ArgumentException("At least one state should be available");
            }
            if (_states != null)
            {
                throw new Exception("State machime has been already init");
            }
            _states = states;
            CurrentState = states[0];
            CurrentState.DidEnter(null);
        }

        public bool CanEnterState(Type type)
        {
            return CurrentState.IsValidNextState(type);
        }

        public bool Enter(Type type)
        {
            if (!CanEnterState(type))
            {
                return false;
            }
            
            IState next = null;
            foreach (var state in _states)
            {
                if (state.GetType() != type)
                {
                    continue;
                }
                next = state;
                break;
            }

            if (next == null)
            {
                return false;
            }
            
            var prev = CurrentState;
            CurrentState.WillExit(next);
            CurrentState = next;
            CurrentState.DidEnter(prev);
            return true;
        }

        public void Update(int deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        public void Dispose()
        {
            if (CurrentState == null)
            {
                return;
            }
            CurrentState.WillExit(null);
            CurrentState = null;
            _states = null;
        }
    }
}