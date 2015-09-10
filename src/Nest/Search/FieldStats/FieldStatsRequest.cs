using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldStatsRequest : IIndicesOptionalExplicitAllPath<FieldStatsRequestParameters> { }

	public partial class FieldStatsRequest : IndicesOptionalExplicitAllPathBase<FieldStatsRequestParameters>, IFieldStatsRequest
	{
		public FieldStatsRequest(Indices indices) : base(indices) { }
	}

	public partial class FieldStatsDescriptor 
		: IndicesOptionalExplicitAllPathDescriptor<FieldStatsDescriptor, FieldStatsRequestParameters>
		, IFieldStatsRequest
	{
		public FieldStatsDescriptor(Indices indices) : base(indices) { }
	}
}
