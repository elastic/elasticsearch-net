using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.User
{
	[SkipVersion("<2.3.0", "")]
	public class UserCrudTests
		: CrudTestBase<XPackCluster, IPutUserResponse, IGetUserResponse, IPutUserResponse, IDeleteUserResponse>
	{
		private readonly string[] _roles = { "user" };

		public UserCrudTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		//Since we basically take the first 8 characters of a guid we have no way
		//to guarantee it starts with a-zA-Z which is mandatory since 5.1
		protected override string Sanitize(string callDistinctValue) => "u" + callDistinctValue;

		protected override LazyResponses Create() => Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, IPutUserResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.PutUser(s, f),
			(s, c, f) => c.PutUserAsync(s, f),
			(s, c, r) => c.PutUser(r),
			(s, c, r) => c.PutUserAsync(r)
		);

		protected PutUserRequest CreateInitializer(string username) => new PutUserRequest(username)
		{
			Password = username, Roles = _roles
		};

		protected IPutUserRequest CreateFluent(string username, PutUserDescriptor d) => d
			.Password(username)
			.Roles(_roles);

		protected override LazyResponses Read() => Calls<GetUserDescriptor, GetUserRequest, IGetUserRequest, IGetUserResponse>(
			ReadInitializer,
			ReadFluent,
			(s, c, f) => c.GetUser(f),
			(s, c, f) => c.GetUserAsync(f),
			(s, c, r) => c.GetUser(r),
			(s, c, r) => c.GetUserAsync(r)
		);

		protected GetUserRequest ReadInitializer(string username) => new GetUserRequest(username);

		protected IGetUserRequest ReadFluent(string username, GetUserDescriptor d) => d.Username(username);

		protected override LazyResponses Update() => Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, IPutUserResponse>(
			UpdateInitializer,
			UpdateFluent,
			(s, c, f) => c.PutUser(s, f),
			(s, c, f) => c.PutUserAsync(s, f),
			(s, c, r) => c.PutUser(r),
			(s, c, r) => c.PutUserAsync(r)
		);

		protected PutUserRequest UpdateInitializer(string username) => new PutUserRequest(username)
		{
			FullName = username,
			Roles = _roles
		};

		protected IPutUserRequest UpdateFluent(string username, PutUserDescriptor d) => d
			.FullName(username)
			.Roles(_roles);

		protected override LazyResponses Delete() => Calls<DeleteUserDescriptor, DeleteUserRequest, IDeleteUserRequest, IDeleteUserResponse>(
			DeleteInitializer,
			DeleteFluent,
			(s, c, f) => c.DeleteUser(s, f),
			(s, c, f) => c.DeleteUserAsync(s, f),
			(s, c, r) => c.DeleteUser(r),
			(s, c, r) => c.DeleteUserAsync(r)
		);

		protected DeleteUserRequest DeleteInitializer(string username) => new DeleteUserRequest(username);

		protected IDeleteUserRequest DeleteFluent(string username, DeleteUserDescriptor d) => d;

		protected override void ExpectAfterCreate(IGetUserResponse response)
		{
			response.Users.Should().NotBeEmpty();
			var user = response.Users.First().Value;
			user.Username.Should().NotBeNullOrWhiteSpace();
			user.Roles.Should().NotBeNullOrEmpty();
			user.FullName.Should().BeNullOrEmpty();
		}

		protected override void ExpectAfterUpdate(IGetUserResponse response)
		{
			response.Users.Should().NotBeEmpty();
			var user = response.Users.First().Value;
			user.Username.Should().NotBeNullOrWhiteSpace();
			user.Roles.Should().NotBeNullOrEmpty();
			user.FullName.Should().NotBeNullOrWhiteSpace();
		}
	}
}
