using UnityEngine;

public class GameLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPositionTransform;
    [SerializeField] private Transform cameraStartTargetTransform;
    [SerializeField] private float zoomedOutOrthographicSize;

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderStartPosition()
    {
        return landerStartPositionTransform.position;
    }

    public Transform GetCameraStartTargetTransform()
    {
        return cameraStartTargetTransform;
    }

    public float GetZoomedOutOrthographicSize()
    {
        return zoomedOutOrthographicSize;
    }

}
