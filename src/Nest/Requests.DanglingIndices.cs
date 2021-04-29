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
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Specification.DanglingIndicesApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IDeleteDanglingIndexRequest : IRequest<DeleteDanglingIndexRequestParameters>
	{
		[IgnoreDataMember]
		IndexUuid IndexUuid
		{
			get;
		}
	}

	///<summary>Request for DeleteDanglingIndex <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
	public partial class DeleteDanglingIndexRequest : PlainRequestBase<DeleteDanglingIndexRequestParameters>, IDeleteDanglingIndexRequest
	{
		protected IDeleteDanglingIndexRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.DanglingIndicesDeleteDanglingIndex;
		///<summary>/_dangling/{index_uuid}</summary>
		///<param name = "indexUuid">this parameter is required</param>
		public DeleteDanglingIndexRequest(IndexUuid indexUuid): base(r => r.Required("index_uuid", indexUuid))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected DeleteDanglingIndexRequest(): base()
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		IndexUuid IDeleteDanglingIndexRequest.IndexUuid => Self.RouteValues.Get<IndexUuid>("index_uuid");
		// Request parameters
		///<summary>Must be set to true in order to delete the dangling index</summary>
		public bool? AcceptDataLoss
		{
			get => Q<bool? >("accept_data_loss");
			set => Q("accept_data_loss", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IImportDanglingIndexRequest : IRequest<ImportDanglingIndexRequestParameters>
	{
		[IgnoreDataMember]
		IndexUuid IndexUuid
		{
			get;
		}
	}

	///<summary>Request for ImportDanglingIndex <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
	public partial class ImportDanglingIndexRequest : PlainRequestBase<ImportDanglingIndexRequestParameters>, IImportDanglingIndexRequest
	{
		protected IImportDanglingIndexRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.DanglingIndicesImportDanglingIndex;
		///<summary>/_dangling/{index_uuid}</summary>
		///<param name = "indexUuid">this parameter is required</param>
		public ImportDanglingIndexRequest(IndexUuid indexUuid): base(r => r.Required("index_uuid", indexUuid))
		{
		}

		///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>
		[SerializationConstructor]
		protected ImportDanglingIndexRequest(): base()
		{
		}

		// values part of the url path
		[IgnoreDataMember]
		IndexUuid IImportDanglingIndexRequest.IndexUuid => Self.RouteValues.Get<IndexUuid>("index_uuid");
		// Request parameters
		///<summary>Must be set to true in order to import the dangling index</summary>
		public bool? AcceptDataLoss
		{
			get => Q<bool? >("accept_data_loss");
			set => Q("accept_data_loss", value);
		}

		///<summary>Specify timeout for connection to master</summary>
		public Time MasterTimeout
		{
			get => Q<Time>("master_timeout");
			set => Q("master_timeout", value);
		}

		///<summary>Explicit operation timeout</summary>
		public Time Timeout
		{
			get => Q<Time>("timeout");
			set => Q("timeout", value);
		}
	}

	[InterfaceDataContract]
	public partial interface IListDanglingIndicesRequest : IRequest<ListDanglingIndicesRequestParameters>
	{
	}

	///<summary>Request for List <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-gateway-dangling-indices.html</para></summary>
	public partial class ListDanglingIndicesRequest : PlainRequestBase<ListDanglingIndicesRequestParameters>, IListDanglingIndicesRequest
	{
		protected IListDanglingIndicesRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.DanglingIndicesList;
	// values part of the url path
	// Request parameters
	}
}