using UnityEngine;

public class Panel : MonoBehaviour{

    [SerializeField]
    private UIPanels _panel;

    public UIPanels PanelEnum {
        get { return _panel; }
    }

    public void Open() {
        this.gameObject.SetActive(true);
    }

    public void Close() {
        this.gameObject.SetActive(false);
    }

}
