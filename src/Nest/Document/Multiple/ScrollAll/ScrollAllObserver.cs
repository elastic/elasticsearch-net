using System;

namespace Nest_5_2_0
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
