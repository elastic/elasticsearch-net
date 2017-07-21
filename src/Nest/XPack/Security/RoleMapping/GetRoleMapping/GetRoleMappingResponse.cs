using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRoleMappingResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackRoleMapping> RoleMappings { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetRoleMappingResponse, string, XPackRoleMapping>))]
	public class GetRoleMappingResponse : DictionaryResponseBase<string, XPackRoleMapping>, IGetRoleMappingResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, XPackRoleMapping> RoleMappings => Self.BackingDictionary;
	}

	//only used by GetRoleMappingResponse thus private setters and IReadOnlyCollection
	public class XPackRoleMapping
	{
		[JsonProperty("metadata")]
		public IDictionary<string, object> Metadata { get; private set; }

		[JsonProperty("enabled")]
		public bool Enabled { get; private set; }

		[JsonProperty("roles")]
		public IReadOnlyCollection<string> Roles { get; private set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("rules")]
		public RoleMappingRuleBase Rules { get; private set; }
	}

}
