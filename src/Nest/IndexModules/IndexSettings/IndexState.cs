using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(IndexState))]
	public interface IIndexState
	{
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		[DataMember(Name = "mappings")]
		ITypeMapping Mappings { get; set; }

		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }
	}

	public class IndexState : IIndexState
	{
		public IAliases Aliases { get; set; }

		public ITypeMapping Mappings { get; set; }
		public IIndexSettings Settings { get; set; }
	}
}
