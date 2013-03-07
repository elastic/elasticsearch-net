using System;
using System.Diagnostics;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Search using T as the return type
		/// </summary>
		public IQueryResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class
		{
			var mltDescriptor = new MoreLikeThisDescriptor<T>();
			var descriptor = mltSelector(mltDescriptor);

			var path = this.PathResolver.GetMoreLikeThisPathFor(descriptor);
			ConnectionStatus status = null;
			if (descriptor._Search == null)
			{
				status = this.Connection.GetSync(path);
			}
			else
			{
				var search = this.Serialize(descriptor._Search);
				status = this.Connection.PostSync(path, search);
			}
			return this.ToParsedResponse<QueryResponse<T>>(status);
		}
	}
}