using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<RegexpQuery, IRegexpQuery>))]
	public interface IRegexpQuery : IFieldNameQuery
	{
		[DataMember(Name = "flags")]
		string Flags { get; set; }

		[DataMember(Name = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		[DataMember(Name = "value")]
		string Value { get; set; }
	}

	public class RegexpQuery : FieldNameQueryBase, IRegexpQuery
	{
		public string Flags { get; set; }
		public int? MaximumDeterminizedStates { get; set; }
		public string Value { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Regexp = this;

		internal static bool IsConditionless(IRegexpQuery q) => q.Field.IsConditionless() || q.Value.IsNullOrEmpty();
	}

	public class RegexpQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<RegexpQueryDescriptor<T>, IRegexpQuery, T>
			, IRegexpQuery where T : class
	{
		protected override bool Conditionless => RegexpQuery.IsConditionless(this);
		string IRegexpQuery.Flags { get; set; }
		int? IRegexpQuery.MaximumDeterminizedStates { get; set; }
		string IRegexpQuery.Value { get; set; }

		public RegexpQueryDescriptor<T> MaximumDeterminizedStates(int? maxDeterminizedStates) =>
			Assign(maxDeterminizedStates, (a, v) => a.MaximumDeterminizedStates = v);

		public RegexpQueryDescriptor<T> Value(string regex) => Assign(regex, (a, v) => a.Value = v);

		public RegexpQueryDescriptor<T> Flags(string flags) => Assign(flags, (a, v) => a.Flags = v);
	}
}
