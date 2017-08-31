using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个脚本将会把所有的子物件序列化到场景存储文件中。
/// 并删除他们。
/// 这么做是为了创造那些可以被玩家一次性拾取，并从世界中消失的物件。
/// 你可以先在场景中摆放他们，然后点击 Save And Delete，他们会从场景中消失
/// 第一次读取场景时，会把他们加载进来，但是他们不是作为场景文件出现，而是作为加载对象出现。
/// 因此当玩家拾取他们之后，他们就会消失。下次保存的时候，他们将不会再被记录在初次创建场景文件中，而是出现在世界文件存档中。
/// 
/// 对于玩家创建的物件
/// 只需要将他们保存在场景存档文件中即可。
/// 
/// 
/// </summary>
public class SaveChildrenAndDeleteFromScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
