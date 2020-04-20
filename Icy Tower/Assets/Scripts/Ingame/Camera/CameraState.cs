#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "Camera States", menuName = "Scriptable Objects/Camera/Camera States")]
public class CameraState : ScriptableObject {

    public CameraStateEnums state;
    public float totalTime;
    public CameraTraversalData[] traversalDatas;

    public void Run() {
        CalculateWeights();

        MoveTo(0);
        RotateTo(0);
    }

    public void CalculateWeights() {
        float totalPositionWeight = 0;
        float totalRotationWeight = 0;
        foreach (CameraTraversalData data in traversalDatas) {
            totalPositionWeight += data.positionTimeWeight;
            totalRotationWeight += data.rotationTimeWeight;
        }

        float unitPositionWeight = totalTime / totalPositionWeight;
        float unitRotationWeight = totalTime / totalRotationWeight;

        foreach (CameraTraversalData data in traversalDatas) {
            data.SetPositionFlyTime(unitPositionWeight * data.positionTimeWeight);
            data.SetRotationFlyTime(unitRotationWeight * data.rotationTimeWeight);
        }
    }

    private void MoveTo(int stateIndex) {
        Vector3 targetPosition = traversalDatas[stateIndex].position;
        float targetPositionTime = traversalDatas[stateIndex].positionTimeWeight;
        LeanTweenType targetEaseType = traversalDatas[stateIndex].easeType;

        LeanTween.move(Camera.main.gameObject, targetPosition, targetPositionTime / totalTime)
            .setEase(targetEaseType)
            .setOnComplete(() => {
                stateIndex++;

                if (stateIndex < traversalDatas.Length) {
                    MoveTo(stateIndex);
                }
            });
    }

    private void RotateTo(int stateIndex) {
        Vector3 targetRotation = traversalDatas[stateIndex].rotation;
        float targetRotationTime = traversalDatas[stateIndex].rotationTimeWeight;
        LeanTweenType targetEaseType = traversalDatas[stateIndex].easeType;

        LeanTween.rotate(Camera.main.gameObject, targetRotation, targetRotationTime / totalTime)
            .setEase(targetEaseType)
            .setOnComplete(() => {
                stateIndex++;

                if (stateIndex < traversalDatas.Length) {
                    RotateTo(stateIndex);
                }
            });
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraState))]
public class CameraStateEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CameraState cameraState = (CameraState)target;
        cameraState.CalculateWeights();
    }
}
#endif