using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatShardsRequest : IRequest<CatShardsRequestParameters> { }
	 
	public partial class CatShardsRequest : PathRequestBase<CatShardsRequestParameters>, ICatShardsRequest { }

	public partial class CatShardsDescriptor : PathDescriptorBase<CatShardsDescriptor, CatShardsRequestParameters>, ICatShardsRequest { }
}
