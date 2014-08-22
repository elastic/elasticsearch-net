
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
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

		public FieldDataNonStringMappingDescriptor Filter(Func<FieldDataFilterDescriptor, FieldDataFilterDescriptor> filterSelector)
		{
			var selector = filterSelector(new FieldDataFilterDescriptor());
			this.FieldData.Filter = selector.Filter;
			return this;
		}
	}
}
