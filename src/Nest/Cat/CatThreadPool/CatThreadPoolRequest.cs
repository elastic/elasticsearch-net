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

	public partial class CatThreadPoolRequest : BasePathRequest<CatThreadPoolRequestParameters>, ICatThreadPoolRequest { }

	public partial class CatThreadPoolDescriptor : BasePathDescriptor<CatThreadPoolDescriptor, CatThreadPoolRequestParameters>, ICatThreadPoolRequest { }
}
