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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query to run for a phrase suggester collate
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(PhraseSuggestCollateQuery))]
	public interface IPhraseSuggestCollateQuery
	{
		/// <summary>
		/// The id for a stored script to execute
		/// </summary>
		[DataMember(Name = "id")]
		Id Id { get; set; }

		/// <summary>
		/// The source script to be executed
		/// </summary>
		[DataMember(Name = "source")]
		string Source { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollateQuery : IPhraseSuggestCollateQuery
	{
		/// <inheritdoc />
		public Id Id { get; set; }

		/// <inheritdoc />
		public string Source { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggestCollateQuery" />
	public class PhraseSuggestCollateQueryDescriptor
		: DescriptorBase<PhraseSuggestCollateQueryDescriptor, IPhraseSuggestCollateQuery>, IPhraseSuggestCollateQuery
	{
		Id IPhraseSuggestCollateQuery.Id { get; set; }
		string IPhraseSuggestCollateQuery.Source { get; set; }

		/// <inheritdoc cref="IPhraseSuggestCollateQuery.Source" />
		public PhraseSuggestCollateQueryDescriptor Source(string source) => Assign(source, (a, v) => a.Source = v);

		/// <inheritdoc cref="IPhraseSuggestCollateQuery.Id" />
		public PhraseSuggestCollateQueryDescriptor Id(Id id) => Assign(id, (a, v) => a.Id = v);
	}
}
