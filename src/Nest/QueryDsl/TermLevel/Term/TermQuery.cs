using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryConverter<TermQuery>))]
	public interface ITermQuery : IFieldNameQuery
	{
		[JsonProperty("value")]
		object Value { get; set; }
		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	public class TermQuery : FieldNameQuery, ITermQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public object Value { get; set; }
		public double? Boost { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Term = this;
		internal static bool IsConditionless(ITermQuery q) => q.Value == null || q.Value.ToString().IsNullOrEmpty() || q.Field.IsConditionless();
	}

	public class TermQueryDescriptorBase<TDescriptor, T> : ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, T>
		where T : class
	{
		private ITermQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => TermQuery.IsConditionless(this);
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
		object ITermQuery.Value { get; set; }
		double? ITermQuery.Boost { get; set; }

		public TDescriptor Name(string name)
		{
			Self.Name = name;
			return (TDescriptor) this;
		}

		public TDescriptor OnField(string field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}

		public TDescriptor OnField(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}

		public TDescriptor OnField<K>(Expression<Func<T, K>> field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}

		public TDescriptor Value(object value)
		{
			Self.Value = value;
			return (TDescriptor)this;
		}

		public TDescriptor Boost(double boost)
		{
			Self.Boost = boost;
			return (TDescriptor)this;
		}
	}

	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, T>
		where T : class
	{	
	}
}
