using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CronometreUI : MonoBehaviour {

    public Text text;
    public List<string> s = new List<string>();

	public void Escriure (int _temps) {

        s.Clear();

        if (_temps > 0)
        {
            if (_temps > 86400) s.Add((_temps / 86400).ToString() + "d ");
            if (_temps > 3600) s.Add(((_temps / 3600) % 24).ToString() + ".");
            s.Add(((_temps / 60) % 60).ToString("00") + ":");
            s.Add((_temps % 60).ToString("00"));
        }
        else
        {
            s.Add("Done!");
        }


        StringBuilder _builder = new StringBuilder();

        for (int i = 0; i < s.Count; i++)
        {
            _builder.Append(s[i]);
        }

        text.text = _builder.ToString();
        //text.text = ((_temps / 60) % 60).ToString("00") + ":" + (_temps % 60).ToString("00");
    }
}
