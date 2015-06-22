using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Murmur3HashMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public Murmur3HashMapping() : base("murmur3") { }

		[JsonIgnore]
		public string IndexName { get; set; }
	}
}
