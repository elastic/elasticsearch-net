using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(MatchNoneQuery))]
	public interface IMatchNoneQuery : IQuery { }

	public class MatchNoneQuery : QueryBase, IMatchNoneQuery
	{
		protected override bool Conditionless => false;

		internal override void InternalWrapInContainer(IQueryContainer container) => container.MatchNone = this;
	}

	public class MatchNoneQueryDescriptor
		: QueryDescriptorBase<MatchNoneQueryDescriptor, IMatchNoneQuery>
			, IMatchNoneQuery
	{
		protected override bool Conditionless => false;
	}
}
