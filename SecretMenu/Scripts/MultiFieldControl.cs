//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MultiFieldControl<T> : MonoBehaviour
{
    private List<InputField> _inputFields;

    [SerializeField]
    private Text _header;

    [SerializeField]
    private Button _addField;

    private Button _removeField;

    [SerializeField]
    private RectTransform _fieldRoot;

    [SerializeField]
    private GameObject _InputFieldPrefab;

    /// <summary>
    /// how many input fields.
    /// </summary>
    public int Count { get { return InputFields.Count; } }

    public Button Adder
    {
        get
        {
            return _addField;
        }

        set
        {
            _addField = value;
        }
    }

    public Button RemoveField
    {
        get
        {
            return _removeField;
        }

        set
        {
            _removeField = value;
        }
    }

    public List<InputField> InputFields
    {
        get
        {
            return _inputFields;
        }

        set
        {
            _inputFields = value;
        }
    }

    /// <summary>
    /// convience accessor for input fields associated with this prefab.
    /// </summary>
    /// <param name="i">
    /// </param>
    /// <returns>
    /// </returns>
    public InputField this[int i]
    {
        get
        {
            return InputFields[i];
        }
    }

    public void RefreshFields()
    {
        InputFields = new List<InputField>(GetComponentsInChildren<InputField>());
    }

    public void BuildControl(T[] parameters)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            AddField();
        }
    }

    public void SetOption(T[] options = null, string header = null)
    {
        if (header != null)
        {
            _header.text = header;
        }
        if (options != null)
        {
            for (int i = 0; i < options.Length; i++)
            {
                InputFields[i].text = options[i].ToString();
            }
        }
    }

    public InputField AddField()
    {
        InputField field = Instantiate(_InputFieldPrefab).GetComponent<InputField>();
        InputFields.Add(field);
        field.transform.SetParent(_fieldRoot);
        SetConstraints(field);
        return field;
    }

    public abstract void SetConstraints(InputField field);

    private void Remove()
    {
        Destroy(transform.GetChild(transform.childCount).gameObject);
        InputFields.RemoveAll(field => field == null);
    }

    private void Awake()
    {
        //Adder.onClick.AddListener(() => UpdateOptions(false));
        //RemoveField.onClick.AddListener(() => UpdateOptions(true));
    }

    /*private void UpdateOptions(bool remove)
    {
        if (!remove && OnFieldAdd != null)
        {
            AddField();
            OnFieldAdd();
        }
        if (remove && OnFieldRemove != null)
        {
            Remove();
            OnFieldRemove();
        }
    }*/
}