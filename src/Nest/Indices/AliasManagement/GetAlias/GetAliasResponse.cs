// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetAliasResponse, IndexName, IndexAliases>))]
	public class GetAliasResponse : DictionaryResponseBase<IndexName, IndexAliases>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexAliases> Indices => Self.BackingDictionary;

		public override bool IsValid => base.IsValid || Indices.Count > 0;
	}

	public class IndexAliases
	{
		[DataMember(Name ="aliases")]
		public IReadOnlyDictionary<string, AliasDefinition> Aliases { get; internal set; } = EmptyReadOnly<string, AliasDefinition>.Dictionary;
	}

}
