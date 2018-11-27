using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatHelpRecord : ICatRecord
	{
		[DataMember(Name ="endpoint")]
		public string Endpoint { get; set; }
	}
}
