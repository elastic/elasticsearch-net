using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatCountRequest : IRequest<CatCountRequestParameters> { }

	public partial class CatCountRequest : BasePathRequest<CatCountRequestParameters>, ICatCountRequest { }

	public partial class CatCountDescriptor : BasePathDescriptor<CatCountDescriptor, CatCountRequestParameters>, ICatCountRequest { }
}
