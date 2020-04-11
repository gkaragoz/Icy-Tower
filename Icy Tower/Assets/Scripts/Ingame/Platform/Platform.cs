using UnityEngine;
[RequireComponent(typeof(PlatformStats))]

public class Platform : MonoBehaviour, IPooledObject {

    [SerializeField]
    [Utils.ReadOnly]
    private PlatformStats _platformStats;
    private void Awake() {
        _platformStats = GetComponent<PlatformStats>();
    }

    private void SetScale() {
        transform.localScale = new Vector3(Random.Range(_platformStats.GetMinScale(), _platformStats.GetMaxScale()), _platformStats.GetThickness(), _platformStats.GetPrefab().transform.localScale.z);
    }

    private void SetPosition() {


        float x = WorldSettings.instance.GetRandomBorderPosition().x;
        float xSag = transform.position.x+(transform.localScale.x / 2);
        float xSol= transform.position.x- (transform.localScale.x / 2);

        if (xSag>WorldSettings.instance.GetMapRightBorderPosition().x)
        {
            float difference = xSag - WorldSettings.instance.GetMapRightBorderPosition().x;
            x -= difference;
        }else if(xSol < WorldSettings.instance.GetMapLeftBorderPosition().x)
        {
            float difference = WorldSettings.instance.GetMapLeftBorderPosition().x-xSol;
            x -= difference;
        }
       


        //float possibleX = WorldSettings.instance.GetRandomBorderPosition().x;
        //if (possibleX >= 0) {
        //    x = WorldSettings.instance.GetRandomBorderPosition().x + (transform.localScale.x / 2);

        //}
        //else {
        //    x = WorldSettings.instance.GetRandomBorderPosition().x - (transform.localScale.x / 2);
        //}
  
        float y = SpawnManager.instance.LastSpawnedPlatformPos += _platformStats.GetDistanceBetweenPlatforms();
        float z = 0;
        Vector3 _randomPos = new Vector3(x, y, z);

        transform.position = _randomPos;
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
        SetPosition();
        SetScale();
    }
}
