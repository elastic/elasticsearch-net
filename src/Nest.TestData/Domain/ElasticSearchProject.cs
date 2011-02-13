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
        public List<Person> Folllowers { get; set; }
    }
}
