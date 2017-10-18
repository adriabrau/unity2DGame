using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Collisions2D))]
public class CollisionInspector : Editor
{

    static bool stateFoldout;
    static bool drawDefaultInspector;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        Collisions2D col = (Collisions2D)target;
        GUIStyle booleanText = new GUIStyle();

        EditorGUILayout.Space();
        EditorGUI.indentLevel = 1;

        stateFoldout = EditorGUILayout.Foldout(stateFoldout, "State", true, EditorStyles.toolbarDropDown);

        if(stateFoldout)
        {
            EditorGUILayout.BeginVertical(EditorStyles.textArea);

            EditorGUI.indentLevel = 2;
            if(col.isGrounded) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("Is Grounded", booleanText);
            EditorGUI.indentLevel = 3;
            if(col.wasGroundedLastFrame) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("wasGroundedLastFrame", booleanText);
            if(col.justGotGrounded) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justGotGrounded", booleanText);
            if(col.justNOTGrounded) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justNOTGrounded", booleanText);
            if(col.isFalling) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("isFalling", booleanText);


            EditorGUILayout.Space();

            EditorGUI.indentLevel = 2;
            if(col.isWalled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("Is Walled", booleanText);
            EditorGUI.indentLevel = 3;
            if(col.wasWalledLastFrame) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("wasWalledLastFrame", booleanText);
            if(col.justGotWalled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justGotWalled", booleanText);
            if(col.justNOTWalled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justNOTWalled", booleanText);

            EditorGUILayout.Space();
            EditorGUI.indentLevel = 2;
            if(col.isCelled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("Is Celled", booleanText);
            EditorGUI.indentLevel = 3;
            if(col.wasCelledLastFrame) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("wasCelledLastFrame", booleanText);
            if(col.justGotCelled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justGotCelled", booleanText);
            if(col.justNOTCelled) booleanText.normal.textColor = Color.green;
            else booleanText.normal.textColor = Color.red;
            EditorGUILayout.LabelField("justNOTCelled", booleanText);



            EditorGUILayout.EndVertical();
        }

        //isGrounded
        //col.isGrounded = EditorGUILayout.Toggle("Is Grounded", col.isGrounded, booleanText);

        EditorGUILayout.Space();
        EditorGUI.indentLevel = 0;
        drawDefaultInspector = EditorGUILayout.Foldout(drawDefaultInspector, "Default Inspector", true, EditorStyles.toolbarDropDown);

        if(drawDefaultInspector)
        {
            EditorGUI.indentLevel = 2;
            base.OnInspectorGUI();
        }
    }

}


//serielized field mostrar privates en inspector
//[hideininspector] oculta publicas
//editorGUYlayout todos las opciones