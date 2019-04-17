using System.Runtime.Serialization;

namespace Nest
{
	public interface IExplainResponse<out TDocument> : IResponse
		where TDocument : class
	{
		IInlineGet<TDocument> Get { get; }
	}

	[DataContract]
	public class ExplainResponse<TDocument> : ResponseBase, IExplainResponse<TDocument>
		where TDocument : class
	{
		[DataMember(Name ="explanation")]
		public ExplanationDetail Explanation { get; internal set; }

		[DataMember(Name ="get")]
		public IInlineGet<TDocument> Get { get; internal set; }

		[DataMember(Name ="matched")]
		public bool Matched { get; internal set; }
	}
}
