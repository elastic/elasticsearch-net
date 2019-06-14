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
using Elasticsearch.Net.Specification.SqlApi;

// ReSharper disable RedundantBaseConstructorCall
// ReSharper disable UnusedTypeParameter
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
namespace Nest
{
	[InterfaceDataContract]
	public partial interface IClearSqlCursorRequest : IRequest<ClearSqlCursorRequestParameters>
	{
	}

	///<summary>Request for ClearCursor <pre>Clear SQL cursor</pre></summary>
	public partial class ClearSqlCursorRequest : PlainRequestBase<ClearSqlCursorRequestParameters>, IClearSqlCursorRequest
	{
		protected IClearSqlCursorRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SqlClearCursor;
	// values part of the url path
	// Request parameters
	}

	[InterfaceDataContract]
	public partial interface IQuerySqlRequest : IRequest<QuerySqlRequestParameters>
	{
	}

	///<summary>Request for Query <pre>Execute SQL</pre></summary>
	public partial class QuerySqlRequest : PlainRequestBase<QuerySqlRequestParameters>, IQuerySqlRequest
	{
		protected IQuerySqlRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SqlQuery;
		// values part of the url path
		// Request parameters
		///<summary>a short version of the Accept header, e.g. json, yaml</summary>
		public string Format
		{
			get => Q<string>("format");
			set => Q("format", value);
		}
	}

	[InterfaceDataContract]
	public partial interface ITranslateSqlRequest : IRequest<TranslateSqlRequestParameters>
	{
	}

	///<summary>Request for Translate <pre>Translate SQL into Elasticsearch queries</pre></summary>
	public partial class TranslateSqlRequest : PlainRequestBase<TranslateSqlRequestParameters>, ITranslateSqlRequest
	{
		protected ITranslateSqlRequest Self => this;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SqlTranslate;
	// values part of the url path
	// Request parameters
	}
}