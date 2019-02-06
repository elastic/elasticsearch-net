using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatPendingTasksRecord : ICatRecord
	{
		[DataMember(Name ="insertOrder")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? InsertOrder { get; set; }

		[DataMember(Name ="priority")]
		public string Priority { get; set; }

		[DataMember(Name ="source")]
		public string Source { get; set; }

		[DataMember(Name ="timeInQueue")]
		public string TimeInQueue { get; set; }
	}
}
