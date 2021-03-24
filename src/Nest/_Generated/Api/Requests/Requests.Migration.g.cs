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
// Run the following in the root of the repository:
//
// TODO - RUN INSTRUCTIONS
//
// ------------------------------------------------
using System;
using System.Text.Json.Serialization;
using Elastic.Transport;

#nullable restore
namespace Nest
{
    public interface IDeprecationInfoRequest : IRequest<DeprecationInfoRequestParameters>
    {
    }

    public class DeprecationInfoRequest : PlainRequestBase<DeprecationInfoRequestParameters>, IDeprecationInfoRequest
    {
        protected IDeprecationInfoRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.MigrationDeprecations;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_migration/deprecations</summary>
        public DeprecationInfoRequest(): base()
        {
        }

        ///<summary>/{index}/_migration/deprecations</summary>
        public DeprecationInfoRequest(IndexName index): base(r => r.Optional("index", index))
        {
        }
    }
}