using System;
using Game.Components;
using UnityEngine;

namespace Environment.Components
{
    [CreateAssetMenu(fileName = "DEFAULT - Player Symbols", menuName = "Environment/Components/Player Symbols")]
    public class PlayerSymbols : ScriptableObject
    {
        // There are only 4 options, and this needs to be serialized so instead of writing editor code I'm just listing
        // all the options here.
        [SerializeField] private GameObject humanX;
        [SerializeField] private GameObject humanO;
        [SerializeField] private GameObject aiX;
        [SerializeField] private GameObject aiO;

        public GameObject GetPlayerSymbol(Player player)
        {
            return player.type switch
            {
                PlayerType.Human when player.symbol == PlayerSymbol.X => humanX,
                PlayerType.Human => humanO,
                PlayerType.AI when player.symbol == PlayerSymbol.X => aiX,
                PlayerType.AI => aiO,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
