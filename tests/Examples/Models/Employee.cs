// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Examples.Models
{
	public class Employee
	{
		public int? Age { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		[DataMember(Name = "employee-id")]
		public string EmployeeId { get; set; }
	}
}
