using UnityEngine;
using UnityEngine.UIElements;
using World;

public class MainMenuManager : MonoBehaviour
{
    private VisualElement _root;
    private void Awake()
    {
          _root = GetComponent<UIDocument>().rootVisualElement;
          SetUpButtons();
    }

    private void SetUpButtons()
    {
        Button startButton = _root.Q<Button>("startButton");
        startButton.clickable.clicked += StartGame;
        
    }

    private void StartGame()
    {
        StartCoroutine(WorldSaveGameManager.instance.LoadScene());
    }
    
}
