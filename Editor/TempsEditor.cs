using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TempsEditor : EditorWindow
{

    public string _tmpKey = "";

    public Temps temps;
    bool info = false;

    public GUIStyle separador;
    public Vector2 scrollPosition;

    [MenuItem("XidoStudio/Temps")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(TempsEditor));
    }

    private void OnEnable()
    {
        if (temps == null)
        {
            temps = (Temps)AssetDatabase.LoadAssetAtPath("Assets/XidoStudio/Temps/_Temps.prefab", typeof(Temps));
        }
        Repaint();
    }

    private void OnGUI()
    {
        if (temps == null)
        {
            temps = (Temps)AssetDatabase.LoadAssetAtPath("Assets/XidoStudio/Temps/_Temps.prefab", typeof(Temps));
            Repaint();
            return;
        }

        if (separador == null) separador = new GUIStyle(GUI.skin.box) { fixedHeight = 1, stretchWidth = true };


        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);


        temps.frequencia = EditorGUILayout.IntField("Frequencia", temps.frequencia);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("CRONOMETRES");
        EditorGUILayout.BeginHorizontal();
        _tmpKey = EditorGUILayout.TextField("Key", _tmpKey);
        if (GUILayout.Button("Nou Conometre") && _tmpKey != "" && Selection.activeGameObject != null)
        {
            Cronometre _crono = Selection.activeGameObject.AddComponent<Cronometre>();
            _crono.key = _tmpKey;
            _tmpKey = "";
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();




        EditorGUILayout.Separator();
        GUILayout.Box("", separador);
        if (info = EditorGUILayout.Foldout(info, "Informació"))
        {
            EditorGUILayout.TextArea(
                "<b>FUNCIONAMENT:</b>\n" +
                " - Gestiona el temps actual en base a un sistema de ticks.\n" +
                " - Cada segon encen un tick, que es capturat a Temps.tick.\n" +
                " - Captura el tick en el que has començat, el temps que portes jugant, el tick exacte en el que tanques la app, i el temps que portes fora del joc, comparant el tick actual amb l'ultim tick al tancar la app.\n" +
                " - Tot es guarda a Guardat o a PlayerPrefs, segons convingui.\n" +
                " - Conometre:\n" +
                "    - Guarda un temps que es va restant a cada tick i un callback que es cridat quan el temps s'acaba.\n" +
                "    - SEMPRE 'Recuperar' els conometres asignats al awake. Així si hi ha un conometre pendent, el recupera.\n" +
                "    - El KEY es el nom amb el que es guarda el conometre per després poderlho recuperar, es important que tots els conometres que es posin a tot el joc, tinguin un Key diferent." +
                "<b>EDICIO:</b>\n" +
                " - Sistema: El tipus de guardat. Binari, en un bitxer per defecte enomentat /guar.dat. PlayerPrefs, mes facil de crackejar.\n" +
                " - Frequencia: La quantitat de ticks cada quan ha de fer un guardat generic.\n" +
                " - [Afegir]: Agafeix al script de Guardat, els keys necessaris per funcionar.\n\n" +
                "<b>FUNCIONS (Conometre):</b>\n" +
                "   Crear(Temps,Callback) - Quan es crea el Cronometre se l'hi ha de passar la funció que ferà al acabar.\n" +
                "   Recuperar(Callback) - Al Awake recuperar el Conometre, i se l'hi torna a passar el Callback, que no es pot guardar d'una partida per l'altre.",
                new GUIStyle(GUI.skin.label) { richText = true, wordWrap = true });
        }

        EditorGUILayout.EndScrollView();


    }
}
