// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetRoleResponse, string, XPackRole>))]
	public class GetRoleResponse : DictionaryResponseBase<string, XPackRole>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackRole> Roles => Self.BackingDictionary;
	}
}
