using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XSuitFunctionStatus : MonoBehaviour {
    [SerializeField]
    GameObject FUNCTION_STATUS_TEXT;
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        more_string = "(๑•̀ㅂ•́)و✧";
    }
    private string more_string;
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        switch (current_function_status)
        {
            case XEnumFunctionStatus.NOTHING:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "w(ﾟДﾟ)w" + " NO FUNCTION YET";
                    break;
                }
            case XEnumFunctionStatus.MOVE:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = more_string + "MOVE FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.ROTATE:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = more_string + "ROTATE FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.SCALE:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = more_string + "SCALE FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.CREATE:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "ヽ(✿ﾟ▽ﾟ)ノ" + "CREATE FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.DELETE:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "ヽ(✿ﾟ▽ﾟ)ノ" + "DELETE FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.COPY:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "ヽ(✿ﾟ▽ﾟ)ノ" + "COPY FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.EXPORT:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "Σ( ° △ °|||)︴" + "EXPORT FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.GROUP:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = more_string + "GROUP FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.ARRAY:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "╰(*°▽°*)╯" + "ARRAY FUNCTION";
                    break;
                }
            case XEnumFunctionStatus.MATERIAL2D:
                {
                    FUNCTION_STATUS_TEXT.GetComponent<Text>().text
                        = "（○｀ 3′○）" + "ARRAY FUNCTION";
                    break;
                }

        }
	}
}
