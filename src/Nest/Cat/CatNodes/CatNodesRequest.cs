using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatNodesRequest : IRequest<CatNodesRequestParameters> { }

	public partial class CatNodesRequest : PathRequestBase<CatNodesRequestParameters>, ICatNodesRequest { }

	public partial class CatNodesDescriptor : PathDescriptorBase<CatNodesDescriptor, CatNodesRequestParameters>, ICatNodesRequest { }
}
