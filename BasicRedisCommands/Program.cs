// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();

//Wipe out the whole redis instance
db.Execute("FLUSHDB");

// sets a string in Redis
db.StringSet("foo", "bar", TimeSpan.FromSeconds(50));

// gets a string from Redis
var stringGetResult = db.StringGet("foo");
Console.WriteLine($"Current value of foo: {stringGetResult}");

// adds an item to a set in Redis
db.SetAdd("myset", "thing-1");
db.SetAdd("myset", "thing-3");
db.SetAdd("myset2", "thing-2");

// checks if an item exists in a set
var inSet = db.SetContains("myset", "thing-1");
var str = inSet ? "was" : "was not";
Console.WriteLine($"thing-1 {str} in myset");

// gets the members of a set
var setMembers = db.SetMembers("myset");
Console.WriteLine($"My set members: {string.Join(',', setMembers)}");

// unions two sets together
var union = db.SetCombine(SetOperation.Union,"myset","myset2");
Console.WriteLine($"Union members: {string.Join(',', union)}");

// adds steve with a time to the sorted set
db.SortedSetAdd("users:last_visited", "Steve", 1651259876 );
db.SortedSetAdd("users:last_visited", "Bob", 1651259876 );

// gets all the members of a sorted set
var usersLastVisited = db.SortedSetRangeByRankWithScores("users:last_visited");
Console.WriteLine(string.Join(',', usersLastVisited));


// sets a hash with fields within the hash
db.HashSet("dog:1", new HashEntry[] {new ("name", "Honey"), new ("breed", "Greyhound")});

// gets a field from a hash
var name = db.HashGet("dog:1", "name");
Console.WriteLine($"dog:1 name from hash: {name}");

// delete dog hash key so we can reuse in JSON
db.KeyDelete("dog:1");

// sets a dog object as a json document in Redis
db.Execute("JSON.SET", "dog:1", "$", "{\"name\":\"Honey\",\"breed\":\"Greyhound\"}");

// gets a field from the JSON object in Redis
db.Execute("JSON.GET", "dog:1", "$.breed");

Console.WriteLine($"dog:1 name from JSON: {name}");

// adds a message to a sensor's stream
db.StreamAdd("sensor:1", "temp", 27);

// reads a single item off of a stream
var msg = db.StreamRead("sensor:1", 0).First();
Console.WriteLine($"Stream message {msg.Id}: {msg.Values.First().Name}, {msg.Values.First().Value}" );

// Create the consumer group
db.StreamCreateConsumerGroup("sensor:1", "avg", 0);

// reads a stream in the context of a group
msg = db.StreamReadGroup("sensor:1", "avg", "con1", ">").First();
Console.WriteLine($"Stream message {msg.Id}: {msg.Values.First().Name}, {msg.Values.First().Value}" );
