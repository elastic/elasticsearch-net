// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class OpenJobResponse : ResponseBase
	{
		[DataMember(Name ="opened")]
		public bool Opened { get; internal set; }

		/// <summary>
		/// The node that the job was assigned to
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "node")]
		public string Node { get; internal set; }
	}
}
