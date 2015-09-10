using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatFielddataRequest : IRequest<CatFielddataRequestParameters> { }

	public partial class CatFielddataRequest : BasePathRequest<CatFielddataRequestParameters>, ICatFielddataRequest { }

	public partial class CatFielddataDescriptor : BasePathDescriptor<CatFielddataDescriptor, CatFielddataRequestParameters>, ICatFielddataRequest { }
}
