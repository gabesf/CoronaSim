using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelControl : MonoBehaviour
{
    public GameObject panel;
    public Toggle panelToggle;
    // Start is called before the first frame update

    private void Start()
    {
        panelToggle.onValueChanged.AddListener(delegate { ChangePanelActive(); });
    }

    private void ChangePanelActive()
    {
        panel.SetActive(panelToggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
