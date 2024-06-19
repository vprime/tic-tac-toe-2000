using UnityEngine;

namespace Environment.Components
{
    [CreateAssetMenu(fileName = "DEFAULT - Button", menuName = "Environment/Components/Button")]
    public class ButtonConfig : ScriptableObject
    {
        public float maxPressDepth = 0.1f;
        public float buttonSnapTime = 0.5f;
        public float buttonCooldownTime = 1f;
    }
}
