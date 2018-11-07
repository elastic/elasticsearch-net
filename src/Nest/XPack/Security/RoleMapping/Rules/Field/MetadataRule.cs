using System;

namespace Nest
{
	public class MetadataRule : FieldRuleBase
	{
		public MetadataRule(string key, object value) => Metadata = Tuple.Create(key, value);
	}
}
