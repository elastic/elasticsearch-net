// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A field mapped as an <see cref="ICompletionProperty" />. Convenience class to use when indexing completion
	/// fields.
	/// </summary>
	public class CompletionField
	{
		/// <summary>
		/// The contexts to associate with the input which can be used at query time to filter and boost suggestions
		/// </summary>
		[DataMember(Name = "contexts")]
		public IDictionary<string, IEnumerable<string>> Contexts { get; set; }

		/// <summary>
		/// The input to store. Can be a single or multiple inputs
		/// </summary>
		[DataMember(Name = "input")]
		public IEnumerable<string> Input { get; set; }

		/// <summary>
		/// A positive integer which defines a weight and allows you to rank your suggestions. This field is optional.
		/// </summary>
		[DataMember(Name = "weight")]
		public int? Weight { get; set; }
	}
}
