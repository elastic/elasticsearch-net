using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<TermQuery>))]
	public interface ITermQuery : IFieldNameQuery
	{
		[DataMember(Name ="value")]
		[JsonConverter(typeof(SourceValueWriteConverter))]
		object Value { get; set; }
	}

	public class TermQuery : FieldNameQueryBase, ITermQuery
	{
		[JsonConverter(typeof(SourceValueWriteConverter))]
		public object Value { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Term = this;

		internal static bool IsConditionless(ITermQuery q) => q.Value == null || q.Value.ToString().IsNullOrEmpty() || q.Field.IsConditionless();
	}

	public abstract class TermQueryDescriptorBase<TDescriptor, TInterface, T>
		: FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>
			, ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ITermQuery
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

	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, ITermQuery, T>
		where T : class { }
}
