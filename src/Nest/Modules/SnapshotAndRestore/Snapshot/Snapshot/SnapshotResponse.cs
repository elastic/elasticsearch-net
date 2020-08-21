// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SnapshotResponse : ResponseBase
	{
		private bool _accepted;

		[DataMember(Name ="accepted")]
		public bool Accepted
		{
			get => _accepted ? _accepted : Snapshot != null;
			internal set => _accepted = value;
		}

		[DataMember(Name ="snapshot")]
		public Snapshot Snapshot { get; set; }
	}
}
