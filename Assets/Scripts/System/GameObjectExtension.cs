using UnityEngine;

namespace System
{
    public static class GameObjectExtension
    {
        public static void SetActivity(this Component component, bool value) => component.gameObject.SetActive(value);
    }
}