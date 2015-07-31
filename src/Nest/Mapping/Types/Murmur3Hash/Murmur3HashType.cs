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

	public class Murmur3HashTypeDescriptor<T> 
		: TypeDescriptorBase<Murmur3HashTypeDescriptor<T>, IMurmur3HashType, T>, IMurmur3HashType
		where T : class
	{
	}
}
