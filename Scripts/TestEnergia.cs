using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnergia : MonoBehaviour {

    public int energia = 100;
    public int forca = 0;
    public Cronometre cronometreForca;
    public int destresa = 0;
    public Cronometre cronometreDestresa;



    public Text text;


    private void Awake()
    {
        energia -=  (int)Temps.tickFora;
        cronometreForca.Recuperar(90, AugmentarForca);
        cronometreDestresa.Recuperar(30,AugmentarDestresa);
    }



    void FixedUpdate () {
        if (Temps.tick)
        {
            energia--;

            string _text = "Data actual: " + Temps.dataActual + "\n" +
            "Data guardada: " + Temps.dataIniciJoc + "\n" +
            "Data desde l'inici del joc: " + (Temps.dataActual - Temps.dataIniciJoc).ToString() + "\n" +
            "Data desde l'inici de la partida: " + (Temps.dataActual - Temps.dataIniciPartida).ToString() + "\n" +
            "Temps transcorregut amb el mobil apagat: " + (Temps.dataIniciPartida - Temps.dataFinalPartidaAnterior).ToString();

            if (Temps.tick) _text += "\n Un Tick!";

            text.text = _text;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            cronometreForca.Crear(90, AugmentarForca);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            cronometreDestresa.Crear(30, AugmentarDestresa);
        }



        

    }

    public void AugmentarForca()
    {
        forca++;

    }
    public void AugmentarDestresa()
    {
        destresa++;
    }
}
