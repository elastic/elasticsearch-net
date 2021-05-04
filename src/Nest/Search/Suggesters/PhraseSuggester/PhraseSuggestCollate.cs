// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Checks each suggestion against the specified query to prune suggestions
	/// for which no matching docs exist in the index.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(PhraseSuggestCollate))]
	public interface IPhraseSuggestCollate
	{
		/// <summary>
		/// The parameters for the query. the suggestion value will be added to the variables you specify.
		/// </summary>
		[DataMember(Name = "params")]
		IDictionary<string, object> Params { get; set; }

		/// <summary>
		/// Controls if all phrase suggestions will be returned. When set to <c>true</c>, the suggestions will have
		/// an additional option collate_match, which will be <c>true</c> if matching documents for the phrase was found,
		/// <c>false</c> otherwise. The default value for <see cref="Prune" /> is <c>false</c>.
		/// </summary>
		[DataMember(Name = "prune")]
		bool? Prune { get; set; }

		/// <summary>
		/// The collate query to run.
		/// </summary>
		[DataMember(Name = "query")]
		IPhraseSuggestCollateQuery Query { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		/// <inheritdoc />
		public IDictionary<string, object> Params { get; set; }

		/// <inheritdoc />
		public bool? Prune { get; set; }

		/// <inheritdoc />
		public IPhraseSuggestCollateQuery Query { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggestCollate" />
	public class PhraseSuggestCollateDescriptor<T> : DescriptorBase<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate>, IPhraseSuggestCollate
		where T : class
	{
		IDictionary<string, object> IPhraseSuggestCollate.Params { get; set; }
		bool? IPhraseSuggestCollate.Prune { get; set; }
		IPhraseSuggestCollateQuery IPhraseSuggestCollate.Query { get; set; }

		/// <inheritdoc cref="IPhraseSuggestCollate.Query" />
		public PhraseSuggestCollateDescriptor<T> Query(Func<PhraseSuggestCollateQueryDescriptor, IPhraseSuggestCollateQuery> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new PhraseSuggestCollateQueryDescriptor()));

		/// <inheritdoc cref="IPhraseSuggestCollate.Prune" />
		public PhraseSuggestCollateDescriptor<T> Prune(bool? prune = true) => Assign(prune, (a, v) => a.Prune = v);

		/// <inheritdoc cref="IPhraseSuggestCollate.Params" />
		public PhraseSuggestCollateDescriptor<T> Params(IDictionary<string, object> paramsDictionary) => Assign(paramsDictionary, (a, v) => a.Params = v);

		/// <inheritdoc cref="IPhraseSuggestCollate.Params" />
		public PhraseSuggestCollateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(paramsDictionary(new FluentDictionary<string, object>()), (a, v) => a.Params = v);
	}
}
