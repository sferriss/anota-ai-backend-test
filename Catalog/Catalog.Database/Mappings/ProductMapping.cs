﻿using Catalog.Domain.Products;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Catalog.Database.Mappings;

internal class ProductMapping
{
    internal static void Configure()
    {
        BsonClassMap.RegisterClassMap<Product>(map =>
        {
            map.AutoMap();

            map.SetIgnoreExtraElements(true);

            map.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new SingleSerializer(BsonType.ObjectId));
            
            map.MapMember(x => x.Title)
                .SetElementName("title")
                .SetIsRequired(true);

            map.MapMember(x => x.Description)
                .SetElementName("description")
                .SetIsRequired(true);
            
            map.MapMember(x => x.Owner)
                .SetElementName("owner")
                .SetIsRequired(true);
            
            map.MapMember(x => x.Price)
                .SetElementName("price")
                .SetIsRequired(true);
            
            map.MapMember(x => x.Category)
                .SetElementName("category")
                .SetIsRequired(true);
        });
    }
}