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
using Elastic.Transport;

#nullable restore
namespace Nest
{
    public interface IAuthenticateRequest : IRequest<AuthenticateRequestParameters>
    {
    }

    public class AuthenticateRequest : PlainRequestBase<AuthenticateRequestParameters>, IAuthenticateRequest
    {
        protected IAuthenticateRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityAuthenticate;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/_authenticate</summary>
        public AuthenticateRequest(): base()
        {
        }
    }

    public interface IChangePasswordRequest : IRequest<ChangePasswordRequestParameters>
    {
    }

    public class ChangePasswordRequest : PlainRequestBase<ChangePasswordRequestParameters>, IChangePasswordRequest
    {
        protected IChangePasswordRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityChangePassword;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}/_password</summary>
        public ChangePasswordRequest(Name username): base(r => r.Optional("username", username))
        {
        }

        ///<summary>/_security/user/_password</summary>
        public ChangePasswordRequest(): base()
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IClearCachedRealmsRequest : IRequest<ClearCachedRealmsRequestParameters>
    {
    }

    public class ClearCachedRealmsRequest : PlainRequestBase<ClearCachedRealmsRequestParameters>, IClearCachedRealmsRequest
    {
        protected IClearCachedRealmsRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityClearCachedRealms;
        protected override HttpMethod HttpMethod => HttpMethod.POST;
        protected override bool SupportsBody => false;
        ///<summary>/_security/realm/{realms}/_clear_cache</summary>
        public ClearCachedRealmsRequest(Names realms): base(r => r.Required("realms", realms))
        {
        }
    }

    public interface IClearCachedRolesRequest : IRequest<ClearCachedRolesRequestParameters>
    {
    }

    public class ClearCachedRolesRequest : PlainRequestBase<ClearCachedRolesRequestParameters>, IClearCachedRolesRequest
    {
        protected IClearCachedRolesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityClearCachedRoles;
        protected override HttpMethod HttpMethod => HttpMethod.POST;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role/{name}/_clear_cache</summary>
        public ClearCachedRolesRequest(Names name): base(r => r.Required("name", name))
        {
        }
    }

    public interface ICreateApiKeyRequest : IRequest<CreateApiKeyRequestParameters>
    {
    }

    public class CreateApiKeyRequest : PlainRequestBase<CreateApiKeyRequestParameters>, ICreateApiKeyRequest
    {
        protected ICreateApiKeyRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityCreateApiKey;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/api_key</summary>
        public CreateApiKeyRequest(): base()
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IDeletePrivilegesRequest : IRequest<DeletePrivilegesRequestParameters>
    {
    }

    public class DeletePrivilegesRequest : PlainRequestBase<DeletePrivilegesRequestParameters>, IDeletePrivilegesRequest
    {
        protected IDeletePrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeletePrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/privilege/{application}/{name}</summary>
        public DeletePrivilegesRequest(Name application, Name name): base(r => r.Required("application", application).Required("name", name))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IDeleteRoleRequest : IRequest<DeleteRoleRequestParameters>
    {
    }

    public class DeleteRoleRequest : PlainRequestBase<DeleteRoleRequestParameters>, IDeleteRoleRequest
    {
        protected IDeleteRoleRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeleteRole;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role/{name}</summary>
        public DeleteRoleRequest(Name name): base(r => r.Required("name", name))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IDeleteRoleMappingRequest : IRequest<DeleteRoleMappingRequestParameters>
    {
    }

    public class DeleteRoleMappingRequest : PlainRequestBase<DeleteRoleMappingRequestParameters>, IDeleteRoleMappingRequest
    {
        protected IDeleteRoleMappingRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeleteRoleMapping;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role_mapping/{name}</summary>
        public DeleteRoleMappingRequest(Name name): base(r => r.Required("name", name))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IDeleteUserRequest : IRequest<DeleteUserRequestParameters>
    {
    }

    public class DeleteUserRequest : PlainRequestBase<DeleteUserRequestParameters>, IDeleteUserRequest
    {
        protected IDeleteUserRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeleteUser;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}</summary>
        public DeleteUserRequest(Name username): base(r => r.Required("username", username))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IDisableUserRequest : IRequest<DisableUserRequestParameters>
    {
    }

