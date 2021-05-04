// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
