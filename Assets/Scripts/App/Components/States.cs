namespace App.Components
{
    public enum RunStates
    {
        Playing,
        Paused
    }

    public enum AppStates
    {
        Init,
        LoadAssets,
        OpeningSequence,
        MainMenu,
        GameCountdown,
        Game,
        CelebrateWinner,
        GameResults,
    }
}
