using UnityEngine;

public class GameLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPositionTransform;

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderStartPosition()
    {
        return landerStartPositionTransform.position;
    }

}
