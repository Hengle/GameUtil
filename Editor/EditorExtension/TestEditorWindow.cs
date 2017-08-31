using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditorWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    Texture2D image;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/ExtEditorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        TestEditorWindow window = (TestEditorWindow)EditorWindow.GetWindow(typeof(TestEditorWindow));
        window.InitTextures();
        window.Show();
    }

    public void InitTextures() 
    {
        this.minSize = new Vector2(800, 600);
        image = Resources.Load("Textures/tex_wood") as Texture2D;         
    }

    public Vector2 operation_menu = Vector2.zero;
    public Vector2 content_menu = new Vector2(100, 0);
    


    void OnGUI()
    {
        operation_menu = GUI.BeginScrollView(new Rect(0,0, 115, 600), operation_menu, new Rect(0, 0, 100, 2000));
        GUI.BeginGroup(new Rect(0, 0, 100, 600));
        GUI.Box(new Rect(0, 0, 120, 600), "Operations");
        GUI.Button(new Rect(0, 30, 100, 20), "Load");
        GUI.Button(new Rect(0, 50, 100, 20), "Save");
        GUI.EndGroup();

        
        GUI.EndScrollView();



        //GUI.Box(new Rect(0, 0, 1000, 800), image);//这种情况下，默认不会拉伸，会居中，也就是等价于GUI.Box(new Rect(100, 0, 800, 800)
    }
}
