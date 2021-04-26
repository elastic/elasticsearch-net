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
	public class Person
	{
		protected static int IdState;
		public string FirstName { get; set; }

		public static Faker<Person> Generator { get; } =
			new Faker<Person>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Id, p => Interlocked.Increment(ref IdState))
				.RuleFor(p => p.FirstName, p => p.Name.FirstName())
				.RuleFor(p => p.LastName, p => p.Name.LastName())
				.RuleFor(p => p.JobTitle, p => p.Name.JobTitle())
				.RuleFor(p => p.Location, p => new GeoLocation(Gimme.Random.Number(-90, 90), Gimme.Random.Number(-180, 180)));

		public long Id { get; set; }
		public string JobTitle { get; set; }
		public string LastName { get; set; }
		public GeoLocation Location { get; set; }

		public static IList<Person> People { get; } = Generator.Clone().Generate(1000);
	}
}
