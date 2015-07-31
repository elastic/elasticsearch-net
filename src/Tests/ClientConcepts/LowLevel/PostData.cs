using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;

namespace Tests.ClientConcepts.LowLevel
{
	public class PostingData
	{
		/** # 
		 * 
		 */

		public void Temp()
		{
			var fromString = ImplicitlyConvertsFrom("fromString");
			var fromByteArray = ImplicitlyConvertsFrom(Encoding.UTF8.GetBytes("fromByteArray"));
			var fromListOfString = ImplicitlyConvertsFrom(Encoding.UTF8.GetBytes("fromListOfStrings"));
		}

		public PostData<object> ImplicitlyConvertsFrom(PostData<object> postData) => postData;
	}
}
