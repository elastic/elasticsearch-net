using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
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
			return this.Dispatch<BulkDescriptor, BulkQueryString, BulkResponse>(
				bulkSelector,
				(p, d) =>
				{
					var json = this.Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatch(p, json);
				}
			);
		}

		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.DispatchAsync<BulkDescriptor, BulkQueryString, BulkResponse, IBulkResponse>(
				bulkSelector,
				(p, d) =>
				{
					var json = this.Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatchAsync(p, json);
				}
			);
		}

	}
}
