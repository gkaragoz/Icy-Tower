using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{

    [SerializeField]
    private Panel[] _panels = null;

    [SerializeField]
    private GameObject _imageOverlayBG = null;

    private Stack<Panel> _panelStack = new Stack<Panel>();

    private void Awake() {
        OpenPanel("PnlMainMenu");
    }

    public void OpenPanel(string panelEnum) {
        foreach (Panel panel in _panels) {
            if(panel.PanelEnum.ToString() == panelEnum) {
                panel.Open();
                _panelStack.Push(panel);
            } 
        }
        OpenImageOverlayBG();
    }

    public void ClosePanel() {
        Panel closedPanel =_panelStack.Pop();

        if(closedPanel == null) {
            return;
        }

        closedPanel.Close();

        if (_panelStack.Count == 0) {
            CloseImageOverlayBG();
        }
    }

    public void ClosePanel(string panelEnum) {
        foreach (Panel panel in _panels) {
            if (panel.PanelEnum.ToString() == panelEnum) {
                panel.Close();
                _panelStack.Pop();
            }
        }
        CloseImageOverlayBG();
    }

    public UIPanels GetActivePanel() {
        return _panelStack.Peek().PanelEnum;
    }

    private void OpenImageOverlayBG() {
        UIPanels activePanel = GetActivePanel();

        if (activePanel== UIPanels.PnlGamePlay || activePanel == UIPanels.PnlMainMenu) {
            return;
        }

        _imageOverlayBG.SetActive(true);
    }

    private void CloseImageOverlayBG() {
        _imageOverlayBG.SetActive(false);
    }
}
