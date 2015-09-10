using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatSegmentsRequest : IRequest<CatSegmentsRequestParameters> { }

	public partial class CatSegmentsRequest : BasePathRequest<CatSegmentsRequestParameters>, ICatSegmentsRequest { }

	public partial class CatSegmentsDescriptor : BasePathDescriptor<CatSegmentsDescriptor, CatSegmentsRequestParameters>, ICatSegmentsRequest { }
}
