using System;
using SharedCode.Extensions;

namespace Code.Common
{
    public interface ILazyModel : IDisposable
    {
        // OnReady is initialization ready signal to support async initialization
        event Action OnReady;

        // Ready indicates that feature initialization is complete
        bool Ready { get; }
    }

    public class LazyModel : ILazyModel
    {
        #region Events
        public event Action OnReady;
        #endregion

        #region State
        public bool Ready { get; private set; }
        #endregion

        #region Lifecycle
        public void Dispose()
        {
            OnReady = null;
        }
        #endregion
        
        #region Private
        // Should be called to complete initialization
        protected void SetReady()
        {
            if (Ready)
            {
                return;
            }

            Ready = true;
            OnReady.Dispatch();
        }
        #endregion
    }
}