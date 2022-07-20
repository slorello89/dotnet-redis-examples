// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();

// sets a string in Redis
db.StringSet("foo", "bar", TimeSpan.FromSeconds(50));

// gets a string from Redis
var stringGetResult = db.StringGet("foo");

// adds an item to a set in Redis
db.SetAdd("myset", "thing-1");
db.SetAdd("myset2", "thing-2");

// checks if an item exists in a set
var inSet = db.SetContains("myset", "thing-1");

// gets the members of a set
var setMembers = db.SetMembers("myset");

// unions two sets together
db.SetCombine(SetOperation.Union,"myset","myset2");

// adds steve with a time to the sorted set
db.SortedSetAdd("users:last_visited", "steve", 1651259876 );

// gets all the members of a sorted set
db.SortedSetRangeByRank("users:last_visited");

// sets a hash with fields within the hash
db.HashSet("dog:1", new HashEntry[] {new ("name", "Honey"), new ("breed", "Greyhound")});

// gets a field from a hash
db.HashGet("dog:1", "name");

db.KeyDelete("dog:1");

// sets a dog object as a json document in Redis
db.Execute("JSON.SET", "dog:1", "$", "{\"name\":\"Honey\",\"breed\":\"Greyhound\"}");

// gets a field from the JSON object in Redis
db.Execute("JSON.GET", "dog:1", "$.breed");

// adds a message to a sensor's stream
db.StreamAdd("sensor:1", "temp", 27);

// reads a single item off of a stream
db.StreamRead("sensor:1", 0);

var info = db.StreamGroupInfo("sensor:1");
if (!info.Any(g => g.Name == "avg"))
{
    db.StreamCreateConsumerGroup("sensor:1", "avg");
}

// reads a stream in the context of a group
db.StreamReadGroup("sensor:1", "avg", "con1", ">");
