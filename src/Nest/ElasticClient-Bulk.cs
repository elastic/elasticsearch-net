using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Resolvers;
using System.Reflection;
using System.Collections.Concurrent;

namespace Nest
{
	using System.Threading.Tasks;

	public partial class ElasticClient
	{
		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			bulkSelector.ThrowIfNull("bulkSelector");
			var bulkDescriptor = bulkSelector(new BulkDescriptor());
			var json = this.Serializer.SerializeBulkDescriptor(bulkDescriptor);
			var pathInfo = ((IPathInfo<BulkQueryString>) bulkDescriptor).ToPathInfo(this._connectionSettings);
			return this.RawDispatch.BulkDispatch(pathInfo, pathInfo)
				.Deserialize<BulkResponse>();
		}

		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			bulkSelector.ThrowIfNull("bulkSelector");
			var bulkDescriptor = bulkSelector(new BulkDescriptor());
			var json = this.Serializer.SerializeBulkDescriptor(bulkDescriptor);
			var pathInfo = ((IPathInfo<BulkQueryString>) bulkDescriptor).ToPathInfo(this._connectionSettings);
			return this.RawDispatch.BulkDispatchAsync(pathInfo, pathInfo)
				.ContinueWith<IBulkResponse>(t =>this.Deserialize<BulkResponse>(t.Result));
		}

	}
}
