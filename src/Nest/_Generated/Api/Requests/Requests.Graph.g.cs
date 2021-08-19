// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using Elastic.Transport;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest.Graph
{
	[ConvertAs(typeof(ExploreRequest))]
	public partial interface IExploreRequest : IRequest<ExploreRequestParameters>
	{
	}

	public partial class ExploreRequest : PlainRequestBase<ExploreRequestParameters>, IExploreRequest
	{
		protected IExploreRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.GraphExplore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		protected override bool CanBeEmpty => false;
		protected override bool IsEmpty => false;

		///<summary>/{index}/_graph/explore</summary>
        public ExploreRequest(Nest.Indices index) : base(r => r.Required("index", index))
		{
		}

		[JsonIgnore]
		public Nest.Routing? Routing { get => Q<Nest.Routing?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Nest.Time? Timeout { get => Q<Nest.Time?>("timeout"); set => Q("timeout", value); }

		[JsonPropertyName("connections")]
		public Nest.Graph.Hop? Connections
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("controls")]
		public Nest.Graph.ExploreControls? Controls
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("query")]
		public Nest.QueryDsl.QueryContainer? Query
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("vertices")]
		public IReadOnlyCollection<Nest.Graph.VertexDefinition>? Vertices
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}
}