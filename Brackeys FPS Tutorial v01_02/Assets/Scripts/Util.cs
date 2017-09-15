using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Util
{
    public static void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        if (_obj == null)
            return;
        _obj.layer = _newLayer;

        foreach  (Transform _child in _obj.transform)
        {
            if (_child == null)
                continue;
            SetLayerRecursively(_child.gameObject, _newLayer);
        }  

     }

    

    // DBG: prints hideflag on all objects in game
    //public static void DumpAttributestoDebug()
    //{
    //    // get root objects in scene
    //    List<GameObject> rootObjects = new List<GameObject>();
    //    Scene scene = SceneManager.GetActiveScene();
    //    scene.GetRootGameObjects(rootObjects);

    //    // iterate root objects and do something
    //    for (int i = 0; i < rootObjects.Count; ++i)
    //    {
    //        GameObject gameObject = rootObjects[i];

    //        Debug.Log("Object Name " + rootObjects[i].name + " and its Hideflags are set to :" + rootObjects[i].hideFlags.ToString());

    //        foreach (Transform TFrm in rootObjects[i].transform)
    //        {

    //            Transform _rootTransform = TFrm.transform;
    //            //Debug.Log("Object Name " + TFrm.name + " and its Hideflags are set to :" + TFrm.hideFlags.ToString());
    //            SearchTransformRecursively(_rootTransform);
    //            if (TFrm == null)
    //                continue;
    //        }
    //    }
    //}
    
    //// Prints HideFlags for All Childrem sought recursively:
    //public static void SearchTransformRecursively(Transform _Tfrm)
    //{
    //    if (_Tfrm == null)
    //        return;

    //    Debug.Log("Object Name " + _Tfrm.name + " and its Hideflags are set to :" + _Tfrm.hideFlags.ToString());

    //    foreach (Transform _child in _Tfrm.transform)
    //    {
    //        if (_child == null)
    //            continue;

    //        //SetLayerRecursively(_child.gameObject, _newLayer);
    //        SearchTransformRecursively(_child.gameObject.transform);
    //    }


    //}

}
