using System;
using System.Collections.Generic;

namespace Nest
{
	public class MetadataRule : FieldRuleBase
	{
		public MetadataRule(string key, object value)
		{
			this.Metadata = Tuple.Create(key, value);
		}
	}
}
