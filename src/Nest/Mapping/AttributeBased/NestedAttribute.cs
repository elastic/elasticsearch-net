using System;

namespace Nest
{
	public class NestedAttribute : ObjectAttribute
	{
		public bool IncludeInParent { get; set; }
		public bool IncludeInRoot { get; set; }

		public override IElasticType ToElasticType() => new NestedType(this);
	}
}
