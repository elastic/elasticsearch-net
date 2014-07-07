using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsFilter : IFilter
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class ExistsFilter : PlainFilter, IExistsFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Exists = this;
		}

		public PropertyPathMarker Field { get; set; }
	}

	public class ExistsFilterDescriptor : FilterBase, IExistsFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IExistsFilter)this).Field.IsConditionless();
			}
		}

		PropertyPathMarker IExistsFilter.Field { get; set;}
		
	}
}
