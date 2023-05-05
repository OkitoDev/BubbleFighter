using System;
using Enums;
using UnityEngine;

namespace Utilities
{
    public static class ProjectConfig
    {
        public static string GetTagName(TagType tagType)
        {
            return tagType switch
            {
                TagType.Player => "Player",
                TagType.Enemy => "Enemy",
                _ => throw new ArgumentOutOfRangeException(nameof(tagType), tagType, null)
            };
        }
        
        public static int GetLayerId(LayerType layerType)
        {
            return layerType switch
            {
                LayerType.ProjectileSpawnedByPlayer => LayerMask.NameToLayer("ProjectileSpawnedByPlayer"),
                LayerType.ProjectileSpawnedByEnemy => LayerMask.NameToLayer("ProjectileSpawnedByEnemy"),
                _ => throw new ArgumentOutOfRangeException(nameof(layerType), layerType, null)
            };
        }
    }
}