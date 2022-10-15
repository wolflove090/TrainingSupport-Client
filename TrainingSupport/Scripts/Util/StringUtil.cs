using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil
{
    static public string GetCommaSeparation(int num)
    {
        return string.Format("{0:#,0}", num);
    }

}
