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

namespace Tests.Mapping.Types.Specialized.Ip
{
	public class IpTest
	{
		[Ip(
			Index = false,
			NullValue = "127.0.0.1")]
		public string Full { get; set; }

		[Ip]
		public string Minimal { get; set; }
	}

	public class IpAttributeTests : AttributeTestsBase<IpTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "ip",
					index = false,
					null_value = "127.0.0.1"
				},
				minimal = new
				{
					type = "ip"
				}
			}
		};
	}
}
