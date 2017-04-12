//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using Com.Visualed.Infra.Storage;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// this is a universal compoenent. it will map any public property in a class into a UI using the
/// <see cref="_contentSlot"/> parameter reference. you can swap out the prefabs for each ui eleement
/// used to represent the property but please stay inline. toggle for bool input for string , int
/// </summary>
/// <typeparam name="T">
/// the class to map its properties too
/// </typeparam>
public abstract class OptionMapper<T> : MonoBehaviour where T : class, new()
{
    /// <summary>
    /// to enable the properties to be edited also in the editor. not a must.
    /// </summary>
    [SerializeField]
    public T _options;

    [SerializeField]
    private Transform _contentSlot;

    [Header("UI OPTIONS")]
    [SerializeField]
    private GameObject _bool;

    [SerializeField]
    private GameObject _int, _intArr, _string, _stringarr, _stringBrowser, _enumPicker;

    public IOpSaver Saver { get; set; }

    public void Awake()
    {
        Load();
    }

    /// <summary>
    /// override this if you want to use persistency use base first.
    /// </summary>
    public abstract void ReadOptionsFromFile();

    public void OnEnable()
    {
        MapOptionsToGui();
    }

    // Use this for initialization

    public void Save()
    {
        try
        {
            Saver.Save(_options);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "while trying to save ");
        }
    }

    public void Load()
    {
        try
        {
            _options = Saver.InitFromFile<T>();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "while trying to save ");
            if (_options == null)
            {
                _options = new T();
            }
        }
    }

    /// <summary>
    /// algorithm to find all the settings and write them.
    /// </summary>
    private void MapOptionsToGui()
    {
        var properties = _options.GetType().GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            GameObject uiPrefab = null;
            var type = properties[i].PropertyType;
            Debug.Log("mapping:" + type.ToString() + "::" + properties[i].Name);

            //we only support these types, since only these type have ui prefabs.
            if (type == typeof(bool))
            {
                uiPrefab = Instantiate(_bool);
                AddFunctionToToggle(uiPrefab.GetComponent<Toggle>(), properties[i]);
            }
            if (type == typeof(string[]))
            {/*
                uiPrefab = Instantiate(_stringarr);
                         var multistring = uiPrefab.GetComponent<MultiString>();
                          //get current Values
                          string[] options = properties[i].GetGetMethod().Invoke(_options, null) as string[];
                          //set them
                          multistring.SetOption(options, properties[i].Name);

                           multistring.InputFields.ForEach((field)=>field.onEndEdit.AddListener(OnEditEndChangeValue(properties[i], field)));
             */
                Debug.LogError("string array not supported");
            }
            if (type == typeof(string))
            {
                uiPrefab = Instantiate(_string);
                var stringer = uiPrefab.GetComponent<StringEntry>();
                //get currentValue
                string option = properties[i].GetGetMethod().Invoke(_options, null) as string;
                //set it
                stringer.SetOption(option, properties[i].Name);
                //enable its changing through reflection
                stringer.InputField.onEndEdit.AddListener(OnEditEndChangeValue(properties[i], stringer));
            }
            if (type == typeof(string) && Attribute.IsDefined(properties[i], typeof(EnableBrowseFileAttribute)))
            {
                //uiPrefab = Instantiate(_stringBrowser);
                //TODO: this
            }
            if (type == typeof(int[]) || type == typeof(float[]))
            {
                Debug.LogError("float or int array not supported");
            }
            if (type == typeof(int) || type == typeof(float))
            {
                uiPrefab = Instantiate(_int);
                var inter = uiPrefab.GetComponent<IntEntry>();
                //get currentValue
                int? option = properties[i].GetGetMethod().Invoke(_options, null) as int?;
                //set it
                inter.SetOption((int)option, properties[i].Name);
                //enable its changing through reflection
                inter.InputField.onEndEdit.AddListener(OnEditEndChangeValue(properties[i], inter));
            }
            if (type == typeof(Enum))
            {
            }
            //set the prefab to place.
            if (uiPrefab != null)
            {
                uiPrefab.transform.SetParent(_contentSlot, false);
            }
        }
    }

    private UnityAction<string> OnEditEndChangeValue(PropertyInfo propertyInfo, StringEntry stringer)
    {
        return (inputVal) =>
        {
            propertyInfo.GetSetMethod().Invoke(_options, new object[]
            {
                    inputVal
            });
        };
    }

    private UnityAction<string> OnEditEndChangeValue(PropertyInfo propertyInfo, IntEntry inter)
    {
        return (inputVal) =>
        {
            int intVal;
            if (int.TryParse(inputVal, out intVal))
            {
                propertyInfo.GetSetMethod().Invoke(_options, new object[]
                {
                    intVal
                });
            }
            else
            {
                propertyInfo.GetSetMethod().Invoke(_options, new object[]
                {
                    inputVal
                });
            }
        };
    }

    /// <summary>
    /// adds a specific function to toggling ui element event
    /// </summary>
    /// <param name="toggle">
    /// </param>
    /// <param name="info">
    /// </param>
    private void AddFunctionToToggle(Toggle toggle, PropertyInfo info)
    {
        toggle.isOn = (bool)info.GetGetMethod().Invoke(_options, null);
        toggle.onValueChanged.AddListener((val) => { info.GetSetMethod().Invoke(_options, new object[] { val }); });
        toggle.GetComponentInChildren<Text>().text = info.Name;
    }
}