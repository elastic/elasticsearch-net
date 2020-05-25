// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public partial interface IRenderSearchTemplateRequest
	{
		[DataMember(Name = "file")]
		string File { get; set; }

		[DataMember(Name = "params")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysBaseFormatter<Dictionary<string, object>, string, object>))]
		Dictionary<string, object> Params { get; set; }

		[DataMember(Name = "source")]
		string Source { get; set; }
	}

	public partial class RenderSearchTemplateRequest
	{
		public string File { get; set; }

		public Dictionary<string, object> Params { get; set; }
		public string Source { get; set; }
	}

	public partial class RenderSearchTemplateDescriptor
	{
		string IRenderSearchTemplateRequest.File { get; set; }

		Dictionary<string, object> IRenderSearchTemplateRequest.Params { get; set; }
		string IRenderSearchTemplateRequest.Source { get; set; }

		public RenderSearchTemplateDescriptor Source(string source) => Assign(source, (a, v) => a.Source = v);

		public RenderSearchTemplateDescriptor File(string file) => Assign(file, (a, v) => a.File = v);

		public RenderSearchTemplateDescriptor Params(Dictionary<string, object> scriptParams) => Assign(scriptParams, (a, v) => a.Params = v);

		public RenderSearchTemplateDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(paramsSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
