// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;

namespace Tests.Mapping.Types.Core.Keyword
{
	public class KeywordTest
	{
		public char Char { get; set; }

		[Keyword(
			EagerGlobalOrdinals = true,
			IgnoreAbove = 50,
			Index = false,
			IndexOptions = IndexOptions.Offsets,
			NullValue = "null",
			Norms = false
		)]
		public string Full { get; set; }

		public Guid Guid { get; set; }

		[Keyword]
		public string Minimal { get; set; }
	}

	public class KeywordAttributeTests : AttributeTestsBase<KeywordTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "keyword",
					eager_global_ordinals = true,
					ignore_above = 50,
					index = false,
					index_options = "offsets",
					null_value = "null",
					norms = false
				},
				minimal = new
				{
					type = "keyword"
				},
				@char = new
				{
					type = "keyword"
				},
				@guid = new
				{
					type = "keyword"
				}
			}
		};
	}
}
