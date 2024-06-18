using UnityEngine;
using App.Components;
using Game;
using Game.Components;

namespace App.Systems
{
    public class MainMenuControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        [SerializeField] private AppUI appUi;
        [SerializeField] private GameControl gameControl;
        [SerializeField] private Board defaultBoard;
        
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void Start()
        {
            appUi.OnStartPlayX += HandleStartPlayerX;
            appUi.OnStartPlayO += HandleStartPlayerO;
            appUi.OnStartPlayTwoPlayer += HandleStartTwoPlayer;
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            appUi.gameObject.SetActive(next == AppStates.MainMenu);
        }

        private void HandleStartPlayerX()
        {
            var setup = new GameSetup
            {
                Board = defaultBoard
            };
            setup.Players.Add(PlayerSymbol.X, new Player(PlayerSymbol.X, PlayerType.Human));
            setup.Players.Add(PlayerSymbol.O, new Player(PlayerSymbol.O, PlayerType.AI));
            gameControl.GameSetup = setup;
            
            appControl.CurrentAppState = AppStates.GameCountdown;
        }

        private void HandleStartPlayerO()
        {
            var setup = new GameSetup
            {
                Board = defaultBoard
            };
            setup.Players.Add(PlayerSymbol.X, new Player(PlayerSymbol.X, PlayerType.AI));
            setup.Players.Add(PlayerSymbol.O, new Player(PlayerSymbol.O, PlayerType.Human));
            gameControl.GameSetup = setup;
            
            appControl.CurrentAppState = AppStates.GameCountdown;
        }

        private void HandleStartTwoPlayer()
        {
            var setup = new GameSetup
            {
                Board = defaultBoard
            };
            setup.Players.Add(PlayerSymbol.X, new Player(PlayerSymbol.X, PlayerType.Human));
            setup.Players.Add(PlayerSymbol.O, new Player(PlayerSymbol.O, PlayerType.Human));
            gameControl.GameSetup = setup;
            
            appControl.CurrentAppState = AppStates.GameCountdown;
        }
    }
}
