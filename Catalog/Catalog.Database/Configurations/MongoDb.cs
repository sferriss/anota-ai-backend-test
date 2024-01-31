using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Catalog.Database.Configurations;

public static class MongoDb
{
    public static void Configure()
    {
        var pack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String),
        };
        
        ConventionRegistry.Register("EnumStringConvention", pack, _ => true);
    }
}