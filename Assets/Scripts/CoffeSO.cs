using UnityEngine;

/// <summary>
/// ScriptableObject representing a coffee with various attributes.
/// </summary>
[CreateAssetMenu(fileName = "CoffeSO", menuName = "Scriptable Objects/Coffe", order = 1)]
public class CoffeSO : ScriptableObject
{
    /// <summary>
    /// The name of the coffee.
    /// </summary>
    [SerializeField] private string coffeName;

    /// <summary>
    /// A description of the coffee.
    /// </summary>
    [SerializeField] private string description;

    /// <summary>
    /// A map sprite associated with the coffee.
    /// </summary>
    [SerializeField] private Sprite map;

    /// <summary>
    /// The altitude at which the coffee is grown.
    /// </summary>
    [SerializeField] private string altitude;

    /// <summary>
    /// The location where the coffee is grown.
    /// </summary>
    [SerializeField] private string location;

    /// <summary>
    /// The process used to produce the coffee.
    /// </summary>
    [SerializeField] private string process;

    /// <summary>
    /// An opinion or review of the coffee.
    /// </summary>
    [SerializeField] private string opinion;

    /// <summary>
    /// The aroma of the coffee.
    /// </summary>
    [SerializeField] private string aroma;

    /// <summary>
    /// The taste of the coffee.
    /// </summary>
    [SerializeField] private string taste;

    /// <summary>
    /// Gets the name of the coffee.
    /// </summary>
    public string CoffeName { get => coffeName; }

    /// <summary>
    /// Gets the description of the coffee.
    /// </summary>
    public string Description { get => description; }

    /// <summary>
    /// Gets the map sprite associated with the coffee.
    /// </summary>
    public Sprite Map { get => map; }

    /// <summary>
    /// Gets the altitude at which the coffee is grown.
    /// </summary>
    public string Altitude { get => altitude; }

    /// <summary>
    /// Gets the location where the coffee is grown.
    /// </summary>
    public string Location { get => location; }

    /// <summary>
    /// Gets the process used to produce the coffee.
    /// </summary>
    public string Process { get => process; }

    /// <summary>
    /// Gets an opinion or review of the coffee.
    /// </summary>
    public string Opinion { get => opinion; }

    /// <summary>
    /// Gets the aroma of the coffee.
    /// </summary>
    public string Aroma { get => aroma; }

    /// <summary>
    /// Gets the taste of the coffee.
    /// </summary>
    public string Taste { get => taste; }
}
