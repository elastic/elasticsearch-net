using System;
using Nest.CommonAbstractions.Reactive;

namespace Nest
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
