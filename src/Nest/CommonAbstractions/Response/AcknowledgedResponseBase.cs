// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public abstract class AcknowledgedResponseBase : ResponseBase
	{
		[DataMember(Name = "acknowledged")]
		public bool Acknowledged { get; internal set; }

		/// <summary>
		/// Checks whether the response returned a valid HTTP status code and that the delete is acknowledged
		/// in one go. See also <see cref="AcknowledgedResponseBase.Acknowledged"/>
		/// </summary>
		public override bool IsValid => base.IsValid && Acknowledged;
	}
}
