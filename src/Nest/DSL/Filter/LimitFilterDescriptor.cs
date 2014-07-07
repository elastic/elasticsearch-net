using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<LimitFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ILimitFilter : IFilter
	{
		[JsonProperty(PropertyName = "value")]
		int? Value { get; set; }
	}
	
	public class LimitFilter : PlainFilter, ILimitFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Limit = this;
		}

		public int? Value { get; set; }
	}

	public class LimitFilterDescriptor : FilterBase, ILimitFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return !((ILimitFilter)this).Value.HasValue;
			}

		}

		int? ILimitFilter.Value { get; set;}
	}
}
