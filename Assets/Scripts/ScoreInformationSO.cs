using UnityEngine;

/// <summary>
/// ScriptableObject that holds score information for a game.
/// </summary>
[CreateAssetMenu(fileName = "ScoreInformation", menuName = "ScriptableObjects/ScoreInformationSO")]
public class ScoreInformationSO : ScriptableObject
{
    /// <summary>
    /// The base score awarded per match.
    /// </summary>
    public int scorePerMatch = 100;

    /// <summary>
    /// The multiplier applied to the score based on the combo count.
    /// </summary>
    public float scoreComboMultiplier = 1.5f;

    /// <summary>
    /// Calculates the score based on the combo count.
    /// </summary>
    /// <param name="comboCount">The number of combos achieved.</param>
    /// <returns>The calculated score.</returns>
    public int GetScore(int comboCount)
    {
        return (int) (Mathf.Pow(scoreComboMultiplier, comboCount) * scorePerMatch);
    }
}
