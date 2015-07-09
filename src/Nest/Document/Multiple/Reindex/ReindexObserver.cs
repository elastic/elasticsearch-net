using System;

namespace Nest
{
	public class ReindexObserver<T> : CoordinatedRequestObserver<IReindexResponse<T>> where T : class
	{
		public ReindexObserver(
			Action<IReindexResponse<T>> onNext = null, 
			Action<Exception> onError = null, 
			Action completed = null)
			: base(onNext, onError, completed)
		{
		}

	}
}