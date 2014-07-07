using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<MissingFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMissingFilter : IFilter
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class MissingFilter : PlainFilter, IMissingFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Missing = this;
		}

		public PropertyPathMarker Field { get; set; }
	}

	public class MissingFilterDescriptor : FilterBase, IMissingFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IMissingFilter)this).Field.IsConditionless();
			}

		}

		PropertyPathMarker IMissingFilter.Field { get; set;}
	}
}
