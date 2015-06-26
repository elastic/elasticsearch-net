using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

	public class MurmurHashMappingDescriptor<T> where T : class
	{
		internal Murmur3HashMapping _Mapping = new Murmur3HashMapping();

		public MurmurHashMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

		public MurmurHashMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
	}
}
