using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{

    #region Singleton

    public static UIManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private ControllerType _selectedControllerType = ControllerType.Joystick;

    [SerializeField]
    private Panel[] _panels = null;

    [SerializeField]
    private GameObject _imageOverlayBG = null;

    private Stack<Panel> _panelStack = new Stack<Panel>();

    [SerializeField]
    private SettingsPanel _settingsPanel = null;

    private void Start() {
        OpenPanel("PnlMainMenu");

        _settingsPanel.SetController(_selectedControllerType);
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
            CloseImageOverlayBG();
            return;
        }

        _imageOverlayBG.SetActive(true);
    }

    private void CloseImageOverlayBG() {
        _imageOverlayBG.SetActive(false);
    }
}
