using UnityEngine;

namespace Utilities
{
    public static class MouseUtils
    {
        public static Vector3 GetMouseWorldPosition => Services.GetServiceFromScene<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
}