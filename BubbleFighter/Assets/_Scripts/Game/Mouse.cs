using UnityEngine;

namespace Game
{
    public static class Mouse
    {
        public static Vector2 GetMouseWorldPosition => ObjectFinder.Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}