using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MatchAllQuery>))]
	public interface IMatchAllQuery : IQuery
	{
		[JsonProperty(PropertyName = "norm_field")]
		string NormField { get; set; }
	}

	public class MatchAllQuery : QueryBase, IMatchAllQuery
	{
		public string NormField { get;  set; }

		protected override bool Conditionless => false;

		internal override void InternalWrapInContainer(IQueryContainer container)
		{
			container.MatchAll = this;
		}
	}

	public class MatchAllQueryDescriptor
		: QueryDescriptorBase<MatchAllQueryDescriptor, IMatchAllQuery>
		, IMatchAllQuery 
	{
		protected override bool Conditionless => false;

		string IMatchAllQuery.NormField { get; set; }

		public MatchAllQueryDescriptor NormField(string normField) => Assign(a => a.NormField = normField);
	}
}
