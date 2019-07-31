using System.Runtime.Serialization;

namespace ApiGenerator.Domain.Specification {
	public enum Stability
	{
		/// <summary>
		/// Highly likely to break in the near future (minor/path), no BWC guarantees. Possibly removed in the future.
		/// </summary>
		[EnumMember(Value = "experimental")]
		Experimental,

		/// <summary>
		/// Less likely to break or be removed but still reserve the right to do so.
		/// </summary>
		[EnumMember(Value = "beta")]
		Beta,

		/// <summary>
		/// No backwards breaking changes in a minor.
		/// </summary>
		[EnumMember(Value = "stable")]
		Stable
	}
}
