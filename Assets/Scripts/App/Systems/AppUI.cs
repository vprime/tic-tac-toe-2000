using System;
using UnityEngine;
using UnityEngine.Assertions;
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
        private void OnEnable()
        {
            Debug.Log("AppUI Enabled");
            _menuRoot = uiDocument.rootVisualElement.Q("root");
            _players = uiDocument.rootVisualElement.Q("PlayerModeSelect");
            _playerSymbol = uiDocument.rootVisualElement.Q("SinglePlayerSymbolSelect");
            
            _singlePlayerButton = uiDocument.rootVisualElement.Q("SinglePlayerButton") as Button;
            _twoPlayerButton = uiDocument.rootVisualElement.Q("TwoPlayerButton") as Button;
            _playXButton = uiDocument.rootVisualElement.Q("PlayX") as Button;
            _playOButton = uiDocument.rootVisualElement.Q("PlayO") as Button;

            Assert.IsNotNull(_menuRoot);
            Assert.IsNotNull(_players);
            Assert.IsNotNull(_playerSymbol);
            Assert.IsNotNull(_singlePlayerButton);
            Assert.IsNotNull(_twoPlayerButton);
            Assert.IsNotNull(_playXButton);
            Assert.IsNotNull(_playOButton);
            
            _players.style.display = DisplayStyle.Flex;
            _playerSymbol.style.display = DisplayStyle.None;

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

        private void HandleSinglePlayerMode(ClickEvent evt)
        {
            Debug.Log("Single player mode selected");
            _players.style.display = DisplayStyle.None;
            _playerSymbol.style.display = DisplayStyle.Flex;
        }

        private void HandleTwoPlayerMode(ClickEvent evt)
        {
            OnStartPlayTwoPlayer?.Invoke();
            gameObject.SetActive(false);
        }

        private void HandlePlayX(ClickEvent evt)
        {
            OnStartPlayX?.Invoke();
            gameObject.SetActive(false);
        }

        private void HandlePlayO(ClickEvent evt)
        {
            OnStartPlayO?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
