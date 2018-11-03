using Newtonsoft.Json;

namespace Nest
{
	/// <summary> Shared properties of <see cref="IQuerySqlRequest" /> and <see cref="ITranslateSqlRequest" /> </summary>
	public interface ISqlRequest
	{
		/// <summary>
		/// fetch_size is a hint for how many results to return in each page. SQL might chose to return more or fewer results though.
		/// </summary>
		[JsonProperty("fetch_size")]
		int? FetchSize { get; set; }

		/// <summary> Further filter the results returned by the SQL query provided on <see cref="Query" /> </summary>
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		/// <summary> The SQL query you want Elasticsearch to execute </summary>
		[JsonProperty("query")]
		string Query { get; set; }

		/// <summary>
		/// time_zone is the time zone to use for date functions and date parsing. time_zone defaults to utc
		/// and can take any values as documented on Joda time's DateTimeZone enum.
		/// </summary>
		[JsonProperty("time_zone")]
		string TimeZone { get; set; }
	}
}
