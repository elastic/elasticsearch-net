using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest)
		{
			restoreRequest.ThrowIfNull("restoreRequest");
			var observable = new RestoreObservable(this, restoreRequest);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> restoreSelector = null)
		{
			restoreSelector.ThrowIfNull("restoreSelector");

			var restoreDescriptor = restoreSelector(new RestoreDescriptor());
			var observable = new RestoreObservable(this, restoreDescriptor);
			return observable;
		}
	}
}