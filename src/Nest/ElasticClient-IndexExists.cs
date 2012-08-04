namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Check if the index already exists
		/// </summary>
		public IIndexExistsResponse IndexExists(string index)
		{
			return this._IndexExists(index);
		}
		private IndexExistsResponse _IndexExists(string index)
		{
			var path = this.PathResolver.CreateIndexPath(index);
			var status = this.Connection.HeadSync(path);
			var response = new IndexExistsResponse()
			{
				IsValid = false,
				Exists = false,
				ConnectionStatus = status
			};
			if (status.Error == null || status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
			{ 
				//404 is an expected possible status code for this call.
				response.IsValid = true;
			}
			if (status.Error == null)
			{ 
				response.Exists = true;
			}
			return response;
		}
	}
}
