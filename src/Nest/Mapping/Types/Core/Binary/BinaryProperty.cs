using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBinaryProperty : IElasticsearchProperty
	{
	}

	public class BinaryProperty : ElasticsearchProperty, IBinaryProperty
	{
		public BinaryProperty() : base("binary") { }

		internal BinaryProperty(BinaryAttribute attribute) : base("binary", attribute) { }
	}

	public class BinaryPropertyDescriptor<T> 
		: PropertyDescriptorBase<BinaryPropertyDescriptor<T>, IBinaryProperty, T>, IBinaryProperty
		where T : class
	{
	}
}