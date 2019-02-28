using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetRoleMappingResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackRoleMapping> RoleMappings { get; }
	}

	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetRoleMappingResponse, string, XPackRoleMapping>))]
	public class GetRoleMappingResponse : DictionaryResponseBase<string, XPackRoleMapping>, IGetRoleMappingResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackRoleMapping> RoleMappings => Self.BackingDictionary;
	}

	//only used by GetRoleMappingResponse thus private setters and IReadOnlyCollection
	public class XPackRoleMapping
	{
		[DataMember(Name ="enabled")]
		public bool Enabled { get; private set; }

		[DataMember(Name ="metadata")]
		public IDictionary<string, object> Metadata { get; private set; }

		[DataMember(Name ="roles")]
		public IReadOnlyCollection<string> Roles { get; private set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name ="rules")]
		public RoleMappingRuleBase Rules { get; private set; }
	}
}