    public class DisableUserRequest : PlainRequestBase<DisableUserRequestParameters>, IDisableUserRequest
    {
        protected IDisableUserRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDisableUser;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}/_disable</summary>
        public DisableUserRequest(Name username): base(r => r.Required("username", username))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IEnableUserRequest : IRequest<EnableUserRequestParameters>
    {
    }

    public class EnableUserRequest : PlainRequestBase<EnableUserRequestParameters>, IEnableUserRequest
    {
        protected IEnableUserRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityEnableUser;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}/_enable</summary>
        public EnableUserRequest(Name username): base(r => r.Required("username", username))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IGetApiKeyRequest : IRequest<GetApiKeyRequestParameters>
    {
    }

    public class GetApiKeyRequest : PlainRequestBase<GetApiKeyRequestParameters>, IGetApiKeyRequest
    {
        protected IGetApiKeyRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetApiKey;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/api_key</summary>
        public GetApiKeyRequest(): base()
        {
        }

        public string? Id { get => Q<string?>("id"); set => Q("id", value); }

        public string? Name { get => Q<string?>("name"); set => Q("name", value); }

        public bool? Owner { get => Q<bool?>("owner"); set => Q("owner", value); }

        public string? RealmName { get => Q<string?>("realm_name"); set => Q("realm_name", value); }

        public string? Username { get => Q<string?>("username"); set => Q("username", value); }
    }

    public interface IGetBuiltinPrivilegesRequest : IRequest<GetBuiltinPrivilegesRequestParameters>
    {
    }

    public class GetBuiltinPrivilegesRequest : PlainRequestBase<GetBuiltinPrivilegesRequestParameters>, IGetBuiltinPrivilegesRequest
    {
        protected IGetBuiltinPrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetBuiltinPrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/privilege/_builtin</summary>
        public GetBuiltinPrivilegesRequest(): base()
        {
        }
    }

    public interface IGetPrivilegesRequest : IRequest<GetPrivilegesRequestParameters>
    {
    }

    public class GetPrivilegesRequest : PlainRequestBase<GetPrivilegesRequestParameters>, IGetPrivilegesRequest
    {
        protected IGetPrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetPrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/privilege</summary>
        public GetPrivilegesRequest(): base()
        {
        }

        ///<summary>/_security/privilege/{application}</summary>
        public GetPrivilegesRequest(Name application): base(r => r.Optional("application", application))
        {
        }

        ///<summary>/_security/privilege/{application}/{name}</summary>
        public GetPrivilegesRequest(Name application, Name name): base(r => r.Optional("application", application).Optional("name", name))
        {
        }
    }

    public interface IGetRoleRequest : IRequest<GetRoleRequestParameters>
    {
    }

    public class GetRoleRequest : PlainRequestBase<GetRoleRequestParameters>, IGetRoleRequest
    {
        protected IGetRoleRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetRole;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role/{name}</summary>
        public GetRoleRequest(Name name): base(r => r.Optional("name", name))
        {
        }

        ///<summary>/_security/role</summary>
        public GetRoleRequest(): base()
        {
        }
    }

    public interface IGetRoleMappingRequest : IRequest<GetRoleMappingRequestParameters>
    {
    }

    public class GetRoleMappingRequest : PlainRequestBase<GetRoleMappingRequestParameters>, IGetRoleMappingRequest
    {
        protected IGetRoleMappingRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetRoleMapping;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role_mapping/{name}</summary>
        public GetRoleMappingRequest(Name name): base(r => r.Optional("name", name))
        {
        }

        ///<summary>/_security/role_mapping</summary>
        public GetRoleMappingRequest(): base()
        {
        }
    }

    public interface IGetUserAccessTokenRequest : IRequest<GetUserAccessTokenRequestParameters>
    {
    }

    public class GetUserAccessTokenRequest : PlainRequestBase<GetUserAccessTokenRequestParameters>, IGetUserAccessTokenRequest
    {
        protected IGetUserAccessTokenRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetToken;
        protected override HttpMethod HttpMethod => HttpMethod.POST;
        protected override bool SupportsBody => false;
        ///<summary>/_security/oauth2/token</summary>
        public GetUserAccessTokenRequest(): base()
        {
        }
    }

    public interface IGetUserRequest : IRequest<GetUserRequestParameters>
    {
    }

    public class GetUserRequest : PlainRequestBase<GetUserRequestParameters>, IGetUserRequest
    {
        protected IGetUserRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetUser;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}</summary>
        public GetUserRequest(Names username): base(r => r.Optional("username", username))
        {
        }

