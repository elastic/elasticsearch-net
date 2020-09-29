// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query allowing you to modify the score of documents that are retrieved by a query.
	/// This can be useful if, for example, a score function is computationally expensive and it is sufficient to
	/// compute the score on a filtered set of documents.
	/// </summary>
	[ReadAs(typeof(ScriptScoreQuery))]
	[InterfaceDataContract]
	public interface IScriptScoreQuery : IQuery
	{
		/// <summary>
		/// The query to execute
		/// </summary>
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// The script to execute
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptScoreQuery" />
	public class ScriptScoreQuery : QueryBase, IScriptScoreQuery
	{
		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.ScriptScore = this;

		internal static bool IsConditionless(IScriptScoreQuery q)
		{
			if (q.Script == null || q.Query.IsConditionless())
				return true;

			switch (q.Script)
			{
				case IInlineScript inlineScript:
					return inlineScript.Source.IsNullOrEmpty();
				case IIndexedScript indexedScript:
					return indexedScript.Id.IsNullOrEmpty();
			}

			return false;
		}
	}

	/// <inheritdoc cref="IScriptScoreQuery" />
	public class ScriptScoreQueryDescriptor<T>
		: QueryDescriptorBase<ScriptScoreQueryDescriptor<T>, IScriptScoreQuery>
			, IScriptScoreQuery where T : class
	{
		protected override bool Conditionless => ScriptScoreQuery.IsConditionless(this);
		QueryContainer IScriptScoreQuery.Query { get; set; }

		IScript IScriptScoreQuery.Script { get; set; }

		/// <inheritdoc cref="IScriptScoreQuery.Query" />
		public ScriptScoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="IScriptScoreQuery.Script" />
		public ScriptScoreQueryDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
