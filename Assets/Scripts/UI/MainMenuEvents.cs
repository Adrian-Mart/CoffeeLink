using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> _users = new Dictionary<string, string>
    {
        {"user1", "password1"},
        {"user2", "password2"},
        {"user3", "password3"}
    };

    private UIDocument _uiDocument;
    
    private Button _loginButton;
    private TextField _usernameField;
    private TextField _passwordField;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _loginButton = _uiDocument.rootVisualElement.Q<Button>("LoginButton");
        _usernameField = _uiDocument.rootVisualElement.Q<TextField>("UserField");
        _passwordField = _uiDocument.rootVisualElement.Q<TextField>("PasswordField");
        _loginButton.RegisterCallback<ClickEvent>(ev => OnLoginButtonClicked());
    }

    private void OnLoginButtonClicked()
    {
        var username = _usernameField.value;
        var password = _passwordField.value;

        if (_users.ContainsKey(username) && _users[username] == password)
        {
            Debug.Log("Login successful");
        }
        else
        {
            Debug.Log("Login failed");
        }
    }
}
