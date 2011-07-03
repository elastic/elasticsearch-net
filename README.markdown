# NEST

*Strongly typed Elasticsearch client*

NEST aims to be a .net client with a very concise API. 

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	client.Index(post);

Indexing asynchronously is as easy as:

	client.IndexAsync(post, (c) => /* called later */);

For more examples please refer to the [Wiki](https://github.com/Mpdreamz/NEST/wiki "Read more about NEST's interface")

* [Connecting](https://github.com/Mpdreamz/NEST/wiki/Connecting)
* [Indexing](https://github.com/Mpdreamz/NEST/wiki/Indexing)
* [Searching](https://github.com/Mpdreamz/NEST/wiki/Searching)
* [Deleting](https://github.com/Mpdreamz/NEST/wiki/Deleting)

## Copyright

Copyright (c) 2010 Martijn Laarman and everyone wonderful enough to contribute to [NEST](https://github.com/Mpdreamz/NEST)

## License

NEST is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/Mpdreamz/NEST/blob/master/src/license.txt) for more information.
