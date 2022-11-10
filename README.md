# Dotnet Redis Examples

Welcome this repo shows some examples as to how to interact with the different data structures in .NET, open the .sln file and look at `BasicRedisCommands/program.cs` for some examples of how to interact with the different redis commands

## Spin up Redis

To run these examples, you will need to be using [Redis Stack](https://redis.io/docs/stack/). The easiest way to spin up a quick instance of Redis Stack is with docker:

```bash
docker run -p 6379:6379 -p 8001:8001 redis/redis-stack
```

## Helpful Links

* [StackExchange.Redis](https://github.com/stackExchange/StackExchange.Redis)
* For all things Redis Related you can check out [redis.io](http://redis.io/)
* If you want to learn more about indexing in Redis and using LINQ with redis
Check out the [Redis OM repo](https://github.com/redis/redis-om-dotnet)
