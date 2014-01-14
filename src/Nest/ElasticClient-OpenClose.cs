using System;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			openIndexSelector.ThrowIfNull("openIndexSelector");
			var descriptor = openIndexSelector(new OpenIndexDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesOpenDispatch(pathInfo)
				.Deserialize<IndicesOperationResponse>();
		}

		public Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			openIndexSelector.ThrowIfNull("openIndexSelector");
			var descriptor = openIndexSelector(new OpenIndexDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesOpenDispatchAsync(pathInfo)
				.ContinueWith<IIndicesOperationResponse>(t => t.Result.Deserialize<IndicesOperationResponse>());
		}

		public IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			closeIndexSelector.ThrowIfNull("closeIndexSelector");
			var descriptor = closeIndexSelector(new CloseIndexDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesCloseDispatch(pathInfo)
				.Deserialize<IndicesOperationResponse>();
		}

		public Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			closeIndexSelector.ThrowIfNull("closeIndexSelector");
			var descriptor = closeIndexSelector(new CloseIndexDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesCloseDispatchAsync(pathInfo)
				.ContinueWith<IIndicesOperationResponse>(t => t.Result.Deserialize<IndicesOperationResponse>());
		}


	}
}
