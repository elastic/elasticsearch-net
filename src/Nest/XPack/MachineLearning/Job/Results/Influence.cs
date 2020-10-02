// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class Influence
	{
		/// <summary>
		/// The field name of the influencer.
		/// </summary>
		[DataMember(Name ="influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		/// <summary>
		/// The entities that influenced, contributed to, or was to blame for the influence.
		/// </summary>
		[DataMember(Name ="influencer_field_values")]
		public IReadOnlyCollection<string> InfluencerFieldValues { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
