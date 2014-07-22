# LiveReload protocol parser

Handles [LiveReload protocol](http://help.livereload.com/kb/ecosystem/livereload-protocol) handshake, validates and parses incoming commands, validates outgoing commands.

## Synopsis

    var Parser = require('livereload-parser');
    parser = new Parser('server', { monitoring: [Parser.protocols.MONITORING_7] });
    parser.on('error', function(err) {
      console.error('Error %s when parsing incoming message: %s', err.code, err.message);
      mywebsocket.close();
    });
    parser.on('connected', function() {
      console.log('Handshake done! Protocols: %j', parser.negotiatedProtocols);
      if (parser.negotiatedProtocols.monitoring >= 7) {
        console.log('TODO: activate monitoring here');
      }
    });
    parser.on('command', function(message) {
      console.log('Incoming command %s, data %j', message.command, message);
    });
    mywebsocket.on('data', function(data) {
      parser.received(data);
    });

## Installation

    npm install livereload-protocol

## Running tests

    npm test

## License

© 2012, Andrey Tarantsov, distributed under the MIT license.
