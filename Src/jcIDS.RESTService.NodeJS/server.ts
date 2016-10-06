"use strict";
var restify = require("restify");
var config = require("./config");
var server = restify.createServer();
server.use(restify.queryParser());
console.log("Listing on port " + config["default"].HTTP_SERVER_PORT + "...");
server.listen(config["default"].HTTP_SERVER_PORT);
