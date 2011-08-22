using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.TestData.Domain
{
    public class ElasticSearchProject
    {
        public int Id { get; set;  }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Content { get; set; } //need this specifically for us - JWD 20110822
        public List<Person> Followers { get; set; }
		public GeoLocation Origin { get; set; }
    }
}
