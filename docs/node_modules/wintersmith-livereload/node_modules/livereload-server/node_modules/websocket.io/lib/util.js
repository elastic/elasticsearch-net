
/*!
 * websocket.io
 * Copyright(c) 2011 LearnBoost <dev@learnboost.com>
 * MIT Licensed
 */

/**
 * Module dependencies.
 */

/**
 * Converts an enumerable to an array.
 *
 * @api public
 */

exports.toArray = function (enu) {
  var arr = [];

  for (var i = 0, l = enu.length; i < l; i++)
    arr.push(enu[i]);

  return arr;
};

/**
 * Unpacks a buffer to a number.
 *
 * @api public
 */

exports.unpack = function (buffer) {
  var n = 0;
  for (var i = 0; i < buffer.length; ++i) {
    n = (i == 0) ? buffer[i] : (n * 256) + buffer[i];
  }
  return n;
}

/**
 * Left pads a string.
 *
 * @api public
 */

exports.padl = function (s,n,c) { 
  return new Array(1 + n - s.length).join(c) + s;
}

/**
 * Writes value to the buffer at the specified offset with specified endian format.
 * Note, value must be a valid unsigned 16 bit integer.
 *
 * @api public
 */

exports.writeUInt16BE = function (value, offset) {
  this[offset] = (value & 0xff00)>>8;
  this[offset+1] = value & 0xff;
}

/**
 * Writes value to the buffer at the specified offset with specified endian format.
 * Note, value must be a valid unsigned 32 bit integer.
 *
 * @api public
 */

exports.writeUInt32BE = function (value, offset) {
  this[offset] = (value & 0xff000000)>>24;
  this[offset+1] = (value & 0xff0000)>>16;
  this[offset+2] = (value & 0xff00)>>8;
  this[offset+3] = value & 0xff;
}
