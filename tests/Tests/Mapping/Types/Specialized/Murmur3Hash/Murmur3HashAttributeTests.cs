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

namespace Tests.Mapping.Types.Specialized.Murmur3Hash
{
	public class Murmur3HashTest
	{
		[Murmur3Hash]
		public string Full { get; set; }

		[Murmur3Hash]
		public string Minimal { get; set; }
	}

	public class Murmur3HashAttributeTests : AttributeTestsBase<Murmur3HashTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "murmur3",
				},
				minimal = new
				{
					type = "murmur3"
				}
			}
		};
	}
}
