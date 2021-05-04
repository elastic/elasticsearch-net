// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetRoleMappingResponse, string, XPackRoleMapping>))]
	public class GetRoleMappingResponse : DictionaryResponseBase<string, XPackRoleMapping>
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
