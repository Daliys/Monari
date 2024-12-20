
using System;

/// <summary>
/// Class that holds the game statistics, such as the number of pairs found, turns taken, and score.
/// </summary>
public class GameStatistic
{
    public static event Action<int> OnPairsFoundCountChanged;
    public static event Action<int> OnTurnsCountChanged;
    public static event Action<int> OnScoreChanged;

    /// <summary>
    /// The number of pairs found in the game.
    /// </summary>
    public int PairsFoundCount { get; private set; }

    /// <summary>
    /// The number of turns taken in the game.
    /// </summary>
    public int TurnsCount { get; private set; }

    /// <summary>
    /// The score achieved in the game.
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    ///  The number of consecutive matches made in the game.
    /// </summary>
    public int ComboCount { get; private set; }


    private readonly ScoreInformationSO scoreInformation;


    public GameStatistic(ScoreInformationSO scoreInformation)
    {
        this.scoreInformation = scoreInformation;
        PairsFoundCount = 0;
        TurnsCount = 0;
        Score = 0;
        ComboCount = 0;

        CallEvents();
    }


    public GameStatistic(ScoreInformationSO scoreInformation, int pairsFoundCount, int turnsCount, int score, int comboCount)
    {
        this.scoreInformation = scoreInformation;
        PairsFoundCount = pairsFoundCount;
        TurnsCount = turnsCount;
        Score = score;
        ComboCount = comboCount;

        CallEvents();

    }

    /// <summary>
    ///  Updates the game statistics based on the result of a swap.
    /// </summary>
    /// <param name="isMatch">Whether the swap resulted in a match.</param>
    public void OnCompleteSwap(bool isMatch)
    {
        if (isMatch)
        {
            PairsFoundCount++;
            TurnsCount++;
            ComboCount++;
            Score += scoreInformation.GetScore(ComboCount);
        }
        else
        {
            TurnsCount++;
            ComboCount = 0;
        }

        CallEvents();

    }

    /// <summary>
    ///  Resets all game statistics to their initial values.
    /// </summary>
    public void ResetAll()
    {
        PairsFoundCount = 0;
        TurnsCount = 0;
        Score = 0;
        ComboCount = 0;

        OnPairsFoundCountChanged?.Invoke(PairsFoundCount);
        OnTurnsCountChanged?.Invoke(TurnsCount);
        OnScoreChanged?.Invoke(Score);
    }

    private void CallEvents()
    {
        OnPairsFoundCountChanged?.Invoke(PairsFoundCount);
        OnTurnsCountChanged?.Invoke(TurnsCount);
        OnScoreChanged?.Invoke(Score);
    }

}
