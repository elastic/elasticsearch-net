using System.Linq;
using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Documents;

[UsesVerify]
public class MGetSerialization : SerializerTestBase
{
	[U]
	public async Task DeserializesError()
	{
		var json = @"{""docs"":[{""_index"":""devs"",""_id"":""1001"",""error"":{""root_cause"":[{""type"":""routing_missing_exception"",""reason"":""routing is required for [devs]/[1001]"",""index_uuid"":""_na_"",""index"":""devs""}],""type"":""routing_missing_exception"",""reason"":""routing is required for [devs]/[1001]"",""index_uuid"":""_na_"",""index"":""devs""}}]}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<MultiGetResponse<Developer>>(stream);

		search.Docs.Count.Should().Be(1);
		var error = search.Docs.First().Item2;
		error.Should().NotBeNull();

		await Verifier.Verify(error);
	}
}
