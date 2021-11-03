// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Tests.Domain;

public class Labels
{
	public LabelActivity LabelActivity { get; set; }
	public string Priority { get; set; }

	public IList<string> Release { get; set; }
}
