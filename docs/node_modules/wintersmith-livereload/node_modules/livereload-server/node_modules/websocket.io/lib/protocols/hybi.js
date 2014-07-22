
/*!
 * websocket.io
 * Copyright(c) 2011 LearnBoost <dev@learnboost.com>
 * MIT Licensed
 */

/**
 * Module requirements.
 */

var Socket = require('../socket')
  , WebSocketServer = require('ws').Server
  , debug = require('debug')('wsio:hybi');

/**
 * Export the constructor.
 */

exports = module.exports = WebSocket;

/**
 * HTTP interface constructor. Interface compatible with all transports that
 * depend on request-response cycles.
 *
 * @api public
 */

function WebSocket (req) {
  var self = this;
  this.wss = new WebSocketServer({
      noServer: true
    , clientTracking: false
  });
  Socket.call(this, req);
};

/**
 * Inherits from Socket.
 */

WebSocket.prototype.__proto__ = Socket.prototype;

/**
 * Transport name
 *
 * @api public
 */

WebSocket.prototype.name = 'websocket';

/**
 * Websocket draft version
 *
 * @api public
 */

WebSocket.prototype.protocolVersion = 'hybi';

/**
 * Called when the socket connects.
 *
 * @api private
 */

WebSocket.prototype.onOpen = function () {
  var self = this;

  this.wss.handleUpgrade(this.req, this.socket, this.req.head, function(ws) {
    self.ws = ws;
    self.protocolVersion = 'hybi-' + ws.protocolVersion;

    ws.on('message', function(message) {
      self.onMessage(message);
    });
    ws.on('close', function () {
      self.end();
    });
    ws.on('error', function (reason) {
      debug(self.name + ' parser error: ' + reason);
      self.end();
    });

    process.nextTick(function() {
      if ('opening' == self.readyState) {
        self.readyState = 'open';
        self.emit('open');
      }
    });
  });
};

/**
 * Writes to the socket.
 *
 * @api private
 */

WebSocket.prototype.write = function (data) {
  if ('open' == this.readyState && this.ws) {
    this.ws.send(data);
    debug(this.name + ' writing', data);
  }
};

/**
 * Writes a payload.
 *
 * @api private
 */

WebSocket.prototype.payload = function (msgs) {
  for (var i = 0, l = msgs.length; i < l; i++) {
    this.write(msgs[i]);
  }
  return this;
};
