using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class LoggingActionResult
	{
		[DataMember(Name ="logged_text")]
		public string LoggedText { get; set; }
	}
}
