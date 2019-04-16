using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ValidationExplanation
	{
		[DataMember(Name ="error")]
		public string Error { get; internal set; }

		[DataMember(Name ="explanation")]
		public string Explanation { get; internal set; }

		[DataMember(Name ="index")]
		public string Index { get; internal set; }

		[DataMember(Name ="valid")]
		public bool Valid { get; internal set; }
	}
}
