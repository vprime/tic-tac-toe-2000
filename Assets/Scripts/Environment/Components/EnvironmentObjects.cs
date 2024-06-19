using UnityEngine;

namespace Environment.Components
{
    
    [CreateAssetMenu(fileName = "DEFAULT - Environment Objects", menuName = "Environment/Components/Environment Objects")]
    public class EnvironmentObjects : ScriptableObject
    {
        public GameObject gameBoardBackground;
        public GamePiece gamePiece;
        public float columnHeight;
        public float rowWidth;
    }
}
