using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Sets up contextual scope for the painless script the execute under.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<object>))]
	public interface IPainlessContextSetup
	{
		/// <summary>
		/// Contains the document that will be temporarily indexed in-memory and is accessible from the script.jj
		/// </summary>
		[JsonProperty("document")]
		object Document { get; set; }
		/// <summary>
		/// The name of an index containing a mapping that is compatable with the document being indexed.
		/// </summary>
		[JsonProperty("index")]
		IndexName Index { get; set; }
		/// <summary>
		/// If _score is used in the script then a query can specified that will be used to compute a score.
		/// </summary>
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}

	/// <inheritdoc cref="IPainlessContextSetup"/>
	public class PainlessContextSetup : IPainlessContextSetup
	{
		/// <inheritdoc cref="IPainlessContextSetup.Document"/>
		public object Document { get; set; }
		/// <inheritdoc cref="IPainlessContextSetup.Index"/>
		public IndexName Index { get; set; }
		/// <inheritdoc cref="IPainlessContextSetup.Query"/>
		public QueryContainer Query { get; set; }
	}

	/// <inheritdoc cref="IPainlessContextSetup"/>
	public class PainlessContextSetupDescriptor : DescriptorBase<PainlessContextSetupDescriptor, IPainlessContextSetup>, IPainlessContextSetup
	{
		object IPainlessContextSetup.Document { get; set; }
		IndexName IPainlessContextSetup.Index { get; set; }
		QueryContainer IPainlessContextSetup.Query { get; set; }

		/// <inheritdoc cref="IPainlessContextSetup.Document"/>
		public PainlessContextSetupDescriptor Document<T>(T document) => Assign(a => a.Document = document);
		/// <inheritdoc cref="IPainlessContextSetup.Index"/>
		public PainlessContextSetupDescriptor Index(IndexName index) => Assign(a => a.Index = index);
		/// <inheritdoc cref="IPainlessContextSetup.Query"/>
		public PainlessContextSetupDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) where T : class =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
