using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.MockData.Domain
{
	public class Person
	{
		public int Id { get; set; }
		[ElasticProperty(Index = FieldIndexOption.Analyzed)]
		public string FirstName { get; set; }
		[ElasticProperty(Index = FieldIndexOption.Analyzed)]
		public string LastName { get; set; }
		public int Age { get; set; }
		[ElasticProperty(Index = FieldIndexOption.NotAnalyzed)]
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		[ElasticProperty(Type = FieldType.GeoPoint)]
		public GeoLocation PlaceOfBirth { get; set; }
	}
}
