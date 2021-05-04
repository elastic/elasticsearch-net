// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetTransformResponse : ResponseBase
	{
		/// <summary>
		/// The count of transforms.
		/// </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary>
		/// An array of transform resources
		/// </summary>
		[DataMember(Name = "transforms")]
		public IReadOnlyCollection<Transform> Transforms { get; internal set; } = EmptyReadOnly<Transform>.Collection;
	}
}
