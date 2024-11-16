using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigationMenuEvents : MonoBehaviour
{
    private Color backgroundColor;
    private Color transparentColor = new Color(0, 0, 0, 0);

    private UIDocument _document;

    private GameObject arCamera;

    private VisualElement arContainer;
    private VisualElement settingsContainer;
    private VisualElement databaseContainer;
    private VisualElement discoverContainer;
    private VisualElement mainContainer;
    private VisualElement registerContainer;
    private ScrollView databaseScrollContainer;


    private ARLogic arLogic;

    private static readonly CustomStyleProperty<Color> _secondaryColor = new CustomStyleProperty<Color>("--secondary-color");

    void Start()
    {
        _document = GetComponent<UIDocument>();
        arCamera = GameObject.Find("ARCamera");

        arContainer = _document.rootVisualElement.Q<VisualElement>("ARContainer");
        databaseContainer = _document.rootVisualElement.Q<VisualElement>("DatabaseContainer");
        discoverContainer = _document.rootVisualElement.Q<VisualElement>("DiscoverContainer");
        settingsContainer = _document.rootVisualElement.Q<VisualElement>("SettingsContainer");
        mainContainer = _document.rootVisualElement.Q<VisualElement>("MainContainer");
        registerContainer = _document.rootVisualElement.Q<VisualElement>("RegisterContainer");
        databaseScrollContainer = _document.rootVisualElement.Q<ScrollView>("DatabaseScrollContainer");

        _document.rootVisualElement.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);

        _document.rootVisualElement.Q<Button>("DatabaseButton").clicked += () => ShowDatabase();
        _document.rootVisualElement.Q<Button>("DiscoverButton").clicked += () => ShowDiscover();
        _document.rootVisualElement.Q<Button>("SettingsButton").clicked += () => ShowSettings();
        _document.rootVisualElement.Q<Button>("ARButton").clicked += () => ShowAR();
        _document.rootVisualElement.Q<Button>("Logout").clicked += () =>
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        _document.rootVisualElement.Q<Button>("DeleteAll").clicked += () => PlayerPrefs.DeleteAll();

        arLogic = new ARLogic(_document, this);

        ShowDatabase();
    }

    private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
    {
        if (evt.customStyle.TryGetValue(_secondaryColor, out var secondaryColor))
            backgroundColor = secondaryColor;
    }

    internal void ShowDatabase()
    {
        // Get all values from PlayerPrefs that start with "Coffee"
        var keys = PlayerPrefs.GetString("Register", "").Split('|');
        foreach (var key in keys)
        {
            Debug.Log(key);
        }
        var coffees = new List<(CoffeSO, int)>();
        foreach (var key in keys)
        {
            if (key == "") continue;
            var value = PlayerPrefs.GetInt(key);
            var coffe = Resources.Load<CoffeSO>($"SO/Coffee_{key}");
            coffees.Add((coffe, value));
        }

        databaseScrollContainer.Clear();
        foreach (var (coffe, value) in coffees)
        {
            if (coffe == null) continue;

            var coffeElement = new VisualElement();
            coffeElement.style.flexDirection = FlexDirection.Row;
            coffeElement.style.justifyContent = Justify.Center;
            coffeElement.style.alignItems = Align.Center;

            var coffeLabel = new Label($"{coffe.CoffeName} | Calificación: {value}");
            coffeLabel.style.borderLeftWidth = 0;
            coffeLabel.style.borderRightWidth = 0;
            coffeLabel.style.borderTopWidth = 5;
            coffeLabel.style.borderBottomWidth = 5;
            coffeLabel.style.borderTopColor = new Color(1, 0.67f, 0, 1);
            coffeLabel.style.borderBottomColor = new Color(1, 0.67f, 0, 1);
            coffeLabel.style.fontSize = 40;
            coffeLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            coffeElement.Add(coffeLabel);

            databaseScrollContainer.Add(coffeElement);
        }


        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.Flex;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.None;
        registerContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = backgroundColor;
    }

    private void ShowDiscover()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;
        registerContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = backgroundColor;
    }

    private void ShowSettings()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.Flex;
        registerContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = backgroundColor;
    }

    private void ShowAR()
    {
        arCamera.SetActive(true);
        arContainer.style.display = DisplayStyle.Flex;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.None;
        registerContainer.style.display = DisplayStyle.None;
        mainContainer.style.backgroundColor = transparentColor;
    }

    public void OnTargetFound(CoffeTarget target)
    {
        arLogic.SetState(ARState.ShowMap, target);
    }

    public void OnTargetLost()
    {
        arLogic.SetState(ARState.NotDetected);
    }

    internal void ShowRegister()
    {
        arCamera.SetActive(false);
        arContainer.style.display = DisplayStyle.None;
        databaseContainer.style.display = DisplayStyle.None;
        discoverContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.None;
        registerContainer.style.display = DisplayStyle.Flex;

        mainContainer.style.backgroundColor = backgroundColor;
    }
}
