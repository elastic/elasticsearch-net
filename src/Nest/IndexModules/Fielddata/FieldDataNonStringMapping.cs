using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataNonStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataNonStringFormat? Format { get; set; }

		[JsonProperty("precision")]
		public GeoPrecision Precision { get; set; }
	}

	public class FieldDataNonStringMappingDescriptor
	{
		internal FieldDataNonStringMapping FieldData { get; private set; }

		public FieldDataNonStringMappingDescriptor()
		{
			this.FieldData = new FieldDataNonStringMapping();
		}
		
		public FieldDataNonStringMappingDescriptor Format(FieldDataNonStringFormat format)
		{
			this.FieldData.Format = format;
			return this;
		}

		public FieldDataNonStringMappingDescriptor Loading(FieldDataLoading loading)
		{
			this.FieldData.Loading = loading;
			return this;
		}

		public FieldDataNonStringMappingDescriptor Filter(Func<FieldDataQueryDescriptor, FieldDataQueryDescriptor> filterSelector)
		{
			var selector = filterSelector(new FieldDataQueryDescriptor());
			this.FieldData.Filter = selector.Filter;
			return this;
		}

		public FieldDataNonStringMappingDescriptor Precision(double precision, GeoPrecisionUnit unit)
		{
			this.FieldData.Precision = new GeoPrecision(precision, unit);
			return this;
		}
	}
}
