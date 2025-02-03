using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUiController : MonoBehaviour
{
    private class Panel
    {
        public Button OpenButton { get; private set; }
        public Button CloseButton { get; private set; }
        public VisualElement PanelElement { get; private set; }

        public Action<Panel> OnOpenButtonClicked;

        public PanelType Type { get; private set; }
        public const string PANEL = "Panel";
        public const string BUTTON = "Button";
        public const string CLOSE = "Close";

        public Panel(VisualElement root, PanelType type)
        {
            Type = type;
            OpenButton = root.Q<Button>(Type.ToString() + BUTTON);
            CloseButton = root.Q<Button>(Type.ToString() + PANEL + CLOSE + BUTTON);
            PanelElement = root.Q<VisualElement>(Type.ToString() + PANEL);

            OpenButton?.RegisterCallback<ClickEvent>(OpenPanel);
            CloseButton?.RegisterCallback<ClickEvent>(ClosePanel);
        }

        private void OpenPanel(ClickEvent clickEvent)
        {
            OnOpenButtonClicked?.Invoke(this);

            if (PanelElement == null)
                return;

            PanelElement.style.display = DisplayStyle.Flex;
        }

        private void ClosePanel(ClickEvent clickEvent)
        {
            if (PanelElement == null) 
                return;

            PanelElement.style.display = DisplayStyle.None;
        }
    }

    private enum PanelType
    {
        Play,
        Inventory,
        Achievements,
        Settings,
        Quit
    }

    private VisualElement _root;

    private Panel[] _panels;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _panels = new Panel[5];
        for (int i = 0; i < _panels.Length; i++)
        {
            var type = (PanelType)i;
            _panels[i] = new Panel(_root, type);
            switch (type)
            {
                case PanelType.Play:
                    break;
                case PanelType.Inventory:
                    break;
                case PanelType.Achievements:
                    break;
                case PanelType.Settings:
                    break;
                case PanelType.Quit:
                    _panels[i].OnOpenButtonClicked += OnQuitButtonClicked;
                    break;
            }
        }
    }

    private void OnPlayButtonClicked(ClickEvent clickEvent)
    {

    }

    private void OnInventoryButtonClicked(ClickEvent clickEvent)
    {

    }

    private void OnAchievmentButtonClicked(ClickEvent clickEvent)
    {

    }

    private void OnSettingsButtonClicked(ClickEvent clickEvent)
    {

    }

    private void OnQuitButtonClicked(Panel panel)
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }
}

