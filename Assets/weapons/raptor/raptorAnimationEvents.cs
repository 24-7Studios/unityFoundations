using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raptorAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private raptorScript rp;

    public void onShellInsert()
    {
        rp.shellIn();
    }

    public void onChmaberClick()
    {
        rp.chamberClick();
    }
}
