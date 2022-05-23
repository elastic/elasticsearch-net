// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Nest;

namespace PlaygroundV7x
{
	public class Person
	{
		public int Id { get; set; }

		public Guid SecondaryId { get; set; } = Guid.NewGuid();
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public int? Age { get; init; }
		//public Routing? Routing { get; init; }

		public Id Idv3 => "testing";
		//public Guid Routing { get; init; } = Guid.NewGuid();

		public string? Email { get; init; }

		public string Data { get; init; } = "NOTHING";

		public DateTime Date { get; set; }

		public Guid Guid { get; set; }
	}
}
