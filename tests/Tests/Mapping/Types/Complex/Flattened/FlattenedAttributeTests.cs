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

using Nest;

namespace Tests.Mapping.Types.Complex.Flattened
{
	public class FlattenedTest
	{
		[Flattened(
			Boost = 2,
			IgnoreAbove = 256,
			Index = true,
			Similarity = "BM25",
			DepthLimit = 5,
			DocValues = true,
			IndexOptions = IndexOptions.Freqs,
			NullValue = "N/A",
			EagerGlobalOrdinals = true,
			SplitQueriesOnWhitespace = true)]
		public InnerObject Full { get; set; }

		[Flattened]
		public InnerObject Minimal { get; set; }

		public class InnerObject
		{
			public string Name { get; set; }
		}
	}

	public class FlattenedAttributeTests : AttributeTestsBase<FlattenedTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "flattened",
					boost = 2.0,
					ignore_above = 256,
					index = true,
					similarity = "BM25",
					depth_limit = 5,
					doc_values = true,
					index_options = "freqs",
					null_value = "N/A",
					eager_global_ordinals = true,
					split_queries_on_whitespace = true
				},
				minimal = new
				{
					type = "flattened"
				}
			}
		};
	}
}
