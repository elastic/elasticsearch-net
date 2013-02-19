using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Tests.MockData.Domain
{
	public class Person
	{
		public int Id { get; set; }
    [ElasticProperty(Index = FieldIndexOption.analyzed)]
		public string FirstName { get; set;  }
    [ElasticProperty(Index = FieldIndexOption.analyzed)]
		public string LastName { get; set;  }
		public int Age { get; set;  }
		public string Email { get; set;  }
		public DateTime DateOfBirth { get; set; }
		public GeoLocation PlaceOfBirth { get; set; }
	}
}
