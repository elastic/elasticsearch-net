using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldStatsRequest : IRequest<FieldStatsRequestParameters> { }

	public partial class FieldStatsRequest : RequestBase<FieldStatsRequestParameters>, IFieldStatsRequest
	{
	}

	public partial class FieldStatsDescriptor 
		: RequestDescriptorBase<FieldStatsDescriptor, FieldStatsRequestParameters>
		, IFieldStatsRequest
	{
	}
}
