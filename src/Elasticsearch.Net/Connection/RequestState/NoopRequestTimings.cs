namespace Elasticsearch.Net.Connection.RequestState
{
	internal class NoopRequestTimings : IRequestTimings
	{
		public static NoopRequestTimings Instance = new NoopRequestTimings();

		public void Finish(bool success, int? httpStatusCode)
		{
		}

		public void Dispose()
		{
		}
	}
}