"use strict";
var restify = require("restify");
var config_1 = require("./config");
var server = restify.createServer();
server.use(restify.queryParser());
console.log("Listing on port " + config_1["default"].HTTP_SERVER_PORT + "...");
server.listen(config_1["default"].HTTP_SERVER_PORT);
