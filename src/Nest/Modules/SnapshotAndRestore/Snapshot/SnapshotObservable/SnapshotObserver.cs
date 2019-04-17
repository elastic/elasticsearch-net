using System;

namespace Nest
{
	public class SnapshotObserver : CoordinatedRequestObserverBase<SnapshotStatusResponse>
	{
		public SnapshotObserver(
			Action<SnapshotStatusResponse> onNext = null,
			Action<Exception> onError = null,
			Action completed = null
		)
			: base(onNext, onError, completed) { }
	}
}
