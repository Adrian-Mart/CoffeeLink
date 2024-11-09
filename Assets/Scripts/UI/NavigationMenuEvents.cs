using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigationMenuEvents : MonoBehaviour
{
    [SerializeField] private Color backgroundColor;
    private Color transparentColor = new Color(0, 0, 0, 0);

    private UIDocument _document;

    private GameObject arCamera;

    private VisualElement arContainer;
    private VisualElement settingsContainer;
    private VisualElement databaseContainer;
    private VisualElement discoverContainer;
    private VisualElement mainContainer;

    void Start()
    {
        _document = GetComponent<UIDocument>();
        arCamera = GameObject.Find("ARCamera");

        arContainer = _document.rootVisualElement.Q<VisualElement>("ARContainer");
        databaseContainer = _document.rootVisualElement.Q<VisualElement>("DatabaseContainer");
        discoverContainer = _document.rootVisualElement.Q<VisualElement>("DiscoverContainer");
        settingsContainer = _document.rootVisualElement.Q<VisualElement>("SettingsContainer");
        mainContainer = _document.rootVisualElement.Q<VisualElement>("MainContainer");

        _document.rootVisualElement.Q<Button>("DatabaseButton").clicked += () => ShowDatabase();
        _document.rootVisualElement.Q<Button>("DiscoverButton").clicked += () => ShowDiscover();
        _document.rootVisualElement.Q<Button>("SettingsButton").clicked += () => ShowSettings();
        _document.rootVisualElement.Q<Button>("ARButton").clicked += () => ShowAR();

        ShowDatabase();
    }

    private void ShowDatabase()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.Flex;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = backgroundColor;
        
    }

    private void ShowDiscover()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = backgroundColor;
    }

    private void ShowSettings()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.Flex;
        mainContainer.style.backgroundColor = backgroundColor;
    }

    private void ShowAR()
    {
        arCamera.SetActive(true);
        arContainer.style.display = DisplayStyle.Flex;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = transparentColor;
    }
}
