"use strict";
var redis = require("redis");
const config_1 = require("./config");
var redisFactory;
redisFactory = (key, response) => {
    const client = redis.createClient(config_1.default.REDIS_DATABASE_PORT, config_1.default.REDIS_DATABASE_HOSTNAME);
    client.on("error", err => {
        console.log(`Error ${err}`);
    });
    client.get(key, (err, reply) => {
        if (reply == null) {
            response.writeHead(200, { "Content-Type": "application/json" });
            response.end("");
            return response;
        }
        response.writeHead(200, { "Content-Type": "application/json" });
        response.end(reply);
        return response;
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = redisFactory;
//# sourceMappingURL=DBFactory.js.map