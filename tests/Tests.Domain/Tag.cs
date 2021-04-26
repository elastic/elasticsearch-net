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
using System.Linq;
using Bogus;
using Tests.Configuration;

namespace Tests.Domain
{
	public class Tag
	{
		public DateTime Added { get; set; }

		public static Faker<Tag> Generator { get; } =
			new Faker<Tag>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Name, p => p.Lorem.Words(1).First())
				.RuleFor(p => p.Added, p => p.Date.Recent())
				.Clone();

		public string Name { get; set; }
	}
}
