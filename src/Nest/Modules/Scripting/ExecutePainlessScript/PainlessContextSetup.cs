// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Sets up contextual scope for the painless script the execute under.
	/// </summary>
	[ReadAs(typeof(PainlessContextSetup))]
	public interface IPainlessContextSetup
	{
		/// <summary>
		/// Contains the document that will be temporarily indexed in-memory and is accessible from the script.
		/// </summary>
		[DataMember(Name ="document")]
		object Document { get; set; }

		/// <summary>
		/// The name of an index containing a mapping that is compatible with the document being indexed.
		/// </summary>
		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		/// <summary>
		/// If _score is used in the script then a query can specified that will be used to compute a score.
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	/// <inheritdoc cref="IPainlessContextSetup" />
	public class PainlessContextSetup : IPainlessContextSetup
	{
		/// <inheritdoc cref="IPainlessContextSetup.Document" />
		public object Document { get; set; }

		/// <inheritdoc cref="IPainlessContextSetup.Index" />
		public IndexName Index { get; set; }

		/// <inheritdoc cref="IPainlessContextSetup.Query" />
		public QueryContainer Query { get; set; }
	}

	/// <inheritdoc cref="IPainlessContextSetup" />
	public class PainlessContextSetupDescriptor : DescriptorBase<PainlessContextSetupDescriptor, IPainlessContextSetup>, IPainlessContextSetup
	{
		object IPainlessContextSetup.Document { get; set; }
		IndexName IPainlessContextSetup.Index { get; set; }
		QueryContainer IPainlessContextSetup.Query { get; set; }

		/// <inheritdoc cref="IPainlessContextSetup.Document" />
		public PainlessContextSetupDescriptor Document<T>(T document) => Assign(document, (a, v) => a.Document = v);

		/// <inheritdoc cref="IPainlessContextSetup.Index" />
		public PainlessContextSetupDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="IPainlessContextSetup.Query" />
		public PainlessContextSetupDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) where T : class =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
