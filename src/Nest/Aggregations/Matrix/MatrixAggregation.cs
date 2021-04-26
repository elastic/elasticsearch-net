/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMatrixAggregation : IAggregation
	{
		[DataMember(Name ="fields")]
		Fields Fields { get; set; }

		[DataMember(Name ="missing")]
		IDictionary<Field, double> Missing { get; set; }
	}

	public abstract class MatrixAggregationBase : AggregationBase, IMatrixAggregation
	{
		internal MatrixAggregationBase() { }

		protected MatrixAggregationBase(string name, Fields field) : base(name) => Fields = field;

		public Fields Fields { get; set; }

		public IDictionary<Field, double> Missing { get; set; }
	}

	public abstract class MatrixAggregationDescriptorBase<TMatrixAggregation, TMatrixAggregationInterface, T>
		: DescriptorBase<TMatrixAggregation, TMatrixAggregationInterface>, IMatrixAggregation
		where TMatrixAggregation : MatrixAggregationDescriptorBase<TMatrixAggregation, TMatrixAggregationInterface, T>
		, TMatrixAggregationInterface, IMatrixStatsAggregation
		where T : class
		where TMatrixAggregationInterface : class, IMatrixAggregation
	{
		Fields IMatrixAggregation.Fields { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		IDictionary<Field, double> IMatrixAggregation.Missing { get; set; }

		string IAggregation.Name { get; set; }

		public TMatrixAggregation Field(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		public TMatrixAggregation Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TMatrixAggregation Missing(Func<FluentDictionary<Field, double>, FluentDictionary<Field, double>> selector) =>
			Assign(selector, (a, v) => a.Missing = v?.Invoke(new FluentDictionary<Field, double>()));

		public TMatrixAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
