using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBinaryProperty : IProperty
	{
	}

	public class BinaryProperty : Property, IBinaryProperty
	{
		public BinaryProperty() : base("binary") { }
	}

	public class BinaryPropertyDescriptor<T> 
		: PropertyDescriptorBase<BinaryPropertyDescriptor<T>, IBinaryProperty, T>, IBinaryProperty
		where T : class
	{
		public BinaryPropertyDescriptor() : base("binary") { }
	}
}