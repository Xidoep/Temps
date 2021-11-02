using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Temps : MonoBehaviour {

    public const string KEY_DATA = "T_Data";
    public const string KEY_FINAL_PARTIDA_ANTERIOR = "T_FinalPartidaAnterior";

    public static long dataActual;
    public static long dataIniciJoc;
    public static long dataIniciPartida;
    public static long dataFinalPartidaAnterior;

    public static bool tick = false;
    public static long tickInici;
    public static long tickPartida;
    public static long tickFora;

    public static int ticksGuardatUpdate;

    public int frequencia = 10;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (!PlayerPrefs.HasKey(KEY_DATA))
        {
            dataIniciJoc = GuardarData(KEY_DATA);
        }
        else
        {
            dataIniciJoc = CarregarData(KEY_DATA);
        }
        if (PlayerPrefs.HasKey(KEY_FINAL_PARTIDA_ANTERIOR)) dataFinalPartidaAnterior = CarregarData(KEY_FINAL_PARTIDA_ANTERIOR);


        dataIniciPartida = DateTime.Now.Ticks / 10000000;
        dataActual = DateTime.Now.Ticks / 10000000;

        SetTicks();
    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteKey(KEY_DATA);
            PlayerPrefs.DeleteKey(KEY_FINAL_PARTIDA_ANTERIOR);
        }



        //Core
        if (tick)
        {
            StartCoroutine(ApagaTickFinalFrame());
        }

        dataActual = GetDataActual();
        SetTicks();


    }

    IEnumerator ApagaTickFinalFrame()
    {
        yield return new WaitForEndOfFrame();
        tick = false;
        ticksGuardatUpdate++;
        if (ticksGuardatUpdate > frequencia)
        {
            ticksGuardatUpdate = 0;
            GuardarData(KEY_FINAL_PARTIDA_ANTERIOR);
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Apago el joc");
        GuardarData(KEY_FINAL_PARTIDA_ANTERIOR);
    }












    void SetTicks()
    {
        tickInici = dataActual - dataIniciJoc;
        tickPartida = dataActual - dataIniciPartida;
        tickFora = dataIniciPartida - dataFinalPartidaAnterior;
    }


    long GetDataActual()
    {
        long _dataActual = DateTime.Now.Ticks / 10000000;
        if (dataActual != _dataActual) {
            tick = true;
        }

        return DateTime.Now.Ticks / 10000000;
    }
    long GuardarData(string _key)
    {
        long _data = DateTime.Now.Ticks / 10000000;
        PlayerPrefs.SetString(_key, _data.ToString());
        Debug.Log("Guaradat: " + _data.ToString());
        return _data;
    }
    long CarregarData(string _key)
    {
        long _dataLong = 0;
        string _data = "";
        _data = PlayerPrefs.GetString(_key);

        if (long.TryParse(_data, out _dataLong))
        {
            return _dataLong;
        }
        else
        {
            return DateTime.Now.Ticks / 10000000;
        }
    }

}
