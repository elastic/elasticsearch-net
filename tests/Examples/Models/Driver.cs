// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest;

namespace Examples.Models
{
	public class Driver
	{
		[PropertyName("last_name")]
		public string LastName { get; set; }

		public IEnumerable<Vehicle> Vehicle { get; set; }
	}
}
