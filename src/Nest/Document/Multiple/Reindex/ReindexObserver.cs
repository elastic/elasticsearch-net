using System;

namespace Nest
{
	public class ReindexObserver<T> : CoordinatedRequestObserverBase<IReindexResponse<T>> where T : class
	{
		public Action<IHit<T>, T, IBulkIndexOperation<T>> Alter { get; }

		public ReindexObserver(
			Action<IReindexResponse<T>> onNext = null,
			Action<Exception> onError = null,
			Action completed = null,
			Action<IHit<T>, T, IBulkIndexOperation<T>> alter = null)
			: base(onNext, onError, completed)
		{
			this.Alter = null;
		}

	}
}
