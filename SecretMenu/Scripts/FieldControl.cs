//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class FieldControl<T> : MonoBehaviour
{
    [SerializeField]
    private Text _header;

    [SerializeField]
    private InputField _inputField;

    public InputField InputField
    {
        get
        {
            return _inputField;
        }

        set
        {
            _inputField = value;
        }
    }

    public void SetOption(T option, string header = null)
    {
        if (header != null)
        {
            _header.text = header;
        }
        if (option != null)
        {
            InputField.text = option.ToString();
        }
    }
}