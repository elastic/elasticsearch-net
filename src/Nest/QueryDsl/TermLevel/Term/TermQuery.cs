using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<TermQuery>))]
	public interface ITermQuery : IFieldNameQuery
	{
		[JsonProperty("value")]
		object Value { get; set; }
	}

	public class TermQuery : FieldNameQueryBase, ITermQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public object Value { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Term = this;

		internal static bool IsConditionless(ITermQuery q) => q.Value == null || q.Value.ToString().IsNullOrEmpty() || q.Field.IsConditionless();
	}

	public abstract class TermQueryDescriptorBase<TDescriptor, T> : FieldNameQueryDescriptorBase<TDescriptor, ITermQuery, T>
		, ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, T>
		where T : class
	{
		protected override bool Conditionless => TermQuery.IsConditionless(this);
		object ITermQuery.Value { get; set; }

		public TDescriptor Value(object value)
		{
			Self.Value = value;
			return (TDescriptor)this;
		}
	}

	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, T>
		where T : class
	{	
	}
}
