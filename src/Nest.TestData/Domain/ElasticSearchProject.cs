using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Mapping;

namespace Nest.TestData.Domain
{
    public class ElasticSearchProject
    {
        public int Id { get; set;  }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Content { get; set; }
		[ElasticProperty(Name="loc")]
		public int LOC { get; set; }
        public List<Person> Followers { get; set; }
		public GeoLocation Origin { get; set; }
    }
}
