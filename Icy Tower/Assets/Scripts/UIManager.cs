using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour{

    [SerializeField]
    private Panel[] _panels = null;

    private Stack<Panel> _panelStack = new Stack<Panel>(); 

    public void OpenPanel(string panelEnum) {
        foreach (Panel panel in _panels) {
            if(panel.PanelEnum.ToString() == panelEnum) {
                panel.Open();
                _panelStack.Push(panel);
            } 
        }
    }

    public void ClosePanel() {
        Panel closedPanel =_panelStack.Pop();

        if(closedPanel == null) {
            return;
        }

        closedPanel.Close();
    }

    public UIPanels GetActivePanel() {
        return _panelStack.Peek().PanelEnum;
    }

}
