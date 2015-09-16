using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatHealthRequest : IRequest<CatHealthRequestParameters> { }

	public partial class CatHealthRequest : RequestBase<CatHealthRequestParameters>, ICatHealthRequest { }

	public partial class CatHealthDescriptor : RequestDescriptorBase<CatHealthDescriptor, CatHealthRequestParameters>, ICatHealthRequest { }
}
