using UnityEngine;

namespace Game
{
    public static class Mouse
    {
        public static Vector3 GetMouseWorldPosition => ObjectFinder.Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}