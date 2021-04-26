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

using System.Collections.Generic;
using System.Threading;
using Bogus;
using Nest;
using Tests.Configuration;
using Tests.Domain.Helpers;

namespace Tests.Domain
{
	public class Developer : Person
	{
		public Gender Gender { get; set; }

		// not populated by generator. Used by ingest geoip test
		public GeoIp GeoIp { get; set; }
		public string IpAddress { get; set; }
		public string OnlineHandle { get; set; }
		public string PrivateValue { get; set; }

		// @formatter:off — enable formatter after this line
		public static new Faker<Developer> Generator { get; } =
			new Faker<Developer>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Id, p => Interlocked.Increment(ref IdState))
				.RuleFor(p => p.FirstName, p => p.Name.FirstName())
				.RuleFor(p => p.LastName, p => p.Name.LastName())
				.RuleFor(p => p.JobTitle, p => p.Name.JobTitle())
				.RuleFor(p => p.Location, p => new GeoLocation(Gimme.Random.Number(-90, 90), Gimme.Random.Number(-180, 180)))
				.RuleFor(p => p.OnlineHandle, p => p.Internet.UserName())
				.RuleFor(p => p.Gender, p => p.PickRandom<Gender>())
				.RuleFor(p => p.PrivateValue, p => "THIS SHOULD NEVER BE INDEXED")
				.RuleFor(p => p.IpAddress, p => p.Internet.Ip());

		public static IList<Developer> Developers { get; } = Generator.Clone().Generate(1000);
		// @formatter:on — enable formatter after this line
	}
}
