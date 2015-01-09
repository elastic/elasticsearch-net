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

		[JsonProperty(PropertyName = "existence")]
		bool? Existence { get; set; }

		[JsonProperty(PropertyName = "null_value")]
		bool? NullValue { get; set; }
	}

	public class MissingFilter : PlainFilter, IMissingFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Missing = this;
		}

		public PropertyPathMarker Field { get; set; }

		public bool? Existence { get; set; }

		public bool? NullValue { get; set; }
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

		private IMissingFilter Self { get { return this; } }

		PropertyPathMarker IMissingFilter.Field { get; set;}
		bool? IMissingFilter.Existence { get; set; }
		bool? IMissingFilter.NullValue { get; set; }

		public MissingFilterDescriptor Existence(bool existence = true)
		{
			Self.Existence = existence;
			return this;
		}

		public MissingFilterDescriptor NullValue(bool nullValue = true)
		{
			Self.NullValue = nullValue;
			return this;
		}
	}
}
