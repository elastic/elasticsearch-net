// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<DeletePrivilegesResponse, string, IDictionary<string, FoundUserPrivilege>>))]
	public class DeletePrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, FoundUserPrivilege>>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, FoundUserPrivilege>> Applications => Self.BackingDictionary;
	}

	public class FoundUserPrivilege
	{
		[DataMember(Name = "found")]
		public bool Found { get; internal set; }
	}
}
