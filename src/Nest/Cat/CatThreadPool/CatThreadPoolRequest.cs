using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatThreadPoolRequest : IRequest<CatThreadPoolRequestParameters> { }

	public partial class CatThreadPoolRequest : PathRequestBase<CatThreadPoolRequestParameters>, ICatThreadPoolRequest { }

	public partial class CatThreadPoolDescriptor : PathDescriptorBase<CatThreadPoolDescriptor, CatThreadPoolRequestParameters>, ICatThreadPoolRequest { }
}
