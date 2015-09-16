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

	public partial class CatFielddataRequest : RequestBase<CatFielddataRequestParameters>, ICatFielddataRequest { }

	public partial class CatFielddataDescriptor : RequestDescriptorBase<CatFielddataDescriptor, CatFielddataRequestParameters>, ICatFielddataRequest { }
}
