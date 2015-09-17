using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeExistsRequest : IRequest<TypeExistsRequestParameters> { }

	public partial class TypeExistsRequest : RequestBase<TypeExistsRequestParameters>, ITypeExistsRequest
	{
	}

	[DescriptorFor("IndicesExistsType")]
	public partial class TypeExistsDescriptor : RequestDescriptorBase<TypeExistsDescriptor, TypeExistsRequestParameters>, ITypeExistsRequest
	{
	}
}
