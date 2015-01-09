using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<MatchAllFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMatchAllFilter : IFilter
	{
	}
	
	public class MatchAllFilter : PlainFilter, IMatchAllFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.MatchAll = this;
		}
	}

	public class MatchAllFilterDescriptor : FilterBase, IMatchAllFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return false;
			}

		}
	}
}