        ///<summary>/_security/user</summary>
        public GetUserRequest(): base()
        {
        }
    }

    public interface IGetUserPrivilegesRequest : IRequest<GetUserPrivilegesRequestParameters>
    {
    }

    public class GetUserPrivilegesRequest : PlainRequestBase<GetUserPrivilegesRequestParameters>, IGetUserPrivilegesRequest
    {
        protected IGetUserPrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityGetUserPrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/_privileges</summary>
        public GetUserPrivilegesRequest(): base()
        {
        }
    }

    public interface IHasPrivilegesRequest : IRequest<HasPrivilegesRequestParameters>
    {
    }

    public class HasPrivilegesRequest : PlainRequestBase<HasPrivilegesRequestParameters>, IHasPrivilegesRequest
    {
        protected IHasPrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityHasPrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.POST;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/_has_privileges</summary>
        public HasPrivilegesRequest(): base()
        {
        }

        ///<summary>/_security/user/{user}/_has_privileges</summary>
        public HasPrivilegesRequest(Name user): base(r => r.Optional("user", user))
        {
        }
    }

    public interface IInvalidateApiKeyRequest : IRequest<InvalidateApiKeyRequestParameters>
    {
    }

    public class InvalidateApiKeyRequest : PlainRequestBase<InvalidateApiKeyRequestParameters>, IInvalidateApiKeyRequest
    {
        protected IInvalidateApiKeyRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityInvalidateApiKey;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/api_key</summary>
        public InvalidateApiKeyRequest(): base()
        {
        }
    }

    public interface IInvalidateUserAccessTokenRequest : IRequest<InvalidateUserAccessTokenRequestParameters>
    {
    }

    public class InvalidateUserAccessTokenRequest : PlainRequestBase<InvalidateUserAccessTokenRequestParameters>, IInvalidateUserAccessTokenRequest
    {
        protected IInvalidateUserAccessTokenRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityInvalidateToken;
        protected override HttpMethod HttpMethod => HttpMethod.DELETE;
        protected override bool SupportsBody => false;
        ///<summary>/_security/oauth2/token</summary>
        public InvalidateUserAccessTokenRequest(): base()
        {
        }
    }

    public interface IPutPrivilegesRequest : IRequest<PutPrivilegesRequestParameters>
    {
    }

    public class PutPrivilegesRequest : PlainRequestBase<PutPrivilegesRequestParameters>, IPutPrivilegesRequest
    {
        protected IPutPrivilegesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityPutPrivileges;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/privilege/</summary>
        public PutPrivilegesRequest(): base()
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IPutRoleRequest : IRequest<PutRoleRequestParameters>
    {
    }

    public class PutRoleRequest : PlainRequestBase<PutRoleRequestParameters>, IPutRoleRequest
    {
        protected IPutRoleRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityPutRole;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role/{name}</summary>
        public PutRoleRequest(Name name): base(r => r.Required("name", name))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IPutRoleMappingRequest : IRequest<PutRoleMappingRequestParameters>
    {
    }

    public class PutRoleMappingRequest : PlainRequestBase<PutRoleMappingRequestParameters>, IPutRoleMappingRequest
    {
        protected IPutRoleMappingRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityPutRoleMapping;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/role_mapping/{name}</summary>
        public PutRoleMappingRequest(Name name): base(r => r.Required("name", name))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IPutUserRequest : IRequest<PutUserRequestParameters>
    {
    }

    public class PutUserRequest : PlainRequestBase<PutUserRequestParameters>, IPutUserRequest
    {
        protected IPutUserRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityPutUser;
        protected override HttpMethod HttpMethod => HttpMethod.PUT;
        protected override bool SupportsBody => false;
        ///<summary>/_security/user/{username}</summary>
        public PutUserRequest(Name username): base(r => r.Required("username", username))
        {
        }

        public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }
    }

    public interface IGetCertificatesRequest : IRequest<GetCertificatesRequestParameters>
    {
    }

    public class GetCertificatesRequest : PlainRequestBase<GetCertificatesRequestParameters>, IGetCertificatesRequest
    {
        protected IGetCertificatesRequest Self => this;
        internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityCertificates;
        protected override HttpMethod HttpMethod => HttpMethod.GET;
        protected override bool SupportsBody => false;
        ///<summary>/_ssl/certificates</summary>
        public GetCertificatesRequest(): base()
        {
        }
    }
}