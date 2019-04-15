using System;

namespace Nest
{
	public class RestoreObserver : CoordinatedRequestObserverBase<RecoveryStatusResponse>
	{
		public RestoreObserver(
			Action<RecoveryStatusResponse> onNext = null,
			Action<Exception> onError = null,
			Action completed = null
		)
			: base(onNext, onError, completed) { }
	}
}
