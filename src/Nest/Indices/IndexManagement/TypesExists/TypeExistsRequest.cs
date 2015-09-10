using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeExistsRequest : IIndexTypePath<TypeExistsRequestParameters> { }

	public partial class TypeExistsRequest : IndexTypePathBase<TypeExistsRequestParameters>, ITypeExistsRequest
	{
		public TypeExistsRequest(IndexName index, TypeName typeNameMarker) : base(index, typeNameMarker) { }
	}

	[DescriptorFor("IndicesExistsType")]
	public partial class TypeExistsDescriptor : IndexTypePathDescriptor<TypeExistsDescriptor, TypeExistsRequestParameters>, ITypeExistsRequest
	{
	}
}
