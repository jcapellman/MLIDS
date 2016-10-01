"use strict";
var redis = require("redis");
var settings = require("./config");
var redisFactory;
redisFactory = function (key, response) {
    var client = redis.createClient(settings.REDIS_DATABASE_PORT, settings.REDIS_DATABASE_HOSTNAME);
    client.on("error", function (err) {
        console.log("Error " + err);
    });
    client.get(key, function (err, reply) {
        if (reply == null) {
            response.writeHead(200, { 'Content-Type': 'application/json' });
            response.end("");
            return response;
        }
        response.writeHead(200, { 'Content-Type': 'application/json' });
        response.end(reply);
        return response;
    });
};
module.exports = redisFactory;
//# sourceMappingURL=DBFactory.js.map