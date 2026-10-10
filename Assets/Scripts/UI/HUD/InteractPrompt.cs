using System;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class InteractPrompt : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] CanvasGroup interactPanel;

        public void SetLabelText(string labelText)
        {
            if (labelText == "")
            {
                interactPanel.alpha = 0;
                text.text = labelText;    
            }
            else
            {
                text.text = labelText;
                interactPanel.alpha = 1;
            }
            
            
        }
        
    }
}
