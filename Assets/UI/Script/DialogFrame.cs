using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogFrame : UIPanel
{
    // Start is called before the first frame update
    public Text characterName;

    public Image characterFace;

    public Text content;

    void Awake()
    {
        canCover=true;
        level=2;
    }
}
