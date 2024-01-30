using Catalog.Database.Mappings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Catalog.Database.Configurations;

public static class MongoDbMapping
{
    public static void ConfigureMappings()
    {
        var pack = new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String),
        };
        
        ConventionRegistry.Register("EnumStringConvention", pack, _ => true);

        CategoryMapping.Configure();
        ProductMapping.Configure();
    }
}