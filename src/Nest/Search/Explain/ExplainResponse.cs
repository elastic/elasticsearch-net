using System.Runtime.Serialization;

namespace Nest
{
	public interface IExplainResponse<TDocument> : IResponse
		where TDocument : class
	{
		ExplanationDetail Explanation { get; }
		InstantGet<TDocument> Get { get; }
		bool Matched { get; }
	}

	[DataContract]
	public class ExplainResponse<TDocument> : ResponseBase, IExplainResponse<TDocument>
		where TDocument : class
	{
		[DataMember(Name ="explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[DataMember(Name ="get")]
		public InstantGet<TDocument> Get { get; internal set; }

		[DataMember(Name ="matched")]
		public bool Matched { get; internal set; }
	}
}
