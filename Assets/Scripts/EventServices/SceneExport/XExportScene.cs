using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading;
using System;
using System.IO;

public class XExportScene : MonoBehaviour {
    [SerializeField]
    GameObject XExportSceneCanvas;

    private string main_path;
    private void Export()
    {
        UnityEngine.Object[] selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        List<string> list = new List<string>();
        for (int i = 0; i < selectedAsset.Length; i++)
        {
            list.Add(AssetDatabase.GetAssetPath(selectedAsset[i]));
        }
        ExportPackageOptions op = ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse;
        AssetDatabase.ExportPackage(list.ToArray(), output_path + "\\VRworkshopX.unitypackage", op);
    }
    private bool is_directory_okay;
    private string output_path;
    // Use this for initialization
    void Start () {
		main_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string folder_name = "VRworkshopX";
        string folder_path = main_path + "\\" + folder_name;
        if (!Directory.Exists(folder_path))
        {
            try
            {
                Directory.CreateDirectory(folder_path);
            }
            catch (Exception)
            {
                is_directory_okay = false;
            }
        }
        is_directory_okay = true;
        output_path = folder_path;
    }
	
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.EXPORT)
        {
            XExportSceneCanvas.SetActive(false);
            return;
        }
        else
        {
            XExportSceneCanvas.SetActive(true);
        }
        //render canvas
        if (!is_directory_okay)
        {
            //render

            return;
        }
        //htc event handler
        if (RightControllerDriver.getInstance() != null)
        {
            if (RightControllerDriver.getInstance().GripDown())
            {
                //export
                Export();
            }
        }
	}
}
