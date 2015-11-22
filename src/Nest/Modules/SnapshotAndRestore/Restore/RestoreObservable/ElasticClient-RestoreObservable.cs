using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IObservable<IRecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> selector = null);

		/// <inheritdoc/>
		IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<IRecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> restoreSelector = null)
		{
			var restoreDescriptor = restoreSelector.InvokeOrDefault(new RestoreDescriptor(repository, snapshot));
			var observable = new RestoreObservable(this, restoreDescriptor, interval);
			return observable;
		}

		/// <inheritdoc/>
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest)=>
			new RestoreObservable(this, restoreRequest, interval);

	}
}