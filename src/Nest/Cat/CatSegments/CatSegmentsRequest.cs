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

	public partial class CatSegmentsRequest : PathRequestBase<CatSegmentsRequestParameters>, ICatSegmentsRequest { }

	public partial class CatSegmentsDescriptor : RequestDescriptorBase<CatSegmentsDescriptor, CatSegmentsRequestParameters>, ICatSegmentsRequest { }
}
