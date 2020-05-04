// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query allowing to define a script to execute as a query
	/// </summary>
	[ReadAs(typeof(ScriptQuery))]
	[InterfaceDataContract]
	public interface IScriptQuery : IQuery
	{
		/// <summary>
		/// The script to execute
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptQuery"/>
	public class ScriptQuery : QueryBase, IScriptQuery
	{
		/// <inheritdoc />
		public IScript Script { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Script = this;

		internal static bool IsConditionless(IScriptQuery q)
		{
			if (q.Script == null)
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

	/// <inheritdoc cref="IScriptQuery"/>
	public class ScriptQueryDescriptor<T>
		: QueryDescriptorBase<ScriptQueryDescriptor<T>, IScriptQuery>
			, IScriptQuery where T : class
	{
		protected override bool Conditionless => ScriptQuery.IsConditionless(this);

		IScript IScriptQuery.Script { get; set; }

		/// <inheritdoc cref="IScriptQuery.Script"/>
		public ScriptQueryDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
