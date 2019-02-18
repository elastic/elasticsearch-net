using System;

namespace Nest
{
	[MapsApi("sql.translate.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<TranslateSqlRequest>))]
	public partial interface ITranslateSqlRequest : ISqlRequest { }

	public partial class TranslateSqlRequest
	{
		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		/// >
		public int? FetchSize { get; set; }

		/// <inheritdoc cref="ISqlRequest.Filter" />
		/// >
		public QueryContainer Filter { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		/// >
		public string Query { get; set; }

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		/// >
		public string TimeZone { get; set; }
	}

	public partial class TranslateSqlDescriptor
	{
		int? ISqlRequest.FetchSize { get; set; }
		QueryContainer ISqlRequest.Filter { get; set; }
		string ISqlRequest.Query { get; set; }
		string ISqlRequest.TimeZone { get; set; }

		/// <inheritdoc cref="ISqlRequest.Query" />
		/// >
		public TranslateSqlDescriptor Query(string query) => Assign(a => a.Query = query);

		/// <inheritdoc cref="ISqlRequest.TimeZone" />
		/// >
		public TranslateSqlDescriptor TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		/// <inheritdoc cref="ISqlRequest.FetchSize" />
		/// >
		public TranslateSqlDescriptor FetchSize(int? fetchSize) => Assign(a => a.FetchSize = fetchSize);

		/// <inheritdoc cref="ISqlRequest.Filter" />
		/// >
		public TranslateSqlDescriptor Filter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
			where T : class => Assign(a => a.Filter = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
