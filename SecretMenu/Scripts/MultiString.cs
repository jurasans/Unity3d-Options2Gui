//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using UnityEngine.UI;

public class MultiString : MultiFieldControl<string>
{
    public override void SetConstraints(InputField field)
    {
        field.contentType = InputField.ContentType.Standard;
    }
}