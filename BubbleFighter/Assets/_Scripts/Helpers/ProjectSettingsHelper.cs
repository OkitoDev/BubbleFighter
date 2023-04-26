using System;
using Enums;

namespace Helpers
{
    public static class ProjectSettingsHelper
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
    }
}