using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataStringFormat? Format { get; set; }
	}

	public class FieldDataStringMappingDescriptor
	{
		internal FieldDataStringMapping FieldData { get; private set; }

		public FieldDataStringMappingDescriptor()
		{
			this.FieldData = new FieldDataStringMapping();
		}
		
		public FieldDataStringMappingDescriptor Format(FieldDataStringFormat format)
		{
			this.FieldData.Format = format;
			return this;
		}

		public FieldDataStringMappingDescriptor Loading(FieldDataLoading loading)
		{
			this.FieldData.Loading = loading;
			return this;
		}

		public FieldDataStringMappingDescriptor Filter(Func<FieldDataQueryDescriptor, FieldDataQueryDescriptor> filterSelector)
		{
			var selector = filterSelector(new FieldDataQueryDescriptor());
			this.FieldData.Filter = selector.Filter;
			return this;
		}
	}
}
