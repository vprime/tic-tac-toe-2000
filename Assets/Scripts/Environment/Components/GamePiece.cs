using System;
using System.Collections;
using System.Collections.Generic;
using Environment.Systems;
using Game.Components;
using UnityEngine;

namespace Environment.Components
{
    public class GamePiece : MonoBehaviour
    {
        [SerializeField] private ClickableObject clickableObject;
        [SerializeField] private DigitSegmentRenderer digitSegmentRenderer;

        [SerializeField] private Material humanXColor;
        [SerializeField] private Material humanOColor;
        [SerializeField] private Material aiColor;
        
        private int _columnAddress;
        private int _rowAddress;
        private Action<int, int, GamePiece> _interactionCallback;
        private Player? _setPlayer;

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
            _setPlayer = player;
            switch (player.symbol)
            {
                case PlayerSymbol.X:
                    switch (player.type)
                    {
                        case PlayerType.Human:
                            digitSegmentRenderer.SetLitMaterial(humanXColor);
                            break;
                        case PlayerType.AI:
                            digitSegmentRenderer.SetLitMaterial(aiColor);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    digitSegmentRenderer.SetCharacter('X');
                    break;
                case PlayerSymbol.O:
                    switch (player.type)
                    {
                        case PlayerType.Human:
                            digitSegmentRenderer.SetLitMaterial(humanOColor);
                            break;
                        case PlayerType.AI:
                            digitSegmentRenderer.SetLitMaterial(aiColor);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    digitSegmentRenderer.SetCharacter('O');
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClearPlayer()
        {
            digitSegmentRenderer.SetSegments(new List<DigitSegments>());
            _setPlayer = null;
            StartCoroutine(AnimateRejection());
        }

        public void RejectInput()
        {
            // Game rejected player input, play an animation to indicate this.
            StartCoroutine(AnimateRejection());
        }

        public void Light()
        {
            digitSegmentRenderer.SetLitMaterial(aiColor);
            digitSegmentRenderer.LightAll();
        }

        IEnumerator AnimateRejection()
        {
            yield return null;
            digitSegmentRenderer.DarkenAll();
            yield return new WaitForSeconds(0.2f);
            if(_setPlayer.HasValue)
                SetPlayer(_setPlayer.Value);
        }

        private void HandlePress()
        {
            digitSegmentRenderer.LightAll();
            _interactionCallback?.Invoke(_columnAddress, _rowAddress, this);
        }
    }
}
