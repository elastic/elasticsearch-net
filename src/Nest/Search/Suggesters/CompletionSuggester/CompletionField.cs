using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A completion field
	/// </summary>
	/// <remarks>
	/// Convenience class for use when indexing completion fields.
	/// </remarks>
	public class CompletionField
	{
		/// <summary>
		/// The contexts
		/// </summary>
		[DataMember(Name ="contexts")]
		public IDictionary<string, IEnumerable<string>> Contexts { get; set; }

		/// <summary>
		/// The inputs to store
		/// </summary>
		[DataMember(Name ="input")]
		public IEnumerable<string> Input { get; set; }

		/// <summary>
		/// A positive integer which defines a weight
		/// and allows you to rank your suggestions. This field is optional.
		/// </summary>
		[DataMember(Name ="weight")]
		public int? Weight { get; set; }
	}
}
