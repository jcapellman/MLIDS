var restify = require("restify");

import config from "./config";

var server = restify.createServer();

server.use(restify.queryParser());

console.log(`Listing on port ${config.HTTP_SERVER_PORT}...`);

server.listen(config.HTTP_SERVER_PORT);