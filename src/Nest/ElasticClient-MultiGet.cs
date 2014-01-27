using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{

	public partial class ElasticClient
	{

		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			descriptor._GetOperations.ThrowIfEmpty("MultiGetFull called but no get operations were specified");
			var pathInfo = ((IPathInfo<MultiGetQueryString>)descriptor).ToPathInfo(this._connectionSettings);
			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			var status = this.RawDispatch.MgetDispatch(pathInfo, descriptor);

			var multiGetResponse = this.Serializer.DeserializeInternal<MultiGetResponse>(
				status,
				piggyBackJsonConverter: multiGetHitConverter
			);

			return multiGetResponse;
		}
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			descriptor._GetOperations.ThrowIfEmpty("MultiGetFull called but no get operations were specified");
			var pathInfo = ((IPathInfo<MultiGetQueryString>)descriptor).ToPathInfo(this._connectionSettings);
			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			return this.RawDispatch.MgetDispatchAsync(pathInfo, descriptor)
				.ContinueWith<IMultiGetResponse>(t =>
				{
					var status = t.Result;
					var multiGetResponse = this.Serializer.DeserializeInternal<MultiGetResponse>(
						status,
						piggyBackJsonConverter: multiGetHitConverter
						);

					return multiGetResponse;
				});
		}

	}
}
