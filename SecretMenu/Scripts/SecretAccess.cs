//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using System.Collections;
using UnityEngine;

public class SecretAccess : MonoBehaviour
{
    private Coroutine _waitForCode;

    [ContextMenu("disable")]
    public void Disable()
    {
        var compoenents = GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < compoenents.Length; i++)
        {
            compoenents[i].gameObject.SetActive(false);
        }
    }

    [ContextMenu("enable")]
    public void Enable()
    {
        var compoenents = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < compoenents.Length; i++)
        {
            compoenents[i].gameObject.SetActive(true);
        }
    }

    // Use this for initialization
    private void Awake()
    {
        Debug.Log("press v for secret menu");
        var compoenents = GetComponentsInChildren<Transform>();
        for (int i = 1; i < compoenents.Length; i++)
        {
            compoenents[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && _waitForCode == null)
        {
            _waitForCode = StartCoroutine(WaitForContinue());
            Debug.Log("now press Left Ctrl and L");
        }
    }

    private IEnumerator WaitForContinue()
    {
        float time = 1000f;
        while ((time -= Time.deltaTime) > 0)
        {
            yield return null;
            if (Input.GetKeyUp(KeyCode.L) && Input.GetKeyUp(KeyCode.RightControl))
            {
                var compoenents = GetComponentsInChildren<Transform>(true);
                for (int i = 0; i < compoenents.Length; i++)
                {
                    compoenents[i].gameObject.SetActive(true);
                }
            }
        }
    }
}