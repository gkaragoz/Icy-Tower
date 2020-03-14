using UnityEngine;

public class GameAssets : MonoBehaviour {
    public AudioClip jumpSound;
    public AudioClip comboJumpSound;

    private static GameAssets _instance;

    #region Singleton
    public static GameAssets instance {
        get {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _instance;
        }
    }

    #endregion

}
