
using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IFieldStatsRequest
	{
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("index_constraints")]
		IIndexConstraints IndexConstraints { get; set; }
	}

	public partial class FieldStatsRequest
	{
		public Fields Fields { get; set; }
		public IIndexConstraints IndexConstraints { get; set; }
	}

	public partial class FieldStatsDescriptor
	{
		Fields IFieldStatsRequest.Fields { get; set; }
		IIndexConstraints IFieldStatsRequest.IndexConstraints { get; set; }

		public FieldStatsDescriptor Fields(Fields fields) => Assign(a => a.Fields = fields);

		public FieldStatsDescriptor IndexConstraints(Func<IndexConstraintsDescriptor, IPromise<IIndexConstraints>> selector) => 
			Assign(a => a.IndexConstraints = selector?.Invoke(new IndexConstraintsDescriptor())?.Value);
	}
}
