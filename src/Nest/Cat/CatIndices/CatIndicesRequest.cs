using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatIndicesRequest : IRequest<CatIndicesRequestParameters> { }

	public partial class CatIndicesRequest : BasePathRequest<CatIndicesRequestParameters>, ICatIndicesRequest { }

	public partial class CatIndicesDescriptor : BasePathDescriptor<CatIndicesDescriptor, CatIndicesRequestParameters>, ICatIndicesRequest { }
}
