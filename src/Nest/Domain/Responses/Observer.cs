using System;

namespace Nest
{
    public class Observer<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _completed;

        public Observer(Action<T> onNext = null, Action<Exception> onError = null, Action completed = null)
        {
            _onNext = onNext;
            _onError = onError;
            _completed = completed;
        }

        public void OnNext(T value)
        {
            if (this._onNext != null) this._onNext(value);
        }

        public void OnError(Exception error)
        {
            if (this._onError != null) this._onError(error);
        }

        public void OnCompleted()
        {
            if (this._completed != null) this._completed();
        }
    }
}