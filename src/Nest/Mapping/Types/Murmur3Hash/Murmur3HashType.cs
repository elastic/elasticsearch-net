using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public interface IMurmur3HashType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class Murmur3HashType : ElasticType, IMurmur3HashType
	{
		public Murmur3HashType() : base("murmur3") { }
	}

	public class Murmur3HashTypeDescriptor<T> where T : class
	{
		internal Murmur3HashType _Mapping = new Murmur3HashType();

		public Murmur3HashTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

		public Murmur3HashTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
	}
}
