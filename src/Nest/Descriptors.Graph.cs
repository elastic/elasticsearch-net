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

// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest.Utf8Json;
using Elasticsearch.Net.Specification.GraphApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	///<summary>Descriptor for Explore <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/graph-explore-api.html</para></summary>
	public partial class GraphExploreDescriptor<TDocument> : RequestDescriptorBase<GraphExploreDescriptor<TDocument>, GraphExploreRequestParameters, IGraphExploreRequest<TDocument>>, IGraphExploreRequest<TDocument>
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.GraphExplore;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		///<summary>/{index}/_graph/explore</summary>
		///<param name = "index">this parameter is required</param>
		public GraphExploreDescriptor(Indices index): base(r => r.Required("index", index))
		{
		}

		///<summary>/{index}/_graph/explore</summary>
		public GraphExploreDescriptor(): this(typeof(TDocument))
		{
		}

		// values part of the url path
		Indices IGraphExploreRequest.Index => Self.RouteValues.Get<Indices>("index");
		///<summary>A comma-separated list of index names to search; use the special string `_all` or Indices.All to perform the operation on all indices</summary>
		public GraphExploreDescriptor<TDocument> Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));
		///<summary>a shortcut into calling Index(typeof(TOther))</summary>
		public GraphExploreDescriptor<TDocument> Index<TOther>()
			where TOther : class => Assign(typeof(TOther), (a, v) => a.RouteValues.Required("index", (Indices)v));
		///<summary>A shortcut into calling Index(Indices.All)</summary>
		public GraphExploreDescriptor<TDocument> AllIndices() => Index(Indices.All);
		// Request parameters
		///<summary>
		/// A document is routed to a particular shard in an index using the following formula
		/// <para> shard_num = hash(_routing) % num_primary_shards</para>
		/// <para>Elasticsearch will use the document id if not provided. </para>
		/// <para>For requests that are constructed from/for a document NEST will automatically infer the routing key
		/// if that document has a <see cref = "Nest.JoinField"/> or a routing mapping on for its type exists on <see cref = "Nest.ConnectionSettings"/></para> 
		///</summary>
		public GraphExploreDescriptor<TDocument> Routing(Routing routing) => Qs("routing", routing);
		///<summary>Explicit operation timeout</summary>
		public GraphExploreDescriptor<TDocument> Timeout(Time timeout) => Qs("timeout", timeout);
	}
}