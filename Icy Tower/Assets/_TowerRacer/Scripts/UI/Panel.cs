using UnityEngine;

public class Panel : MonoBehaviour{

    [SerializeField]
    private UIPanels _panel;

    public UIPanels PanelEnum {
        get { return _panel; }
    }

    [SerializeField]
    private OpenPanelTween _openPanelTween;

    public void Open() {
        _openPanelTween.OpenPanel();
    }

    public void Close() {
        _openPanelTween.ClosePanel();
    }

}
