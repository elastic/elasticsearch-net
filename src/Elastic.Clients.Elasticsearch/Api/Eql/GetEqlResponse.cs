// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Eql;

public partial class GetEqlResponse<TEvent>
{
	private IReadOnlyCollection<HitsEvent<TEvent>> _events;
	private IReadOnlyCollection<HitsSequence<TEvent>> _sequences;


	[JsonIgnore]
	public IReadOnlyCollection<HitsEvent<TEvent>> Events =>
		_events ??= Hits?.Events ?? EmptyReadOnly<HitsEvent<TEvent>>.Collection;

	[JsonIgnore]
	public IReadOnlyCollection<HitsSequence<TEvent>> Sequences =>
		_sequences ??= Hits?.Sequences ?? EmptyReadOnly<HitsSequence<TEvent>>.Collection;

	[JsonIgnore]
	public long Total => Hits?.Total.Value ?? -1;
}
