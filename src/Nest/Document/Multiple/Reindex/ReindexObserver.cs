using System;

namespace Nest
{
	public class ReindexObserver<T> : CoordinatedRequestObserverBase<IReindexResponse<T>> where T : class
	{
		public Action<IHit<T>, T, IBulkIndexOperation<T>> Alter { get; }

		public ReindexObserver(
			Action<IReindexResponse<T>> onNext = null,
			Action<Exception> onError = null,
			System.Action onCompleted = null,
			Action<IHit<T>, T, IBulkIndexOperation<T>> alter = null)
			: base(onNext, onError, onCompleted)
		{
			this.Alter = null;
		}

	}
}
