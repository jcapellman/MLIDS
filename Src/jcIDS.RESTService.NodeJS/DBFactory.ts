var redis = require("redis");
import config from "./config";

var redisFactory;

redisFactory = (key, response) => {
    const client = redis.createClient(config.REDIS_DATABASE_PORT, config.REDIS_DATABASE_HOSTNAME);

    client.on("error",
        err => {
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

export default redisFactory;