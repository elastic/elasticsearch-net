// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class AliasRemoveOperation
	{
		/// <summary>
		/// An alias to remove.
		/// Multiple aliases can be specified with <see cref="Aliases"/>
		/// </summary>
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		/// <summary>
		/// A collection of aliases to remove
		/// </summary>
		[DataMember(Name ="aliases")]
		public IEnumerable<string> Aliases { get; set; }

		/// <summary>
		/// The index to which to remove the alias.
		/// Multiple indices can be specified with <see cref="Indices"/>
		/// </summary>
		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		/// <summary>
		/// The indices to which to remove the alias
		/// </summary>
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices Indices { get; set; }

		/// <summary>
		/// If <c>true</c>, the alias to remove must exist. Defaults to <c>false</c>.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name = "must_exist")]
		public bool? MustExist { get; set; }
	}
}
