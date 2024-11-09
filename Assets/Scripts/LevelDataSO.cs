using UnityEngine;

/// <summary>
/// Represents data for a level in the Monari Unity project.
/// </summary>
/// <remarks>
/// This ScriptableObject includes the grid size and associated save data for the level (if we have it).
/// </remarks>
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public Vector2Int gridSize;
    public SaveData saveData;

}
