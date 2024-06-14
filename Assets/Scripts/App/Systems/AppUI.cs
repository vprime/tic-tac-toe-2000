using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.Systems
{
    public class AppUI : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private VisualElement _menuRoot;
        private VisualElement _players;
        private VisualElement _playerSymbol;

        private Button _singlePlayerButton;
        private Button _twoPlayerButton;
        private Button _playXButton;
        private Button _playOButton;

        public event Action OnStartPlayX;
        public event Action OnStartPlayO;
        public event Action OnStartPlayTwoPlayer;
        
        
        // Start is called before the first frame update
        void OnEnable()
        {
            _menuRoot = uiDocument.rootVisualElement.Q("root");
            _players = uiDocument.rootVisualElement.Q("PlayerModeSelect");

            _menuRoot.visible = true;
            _players.visible = true;
            _playerSymbol.visible = false;
            
            _playerSymbol = uiDocument.rootVisualElement.Q("SinglePlayerSymbolSelect");
            _singlePlayerButton = uiDocument.rootVisualElement.Q("SinglePlayerButton") as Button;
            _twoPlayerButton = uiDocument.rootVisualElement.Q("TwoPlayerButton") as Button;
            _playXButton = uiDocument.rootVisualElement.Q("PlayX") as Button;
            _playOButton = uiDocument.rootVisualElement.Q("PlayO") as Button;

            _singlePlayerButton?.RegisterCallback<ClickEvent>(HandleSinglePlayerMode);
            _twoPlayerButton?.RegisterCallback<ClickEvent>(HandleTwoPlayerMode);
            _playXButton?.RegisterCallback<ClickEvent>(HandlePlayX);
            _playOButton?.RegisterCallback<ClickEvent>(HandlePlayO);
        }

        private void OnDisable()
        {
            _singlePlayerButton.UnregisterCallback<ClickEvent>(HandleSinglePlayerMode);
            _twoPlayerButton.UnregisterCallback<ClickEvent>(HandleTwoPlayerMode);
            _playXButton.UnregisterCallback<ClickEvent>(HandlePlayX);
            _playOButton.UnregisterCallback<ClickEvent>(HandlePlayO);
        }

        void HandleSinglePlayerMode(ClickEvent evt)
        {
            _players.visible = false;
            _playerSymbol.visible = true;
            
        }

        void HandleTwoPlayerMode(ClickEvent evt)
        {
            OnStartPlayTwoPlayer?.Invoke();
            gameObject.SetActive(false);
        }

        void HandlePlayX(ClickEvent evt)
        {
            OnStartPlayX?.Invoke();
            gameObject.SetActive(false);
        }

        void HandlePlayO(ClickEvent evt)
        {
            OnStartPlayO?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
