// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc />
	public class GatewaySettings
	{
		/// <inheritdoc />
		public int? ExpectedDataNodes { get; internal set; }

		/// <inheritdoc />
		public int? ExpectedMasterNodes { get; internal set; }

		/// <inheritdoc />
		public int? ExpectedNodes { get; internal set; }

		/// <inheritdoc />
		public Time RecoveryAfterTime { get; internal set; }
	}
}
