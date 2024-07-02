using Game.Components;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Game.Systems
{
    public class GameUi : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private VisualElement _announcePlayer;
        private VisualElement _announceWinner;
        private VisualElement _announceDraw;
        private Label _winnerName;
        private Label _playerName;
        
        private void OnEnable()
        {
            _announcePlayer = uiDocument.rootVisualElement.Q("AnnouncePlayer");
            _announceWinner = uiDocument.rootVisualElement.Q("AnnounceWinner");
            _announceDraw = uiDocument.rootVisualElement.Q("AnnounceDraw");
            
            _winnerName = uiDocument.rootVisualElement.Q("WinnerName") as Label;
            _playerName = uiDocument.rootVisualElement.Q("PlayerName") as Label;

            Assert.IsNotNull(_announcePlayer);
            Assert.IsNotNull(_announceWinner);
            Assert.IsNotNull(_announceDraw);
            Assert.IsNotNull(_winnerName);
            Assert.IsNotNull(_playerName);
            
            Clear();
        }

        public void Clear()
        {
            _announcePlayer.style.display = DisplayStyle.None;
            _announceWinner.style.display = DisplayStyle.None;
            _announceDraw.style.display = DisplayStyle.None;
        }

        public void AnnouncePlayer(PlayerSymbol player)
        {
            _announceWinner.style.display = DisplayStyle.None;
            _announceDraw.style.display = DisplayStyle.None;
            
            _playerName.text = $"{player}'s Turn";
            _announcePlayer.style.display = DisplayStyle.Flex;
        }

        public void AnnounceWinner(PlayerSymbol player)
        {
            _announcePlayer.style.display = DisplayStyle.None;
            _announceDraw.style.display = DisplayStyle.None;
            
            _winnerName.text = $"{player} Won";
            _announceWinner.style.display = DisplayStyle.Flex;
        }

        public void AnnounceDraw()
        {
            _announcePlayer.style.display = DisplayStyle.None;
            _announceWinner.style.display = DisplayStyle.None;
            _announceDraw.style.display = DisplayStyle.Flex;
        }
        
    }
}
