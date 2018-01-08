using System.Collections.Generic;
using ApiGenerator.Overrides.Descriptors;

namespace ApiGenerator.Overrides
{
	public class GlobalOverrides : DescriptorOverridesBase
	{
		/// <summary>
		/// Force these be rendered as interface properties only, so that they'd have to be implemented manually
		/// and become part of the body. This only takes affect for reques
		/// </summary>
		public override IEnumerable<string> RenderPartial => new[]
		{
			"stored_fields",
			"script_fields",
			"docvalue_fields"
		};
	}
}
