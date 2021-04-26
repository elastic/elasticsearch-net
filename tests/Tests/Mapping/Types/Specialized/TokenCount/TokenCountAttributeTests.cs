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

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountTest
	{
		[TokenCount(
			Index = false,
			Analyzer = "standard",
			Boost = 1.2,
			EnablePositionIncrements = false,
			NullValue = 0)]
		public int Full { get; set; }

		[TokenCount]
		public int Minimal { get; set; }
	}

	public class TokenCountAttributeTests : AttributeTestsBase<TokenCountTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "token_count",
					analyzer = "standard",
					enable_position_increments = false,
					index = false,
					boost = 1.2,
					null_value = 0.0,
				},
				minimal = new
				{
					type = "token_count"
				}
			}
		};
	}
}
