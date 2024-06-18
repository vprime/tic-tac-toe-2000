using System;
using Environment.Systems;
using Game.Components;
using UnityEngine;

namespace Environment.Components
{
    public class GamePiece : MonoBehaviour
    {
        [SerializeField] private ClickableObject clickableObject;
        [SerializeField] private PlayerSymbols playerSymbols;
        
        private int _columnAddress;
        private int _rowAddress;
        private Action<int, int, GamePiece> _interactionCallback;
        private GameObject _activePlayerSymbol;

        private void OnEnable()
        {
            clickableObject.OnPress += HandlePress;
        }

        private void OnDisable()
        {
            clickableObject.OnPress -= HandlePress;
        }

        public void Setup(int column, int row, Action<int, int, GamePiece> interactionCallback)
        {
            _columnAddress = column;
            _rowAddress = row;
            _interactionCallback = interactionCallback;
        }

        public void SetPlayer(Player player)
        {
            // Update the renderer to display the chosen player. 
            RenderPlayerSymbol(playerSymbols.GetPlayerSymbol(player));
        }

        public void RejectInput()
        {
            // Game rejected player input, play an animation to indicate this.
        }

        private void RenderPlayerSymbol(GameObject prefab)
        {
            if (_activePlayerSymbol)
                Destroy(_activePlayerSymbol);
            _activePlayerSymbol = Instantiate(prefab, transform);
        }

        private void HandlePress()
        {
            _interactionCallback?.Invoke(_columnAddress, _rowAddress, this);
        }
    }
}
