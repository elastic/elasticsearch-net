using System;

namespace Nest
{
	public class ScrollAllObserver<T> : CoordinatedRequestObserverBase<IScrollAllResponse<T>> where T : class
	{
		public ScrollAllObserver(
			Action<IScrollAllResponse<T>> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null)
			: base(onNext, onError, onCompleted) { }

	}


}
