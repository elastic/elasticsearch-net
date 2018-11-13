using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Convenience class for use when indexing completion fields.
	/// </summary>
	public class CompletionField
	{
		[DataMember(Name ="contexts")]
		public IDictionary<string, IEnumerable<string>> Contexts { get; set; }

		[DataMember(Name ="input")]
		public IEnumerable<string> Input { get; set; }

		[DataMember(Name ="weight")]
		public int? Weight { get; set; }
	}
}
