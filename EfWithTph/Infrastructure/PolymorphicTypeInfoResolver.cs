using EfWithTph.Models;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;

namespace EfWithTph.Infrastructure;

public static class PolymorphicTypeInfoResolver
{
    public static void Modifier(JsonTypeInfo typeInfo)
    {
        if (typeInfo.Type == typeof(BaseRoadmapItem))
        {
            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = false,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(RoadmapActivity), nameof(RoadmapActivity)),
                    new JsonDerivedType(typeof(RoadmapMilestone), nameof(RoadmapMilestone))
                }
            };
        }
    }
}
