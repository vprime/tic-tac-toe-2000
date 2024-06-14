using System;
using UnityEngine.Serialization;

namespace Game.Components
{
    [Serializable]
    public struct Player
    {
        public int index;

        public PlayerType type;

        public Player(int index, PlayerType type)
        {
            this.index = index;
            this.type = type;
        }

        public override string ToString()
        {
            return $"{index}";
        }
    }
}
