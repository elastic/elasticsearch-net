using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatPendingTasksRecord : ICatRecord
	{
		[DataMember(Name ="insertOrder")]
		public int? InsertOrder { get; set; }

		[DataMember(Name ="priority")]
		public string Priority { get; set; }

		[DataMember(Name ="source")]
		public string Source { get; set; }

		[DataMember(Name ="timeInQueue")]
		public string TimeInQueue { get; set; }
	}
}
