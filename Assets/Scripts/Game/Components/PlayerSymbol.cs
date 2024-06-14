using UnityEngine;

namespace Game.Components
{
    [CreateAssetMenu(fileName = "DEFAULT - Symbol", menuName = "Game/Components/Player Symbol")]
    public class PlayerSymbol : ScriptableObject
    {
        /// <summary>
        /// Object rendered for player symbol.
        /// </summary>
        public GameObject renderedPrefab;
        
        
    }
}
