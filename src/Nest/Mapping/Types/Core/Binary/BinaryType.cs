using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBinaryType : IElasticType
	{
	}

	public class BinaryType : ElasticType, IBinaryType
	{
		public BinaryType() : base("binary") { }
		internal BinaryType(BinaryAttribute attribute) : base("binary", attribute) { }
	}

	public class BinaryTypeDescriptor<T> 
		: TypeDescriptorBase<BinaryTypeDescriptor<T>, IBinaryType, T>, IBinaryType
		where T : class
	{
	}
}