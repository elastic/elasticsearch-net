// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class GetTemplateResponse
{
	[JsonIgnore]
	public IReadOnlyDictionary<string, TemplateMapping> TemplateMappings => BackingDictionary;
}
