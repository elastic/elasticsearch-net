using System;

namespace Nest_5_2_0
{
	public class SnapshotObserver : CoordinatedRequestObserverBase<ISnapshotStatusResponse>
	{
		public SnapshotObserver(
			Action<ISnapshotStatusResponse> onNext = null,
			Action<Exception> onError = null,
			Action completed = null)
			: base(onNext, onError, completed)
		{

		}
	}
}
