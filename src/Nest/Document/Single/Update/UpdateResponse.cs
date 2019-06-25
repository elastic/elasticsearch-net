using System.Runtime.Serialization;

namespace Nest
{
	public interface IUpdateResponse<out TDocument> : IResponse where TDocument : class
	{
		IInlineGet<TDocument> Get { get; }
	}

	[DataContract]
	public class UpdateResponse<TDocument> : WriteResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		public override bool IsValid => base.IsValid && 
			(Result != Result.NotFound && Result != Result.Error);
		
		[DataMember(Name ="get")]
		public IInlineGet<TDocument> Get { get; internal set; }

	}
}
