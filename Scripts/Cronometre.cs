using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cronometre : MonoBehaviour {

    public delegate void OnFinalitzat();
    private OnFinalitzat onFinalitzar;

    const string PREFIX = "Crono";

    [Header("Info base")]
    public string key;
    public int tempsInicial;
    public int temps;

    [Header("Opcions")]
    public bool acumulatiu;

    [Header("Estats")]
    public bool activat;
    public bool finalitzat = false;

    public bool ticked = false;

    [Header("UI")]
    public CronometreUI ui;

    public void Recuperar(int _tempsInicial, OnFinalitzat _onFinalitzar)
    {
        if (PlayerPrefs.HasKey(PREFIX + key))
        {
            temps = PlayerPrefs.GetInt(PREFIX + key);
            tempsInicial = _tempsInicial;
            activat = true;
        }

        onFinalitzar = _onFinalitzar;
    }

    public void Crear(int _temps, OnFinalitzat _onFinalitzar)
    {
        if (!PlayerPrefs.HasKey(PREFIX + key))
        {
            finalitzat = false;
            PlayerPrefs.SetInt(PREFIX + key, _temps);
            tempsInicial = _temps;
            temps = _temps;
            activat = true;
        }

        onFinalitzar = _onFinalitzar;
    }





    void Start()
    {
        if (!activat)
            return;

        temps -= (int)Temps.tickFora;
        Debug.Log("Cronometre temps = " + temps);
        if (temps <= 0)
        {
            if (!acumulatiu)
            {
                Finalitzar();
            }
            else
            {
                int _acumulat = -temps / tempsInicial;
                for (int i = 0; i < _acumulat; i++)
                {
                    Finalitzar();
                }
            }
        }



    }

    void FixedUpdate()
    {
        if (!activat)
            return;

        if (ticked) ticked = false;

        if (temps > 0)
        {
            if (Temps.tick)
            {
                if (!ticked)
                {
                    temps--;
                    ticked = true;
                    if (ui)
                    {
                        ui.Escriure(temps);
                    }
                }
            }
        }
        else
        {
            if (!finalitzat)
            {
                Finalitzar();
            }
        }

    }


    void Finalitzar()
    {
        activat = false;
        finalitzat = true;
        Debug.Log("Finalitzar");
        PlayerPrefs.DeleteKey(PREFIX + key);
        onFinalitzar.Invoke();
    }

    public void Cancelar()
    {
        activat = false;
        Debug.Log("Cancelat");
        PlayerPrefs.DeleteKey(PREFIX + key);
    }

    private void OnApplicationQuit()
    {
        if (finalitzat)
            return;

        Debug.Log("Guardar cronometre");
        PlayerPrefs.SetInt(PREFIX + key, temps);
    }
}
