# LiveReload server in Node.js

Implementation of the server side of the [LiveReload protocol](https://github.com/livereload/livereload-protocol) in Node.js; a component of LiveReload.app and some other projects.

Status: beta (expected to work, but not tested in production yet).


## Installation

    npm install livereload-server


## Example

See example/dummy-server.js for a runnable version of this code:

    var fs   = require('fs');
    var Path = require('path');
    var LRWebSocketServer = require('livereload-server');

    // id, name, version identifies your app;
    // protocols specifies the versions of subprotocols you support
    var server = new LRWebSocketServer({ id: "com.example.acme", name: "Acme", version: "1.0", protocols: { monitoring: 7, saving: 1 } });

    server.on('connected', function(connection) {
      console.log("Client connected (%s)", connection.id);
    });

    server.on('disconnected', function(connection) {
      console.log("Client disconnected (%s)", connection.id);
    });

    server.on('command', function(connection, message) {
      console.log("Received command %s: %j", message.command, message);
    });

    server.on('error', function(err, connection) {
      console.log("Error (%s): %s", connection.id, err.message);
    });

    server.on('livereload.js', function(request, response) {
      console.log("Serving livereload.js.");
      fs.readFile(Path.join(__dirname, 'res/livereload.js'), 'utf8', function(err, data) {
        if (err) throw err;

        response.writeHead(200, {'Content-Length': data.length, 'Content-Type': 'text/javascript'});
        response.end(data);
      });
    });

    server.on('httprequest', function(url, request, response) {
      response.writeHead(404);
      response.end()
    });

    server.listen(function(err) {
        if (err) {
            console.error("Listening failed: %s", err.message);
            return;
        }
        console.log("Listening on port %d.", server.port);
    });



## License

© 2012, Andrey Tarantsov, distributed under the MIT license.
