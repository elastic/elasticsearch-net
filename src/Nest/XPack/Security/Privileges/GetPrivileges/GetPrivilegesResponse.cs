// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetPrivilegesResponse, string, IDictionary<string, PrivilegesActions>>))]
	public class GetPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PrivilegesActions>>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications => Self.BackingDictionary;
	}
}
