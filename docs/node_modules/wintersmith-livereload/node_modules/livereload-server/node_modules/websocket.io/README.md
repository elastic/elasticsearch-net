WebSocket.IO
============

[![Build Status](https://secure.travis-ci.org/LearnBoost/websocket.io.png)](http://travis-ci.org/LearnBoost/websocket.io)

WebSocket.IO is an abstraction of the websocket server previously used by Socket.IO.
It has the broadest support for websocket protocol/specifications and an API that
allows for interoperability with higher-level frameworks such as
[Engine](http://github.com/learnboost/engine.io),
[Socket.IO](http://github.com/learnboost/socket.io)'s realtime core.

## Features

- Fast
- Minimalistic
  - Offers an integration API for higher-level impls to handle authorization,
    routing, etc
- Widest support of protocols
  - Draft-75
  - Draft-76
  - Draft-00
  - Protocol version 7
  - Protocol version 8
  - Protocol version 13
- Written for Node 0.6

## How to use

### Server

#### (A) Listening on a port

```js
var ws = require('websocket.io')
  , server = ws.listen(3000)

server.on('connection', function (socket) {
  socket.on('message', function () { });
  socket.on('close', function () { });
});
```

#### (B) Intercepting WebSocket requests for a http.Server

```js
var ws = require('websocket.io')
  , http = require('http').createServer().listen(3000)
  , server = ws.attach(http)

server.on('connection', function (socket) {
  socket.on('message', function () { });
  socket.on('close', function () { });
});
```

#### (C) Passing in requests

```js
var ws = require('websocket.io')
  , server = new ws.Server()

server.on('connection', function (socket) {
  socket.send('hi');
});

// …
httpServer.on('upgrade', function (req, socket, head) {
  server.handleUpgrade(req, socket, head);
});
```

### Client-side example

```js
var ws = new WebSocket("ws://host:port/");        

socket.onopen = function() {
 //do something when connection estabilished
};

socket.onmessage = function(message) {
 //do something when message arrives
};

socket.onclose = function() {
 //do something when connection close
};

```

## API

### Server

<hr><br>

#### Top-level

These are exposed by `require('websocket.io')`:

##### Properties

- `version` _(String)_: websocket.io version
- `protocols` _(Object)_: constructors for drafts 75/76/00, protocols 7/8/13.

##### Methods

- `listen`
    - Creates an `http.Server` which listens on the given port and attaches WS
      to it. It returns `501 Not Implemented` for regular http requests.
    - **Parameters**
      - `Number`: port to listen on
      - `Object`: optional options object. See `Server` for options details.
      - `Function`: callback for `listen`
    - **Returns** `Server`
- `attach`
    - Captures `upgrade` requests for a `http.Server`. In other words, makes a regular `http.Server` websocket-compatible.
    - **Parameters**
      - `http.Server`: server to attach to.
      - `Object`: optional, options object. See `Server` for options details.
    - **Returns** `Server`

<hr><br>

#### Server

The main server/manager. _Inherits from EventEmitter_.

##### Properties

- `clients` _(Object)_: table of all connected clients. This property is
  not set when the `Server` option `clientTracking` is `false`.
- `clientsCount` _(Number)_: total count of connected clients. This property is
  not set when the `Server` option `clientTracking` is `false`.

##### Events

- `connection`
    - Fired when a new connection is established. 
    - **Arguments**
      - `Socket`: a Socket object

##### Methods

- **constructor**
    - Initializes the server
    - **Options**
      - path (`String`): if set, server only listens to request for this path
      - clientTracking (`Boolean`): enables client tracking. The
        `Server#clients` property only is available when this is true (`true`)
- `handleUpgrade`

<hr><br>

#### Socket

A representation of a client. _Inherits from EventEmitter_.

##### Properties

- `req` _(http.ServerRequest)_: Request that originated the connection.
- `socket` _(net.Socket)_: Stream that originated the connection.
- `readyState` _(String)_: `opening`, `open`, `closed`.
- `name` _(String)_: `websocket-hixie`, `websocket`.
- `protocolVersion` _(String)_: `hixie-75`, `hixie-76` and `hybi`.

##### Events

- `close`
    - Fired when the connection is closed.
- `message`/`data`
    - Fired when data is received
    - **Arguments**
      - `String`: utf-8 string
- `error`
    - Fired when an error occurs.
    - **Arguments**
      - `Error`: error object

##### Methods

- `send` / `write`
    - Sends a message.
    - **Parameters**
      - `String`: utf-8 string with outgoing data
    - **Returns** `Socket` for chaining
- `close` / `end`
    - Closes the socket
    - **Returns** `Socket` for chaining
- `destroy`
    - Forcibly closes the socket.

## Support

The support channels for `websocket.io` are the same as `socket.io`:

  * irc.freenode.net **#socket.io**
  * [Google Groups](http://groups.google.com/group/socket_io)
  * [Website](http://socket.io)

## Development

To contribute patches, run tests or benchmarks, make sure to clone the
repository:

```
git clone git://github.com/LearnBoost/websocket.io.git
```

Then:

```
cd websocket.io
npm install
```

## Tests

```
$ make test
```

## Credits

WebSocket.IO is possible thanks to the contributions by:

- Einar Otto Stangvik [einaros@gmail.com]
- Arnout Kazemier [info@3rd-eden.com]
- Nico Kaiser [nico@kaiser.me]

## License 

(The MIT License)

Copyright (c) 2011 Guillermo Rauch &lt;guillermo@learnboost.com&gt;

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
'Software'), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
