using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cronometre))]
public class CronometreInspector : Editor {

    GUIStyle label;

    public override void OnInspectorGUI()
    {
        label = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };


        Cronometre cronometre = (Cronometre)target;

        cronometre.key = EditorGUILayout.TextField("Key", cronometre.key);
        if (cronometre.temps > 0)
        {
            cronometre.temps = EditorGUILayout.IntField("Temps", cronometre.temps);
        }

        if (cronometre.finalitzat)
        {
            EditorGUILayout.LabelField("Finalitzat",label);
        }
        else
        {
            if (cronometre.activat)
            {
                EditorGUILayout.LabelField("Activat", label);
            }
        }
        cronometre.ui = (CronometreUI)EditorGUILayout.ObjectField("UI", cronometre.ui, typeof(CronometreUI), true);
        cronometre.acumulatiu = EditorGUILayout.Toggle("Acumulatiu", cronometre.acumulatiu);
    }
}
