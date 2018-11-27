using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryFiles
	{
		[DataMember(Name ="details")]
		public IEnumerable<RecoveryFileDetails> Details { get; internal set; }

		[DataMember(Name ="percent")]
		public string Percent { get; internal set; }

		[DataMember(Name ="recovered")]
		public long Recovered { get; internal set; }

		[DataMember(Name ="reused")]
		public long Reused { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }
	}
}
