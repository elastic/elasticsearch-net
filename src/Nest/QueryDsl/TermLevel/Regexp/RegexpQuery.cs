using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<RegexpQuery>))]
	public interface IRegexpQuery : IFieldNameQuery
	{
		[JsonProperty("value")]
		string Value { get; set; }

		[JsonProperty("flags")]
		string Flags { get; set; }

		[JsonProperty(PropertyName = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }
	}

	public class RegexpQuery : FieldNameQueryBase, IRegexpQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public string Value { get; set; }
		public string Flags { get; set; }
		public int? MaximumDeterminizedStates { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Regexp = this;
		internal static bool IsConditionless(IRegexpQuery q) => q.Field.IsConditionless() || q.Value.IsNullOrEmpty();
	}

	public class RegexpQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<RegexpQueryDescriptor<T>, IRegexpQuery, T>
		, IRegexpQuery where T : class
	{
		protected override bool Conditionless => RegexpQuery.IsConditionless(this);
		string IRegexpQuery.Value { get; set; }
		string IRegexpQuery.Flags { get; set; }
		int? IRegexpQuery.MaximumDeterminizedStates { get; set; }

		public RegexpQueryDescriptor<T> MaximumDeterminizedStates(int maxDeterminizedStates) =>
			Assign(a => a.MaximumDeterminizedStates = maxDeterminizedStates);

		public RegexpQueryDescriptor<T> Value(string regex) => Assign(a => a.Value = regex);

		public RegexpQueryDescriptor<T> Flags(string flags) => Assign(a => a.Flags = flags);
	}
}
