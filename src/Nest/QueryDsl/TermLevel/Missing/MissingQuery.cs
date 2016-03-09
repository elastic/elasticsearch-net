using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MissingQuery>))]
	public interface IMissingQuery : IQuery
	{
		[JsonProperty(PropertyName = "field")]
		Field Field { get; set; }

		[JsonProperty(PropertyName = "existence")]
		bool? Existence { get; set; }

		[JsonProperty(PropertyName = "null_value")]
		bool? NullValue { get; set; }
	}

	public class MissingQuery : QueryBase, IMissingQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public bool? Existence { get; set; }
		public bool? NullValue { get; set; }
		public Field Field { get; set; }
		internal override void InternalWrapInContainer(IQueryContainer container) => container.Missing = this;
		internal static bool IsConditionless(IMissingQuery q) => q.Field.IsConditionless();
	}

	public class MissingQueryDescriptor<T> 
		: QueryDescriptorBase<MissingQueryDescriptor<T>, IMissingQuery>
		, IMissingQuery where T : class
	{
		protected override bool Conditionless => MissingQuery.IsConditionless(this);
		bool? IMissingQuery.Existence { get; set; }
		bool? IMissingQuery.NullValue { get; set; }
		Field IMissingQuery.Field { get; set; }

		public MissingQueryDescriptor<T> Existence(bool? existence = true) => Assign(a => a.Existence = existence);

		public MissingQueryDescriptor<T> NullValue(bool? nullValue = true) => Assign(a => a.NullValue = nullValue);

		public MissingQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);
		public MissingQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
