/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.IO;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("create.json")]
	[JsonFormatter(typeof(CreateRequestFormatter<>))]
	public partial interface ICreateRequest<TDocument> : IProxyRequest, IDocumentRequest where TDocument : class
	{
		TDocument Document { get; set; }
	}

	public partial class CreateRequest<TDocument> where TDocument : class
	{
		public TDocument Document { get; set; }

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Document = document;
	}

	public partial class CreateDescriptor<TDocument> where TDocument : class
	{
		TDocument ICreateRequest<TDocument>.Document { get; set; }

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Document, stream, formatting);

		partial void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.Document = v);

		/// <summary>
		/// Sets the id for the document. Overrides the id that may be inferred from the document.
		/// </summary>
		public CreateDescriptor<TDocument> Id(Id id) => Assign(id,(a,v) => a.RouteValues.Required("id", v));
	}
}
