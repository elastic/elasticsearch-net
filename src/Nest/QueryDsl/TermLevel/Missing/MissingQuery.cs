using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MissingQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMissingQuery : IFieldNameQuery
	{

		[JsonProperty(PropertyName = "existence")]
		bool? Existence { get; set; }

		[JsonProperty(PropertyName = "null_value")]
		bool? NullValue { get; set; }
	}

	public class MissingQuery : FieldNameQueryBase, IMissingQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public bool? Existence { get; set; }
		public bool? NullValue { get; set; }
		protected override void WrapInContainer(IQueryContainer container) => container.Missing = this;
		internal static bool IsConditionless(IMissingQuery q) => q.Field.IsConditionless();
	}

	public class MissingQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<MissingQueryDescriptor<T>, IMissingQuery, T>
		, IMissingQuery where T : class
	{
		bool IQuery.Conditionless => MissingQuery.IsConditionless(this);
		bool? IMissingQuery.Existence { get; set; }
		bool? IMissingQuery.NullValue { get; set; }

		public MissingQueryDescriptor<T> Existence(bool existence = true) => Assign(a => a.Existence = existence);

		public MissingQueryDescriptor<T> NullValue(bool nullValue = true) => Assign(a => a.NullValue = nullValue);
	}
}
