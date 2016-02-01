using System;

namespace Nest
{
	public abstract class CoordinatedRequestObserverBase<T> : IObserver<T>
	{
		private readonly Action<T> _onNext;
		private readonly Action<Exception> _onError;
		private readonly Action _completed;

	    protected CoordinatedRequestObserverBase(Action<T> onNext = null, Action<Exception> onError = null, Action completed = null)
		{
			_onNext = onNext;
			_onError = onError;
			_completed = completed;
		}

		public void OnNext(T value)
		{
		    this._onNext?.Invoke(value);
		}

	    public void OnError(Exception error)
	    {
	        this._onError?.Invoke(error);
	    }

	    public void OnCompleted()
	    {
	        this._completed?.Invoke();
	    }
	}
}