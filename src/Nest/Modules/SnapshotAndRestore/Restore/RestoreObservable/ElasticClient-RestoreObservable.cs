using System;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IObservable<IRecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval, Func<RestoreDescriptor, IRestoreRequest> selector = null);

		/// <inheritdoc/>
		IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<IRecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval, Func<RestoreDescriptor, IRestoreRequest> selector = null)
		{
			var restoreDescriptor = selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshot));
			var observable = new RestoreObservable(this, restoreDescriptor, interval);
			return observable;
		}

		/// <inheritdoc/>
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest request)=>
			new RestoreObservable(this, request, interval);
	}
}