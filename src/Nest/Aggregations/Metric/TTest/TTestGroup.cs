// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A population for a <see cref="TTestAggregation"/>
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TTestPopulation))]
	public interface ITTestPopulation
	{
		/// <summary>
		/// The field to use for the population values. Must be a numeric field.
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// A script tp use to calculate population values.
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }

		/// <summary>
		/// A filter to apply to target field to filter population values. Useful
		/// when two populations use the same field for values, to filter the values.
		/// </summary>
		[DataMember(Name = "filter")]
		QueryContainer Filter { get; set; }
	}

	/// <inheritdoc />
	// ReSharper disable once InconsistentNaming
	public class TTestPopulation : ITTestPopulation
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public IScript Script { get; set; }
		/// <inheritdoc />
		public QueryContainer Filter { get; set; }
	}

	/// <inheritdoc cref="ITTestPopulation"/>
	// ReSharper disable once InconsistentNaming
	public class TTestPopulationDescriptor<T> : DescriptorBase<TTestPopulationDescriptor<T>, ITTestPopulation>, ITTestPopulation where T : class
	{
		Field ITTestPopulation.Field { get; set; }
		IScript ITTestPopulation.Script { get; set; }
		QueryContainer ITTestPopulation.Filter { get; set; }

		/// <inheritdoc cref="ITTestPopulation.Field"/>
		public TTestPopulationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITTestPopulation.Field"/>
		public TTestPopulationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITTestPopulation.Script"/>
		public TTestPopulationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		/// <inheritdoc cref="ITTestPopulation.Script"/>
		public TTestPopulationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="ITTestPopulation.Filter"/>
		public TTestPopulationDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filter) =>
			Assign(filter, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
