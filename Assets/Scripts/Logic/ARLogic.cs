using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ARLogic
{
    private Label label;
    private Button nextButton;
    private Button previousButton;
    private Button registerButton;
    private VisualElement mapContainer;
    private Label auxLabel;
    private Label registerLabel;
    private RadioButtonGroup registerGroup;
    private CoffeTarget coffeTarget;
    private NavigationMenuEvents navigationMenuEvents;

    private ARState state = ARState.NotDetected;
    public ARState State { get; }

    public ARLogic(UIDocument document, NavigationMenuEvents navigationMenuEvents)
    {
        label = document.rootVisualElement.Q<Label>("ARContainerLabel");
        nextButton = document.rootVisualElement.Q<Button>("ARNextButton");
        previousButton = document.rootVisualElement.Q<Button>("ARPrevButton");
        mapContainer = document.rootVisualElement.Q<VisualElement>("ARMapContainer");
        auxLabel = document.rootVisualElement.Q<Label>("ARAuxLabel");
        registerButton = document.rootVisualElement.Q<Button>("ARRegisterButton");

        registerLabel = document.rootVisualElement.Q<Label>("RegisterCoffeeLabel");
        registerGroup = document.rootVisualElement.Q<RadioButtonGroup>("RegisterCoffeeSelection");
        document.rootVisualElement.Q<Button>("RegisterCoffeeButton").clicked += () => RegisterCoffee();

        UpdateUI();
        nextButton.clicked += NextState;
        previousButton.clicked += PreviousState;

        registerButton.clicked += () => GoToRegister();

        this.navigationMenuEvents = navigationMenuEvents;
    }

    private void RegisterCoffee()
    {
        var register = PlayerPrefs.GetString("Register", "");
        PlayerPrefs.SetString("Register", $"{coffeTarget.CoffeSO.CoffeName}|{register}");
        PlayerPrefs.SetInt(coffeTarget.CoffeSO.CoffeName, registerGroup.value + 1);
        navigationMenuEvents.ShowDatabase();
        ResetState();
    }

    private void GoToRegister()
    {
        registerLabel.text = coffeTarget.CoffeSO.CoffeName;
        navigationMenuEvents.ShowRegister();
        ResetState();
    }

    public void SetState(ARState state, CoffeTarget coffeTarget)
    {
        this.coffeTarget = coffeTarget;
        SetState(state);
    }

    public void SetState(ARState state)
    {
        this.state = state;
        coffeTarget.UpdateCoffeInfo(state);
        UpdateUI();
    }

    private void UpdateUI()
    {
        label.text = GetLabel();

        if (state == ARState.NotDetected)
        {
            nextButton.style.display = DisplayStyle.None;
            previousButton.style.display = DisplayStyle.None;
        }
        else
        {
            nextButton.style.display = DisplayStyle.Flex;
            previousButton.style.display = DisplayStyle.Flex;
        }

        UpdateAuxLabel();

        if (state == ARState.ShowMap)
        {
            mapContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            mapContainer.style.display = DisplayStyle.None;
        }

        if (state == ARState.ShowRating && PlayerPrefs.GetInt(coffeTarget.CoffeSO.CoffeName, -1) == -1)
        {
            registerButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            registerButton.style.display = DisplayStyle.None;
        }
    }

    private void UpdateAuxLabel()
    {
        switch (state)
        {
            case ARState.ShowTaste:
                auxLabel.style.display = DisplayStyle.Flex;
                auxLabel.text = $"Sabor: {coffeTarget.CoffeSO.Taste}";
                break;
            case ARState.ShowAroma:
                auxLabel.style.display = DisplayStyle.Flex;
                auxLabel.text = $"Aroma: {coffeTarget.CoffeSO.Aroma}";
                break;
            case ARState.ShowRating:
                auxLabel.style.display = DisplayStyle.Flex;
                auxLabel.text = $"Opiniones recientes:\n{coffeTarget.CoffeSO.Opinion}";
                break;
            case ARState.ShowMap:
                auxLabel.style.display = DisplayStyle.Flex;
                auxLabel.text = $"Ubicación: {coffeTarget.CoffeSO.Location}\nAltura: {coffeTarget.CoffeSO.Altitude}";
                break;
            case ARState.ShowBean:
                auxLabel.style.display = DisplayStyle.Flex;
                auxLabel.text = $"Proceso: {coffeTarget.CoffeSO.Process}\nVariedad: {coffeTarget.CoffeSO.CoffeName}";
                break;
            default:
                auxLabel.text = "";
                auxLabel.style.display = DisplayStyle.None;
                break;
        }
    }

    public string GetLabel()
    {
        switch (state)
        {
            case ARState.ShowTaste:
                return "Sabor";
            case ARState.ShowAroma:
                return "Aroma";
            case ARState.ShowRating:
                return "Clasificación";
            case ARState.ShowMap:
                return "Origen";
            case ARState.ShowBean:
                return "Grano";
            default:
                return "Buscando...";
        }
    }

    public void NextState()
    {
        if (state == ARState.NotDetected) return;

        state = (ARState)(((int)state + 1) % 6);
        if (state == ARState.NotDetected) state++;
        coffeTarget.UpdateCoffeInfo(state);
        UpdateUI();
    }

    public void PreviousState()
    {
        if (state == ARState.NotDetected) return;

        state = (ARState)(((int)state + 5) % 6);
        if (state == ARState.NotDetected) state = (ARState)(((int)state + 5) % 6);
        coffeTarget.UpdateCoffeInfo(state);
        UpdateUI();
    }

    public void ResetState()
    {
        state = ARState.NotDetected;
        UpdateUI();
    }
}

/// <summary>
/// Represents the different states of the AR (Augmented Reality) experience.
/// </summary>
/// <remarks>
/// The possible states are:
/// <list type="bullet">
///   <item>
///     <term><c>NotDetected</c></term>
///     <description>AR target is not detected.</description>
///   </item>
///   <item>
///     <term><c>ShowTaste</c></term>
///     <description>Display information about taste.</description>
///   </item>
///   <item>
///     <term><c>ShowAroma</c></term>
///     <description>Display information about aroma.</description>
///   </item>
///   <item>
///     <term><c>ShowRating</c></term>
///     <description>Display rating information.</description>
///   </item>
///   <item>
///     <term><c>ShowMap</c></term>
///     <description>Display map or location information.</description>
///   </item>
///   <item>
///     <term><c>ShowBean</c></term>
///     <description>Display information about coffee beans.</description>
///   </item>
/// </list>
/// </remarks>
public enum ARState
{
    /// <summary>
    /// AR target is not detected.
    /// </summary>
    NotDetected,
    /// <summary>
    /// Display information about taste.
    /// </summary>
    ShowTaste,
    /// <summary>
    /// Display information about aroma.
    /// </summary>
    ShowAroma,
    /// <summary>
    /// Display rating information.
    /// </summary>
    ShowRating,
    /// <summary>
    /// Display map or location information.
    /// </summary>
    ShowMap,
    /// <summary>
    /// Display information about coffee beans.
    /// </summary>
    ShowBean
}