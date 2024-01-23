using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField] Button startButton;
    TMP_InputField m_NameInputField;


    private void Start()
    {
        m_NameInputField = GetComponent<TMP_InputField>();
        m_NameInputField.onValueChanged.AddListener(name => SaveName(name));
        m_NameInputField.onEndEdit.AddListener(name => SaveName(name));

    }

    private void SaveName(string name)
    {
        if (name == string.Empty)
        {
            startButton.interactable = false;
            return;
        }

        startButton.interactable = true;
        PlayerManager.Instance.SavePlayerName(name);
    }

}
