using UnityEngine;

namespace Utilities
{
    public static class MouseUtils
    {
        public static Vector3 GetMouseWorldPosition => Services.GetServiceFromComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
}