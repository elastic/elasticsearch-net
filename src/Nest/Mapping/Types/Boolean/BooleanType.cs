using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{

	[JsonObject(MemberSerialization.OptIn)]
	public interface IBooleanType : IElasticType
	{
		[JsonProperty("index")]
		NonStringIndexOption? Index { get; set; }
		[JsonProperty("boost")]
		double? Boost { get; set; }
		[JsonProperty("null_value")]
		bool? NullValue { get; set; }
	}
	
	public class BooleanType : ElasticType, IBooleanType
	{
		public BooleanType() : base("boolean") { }
		
		public NonStringIndexOption? Index { get; set; }
		public double? Boost { get; set; }
		public bool? NullValue { get; set; }
	}

	public class BooleanTypeDescriptor<T>
		: TypeDescriptorBase<BooleanTypeDescriptor<T>, IBooleanType, T>, IBooleanType
		where T : class
	{
		double? IBooleanType.Boost { get; set; }
		NonStringIndexOption? IBooleanType.Index { get; set; }
		bool? IBooleanType.NullValue { get; set; }

		public BooleanTypeDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public BooleanTypeDescriptor<T> Index(NonStringIndexOption index) => Assign(a => a.Index = index);
		public BooleanTypeDescriptor<T> NullValue(bool nullValue) => Assign(a => a.NullValue = nullValue);
	}
}