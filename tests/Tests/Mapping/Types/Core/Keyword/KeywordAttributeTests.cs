/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
