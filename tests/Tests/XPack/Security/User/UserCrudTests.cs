// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.User
{
	[SkipVersion("<2.3.0", "")]
	public class UserCrudTests
		: CrudTestBase<Security, PutUserResponse, GetUserResponse, PutUserResponse, DeleteUserResponse>
	{
		private readonly string[] _roles = { "user" };

		public UserCrudTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

		//Since we basically take the first 8 characters of a guid we have no way
		//to guarantee it starts with a-zA-Z which is mandatory since 5.1
		protected override string Sanitize(string callDistinctValue) => "u" + callDistinctValue;

		protected override LazyResponses Create() => Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.Security.PutUser(s, f),
			(s, c, f) => c.Security.PutUserAsync(s, f),
			(s, c, r) => c.Security.PutUser(r),
			(s, c, r) => c.Security.PutUserAsync(r)
		);

		protected PutUserRequest CreateInitializer(string username) => new PutUserRequest(username)
		{
			Password = username, Roles = _roles
		};

		protected IPutUserRequest CreateFluent(string username, PutUserDescriptor d) => d
			.Password(username)
			.Roles(_roles);

		protected override LazyResponses Read() => Calls<GetUserDescriptor, GetUserRequest, IGetUserRequest, GetUserResponse>(
			ReadInitializer,
			ReadFluent,
			(s, c, f) => c.Security.GetUser(f),
			(s, c, f) => c.Security.GetUserAsync(f),
			(s, c, r) => c.Security.GetUser(r),
			(s, c, r) => c.Security.GetUserAsync(r)
		);

		protected GetUserRequest ReadInitializer(string username) => new GetUserRequest(username);

		protected IGetUserRequest ReadFluent(string username, GetUserDescriptor d) => d.Username(username);

		protected override LazyResponses Update() => Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
			UpdateInitializer,
			UpdateFluent,
			(s, c, f) => c.Security.PutUser(s, f),
			(s, c, f) => c.Security.PutUserAsync(s, f),
			(s, c, r) => c.Security.PutUser(r),
			(s, c, r) => c.Security.PutUserAsync(r)
		);

		protected PutUserRequest UpdateInitializer(string username) => new PutUserRequest(username)
		{
			FullName = username,
			Roles = _roles
		};

		protected IPutUserRequest UpdateFluent(string username, PutUserDescriptor d) => d
			.FullName(username)
			.Roles(_roles);

		protected override LazyResponses Delete() => Calls<DeleteUserDescriptor, DeleteUserRequest, IDeleteUserRequest, DeleteUserResponse>(
			DeleteInitializer,
			DeleteFluent,
			(s, c, f) => c.Security.DeleteUser(s, f),
			(s, c, f) => c.Security.DeleteUserAsync(s, f),
			(s, c, r) => c.Security.DeleteUser(r),
			(s, c, r) => c.Security.DeleteUserAsync(r)
		);

		protected DeleteUserRequest DeleteInitializer(string username) => new DeleteUserRequest(username);

		protected IDeleteUserRequest DeleteFluent(string username, DeleteUserDescriptor d) => d;

		protected override void ExpectAfterCreate(GetUserResponse response)
		{
			response.Users.Should().NotBeEmpty();
			var user = response.Users.First().Value;
			user.Username.Should().NotBeNullOrWhiteSpace();
			user.Roles.Should().NotBeNullOrEmpty();
			user.FullName.Should().BeNullOrEmpty();
		}

		protected override void ExpectAfterUpdate(GetUserResponse response)
		{
			response.Users.Should().NotBeEmpty();
			var user = response.Users.First().Value;
			user.Username.Should().NotBeNullOrWhiteSpace();
			user.Roles.Should().NotBeNullOrEmpty();
			user.FullName.Should().NotBeNullOrWhiteSpace();
		}

		protected override void ExpectDeleteNotFoundResponse(DeleteUserResponse response)
		{
			response.Found.Should().BeFalse();
			response.ServerError.Should().BeNull();
		}
	}
}
