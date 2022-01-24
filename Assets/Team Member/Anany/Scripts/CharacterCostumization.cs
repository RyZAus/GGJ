using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CharacterCostumization : MonoBehaviour
{
    [SerializeField] private Renderer rend;

    private Dictionary<string, string> colors =
        new Dictionary<string, string>()
        {
            {"Red", "FF0000" },
            {"Blue", "0021FF" },
            {"Green", "00FF28" },
            {"Yellow", "FDFF00" },
            {"Pink", "FF009F" }
        };

    private int mainColorID;
    private int accentColorID;

    [SerializeField] private TextMeshProUGUI mainColorText;
    [SerializeField] private TextMeshProUGUI accentColorText;


    private void Awake()
    {
        mainColorID = PlayerPrefs.GetInt("mainBody", 0);
        accentColorID = PlayerPrefs.GetInt("accents", 0);
    }

    private void Start()
    {
        SetItem("mainColor");
        SetItem("accentColor");
    }

    public void SelectMainColor(bool isForward)
    {
        if (isForward)
        {
            if (mainColorID == colors.Count - 1)
            {
                mainColorID = 0;
            }

            else
            {
                mainColorID++;
            }
        }

        else
        {
            mainColorID--;
        }

        PlayerPrefs.SetInt("mainColor", mainColorID);
        SetItem("mainColor");
    }

    public void SelectAccentColor(bool isForward)
    {
        if (isForward)
        {
            if (accentColorID == colors.Count - 1)
            {
                accentColorID = 0;
            }

            else
            {
                accentColorID++;
            }
        }

        else
        {
            accentColorID--;
        }

        PlayerPrefs.SetInt("accentColor", accentColorID);
        SetItem("accentColor");

    }


    private void SetItem(string type)
    {
        switch (type)
        {
            case "mainColor":
                string mainColorName = colors.Keys.ElementAt(mainColorID);
                mainColorText.text = mainColorName.ToLower();
                if (ColorUtility.TryParseHtmlString(colors.Values.ElementAt(mainColorID), out Color mainColor))
                {
                    rend.materials[0].SetColor("_BaseColor", mainColor);
                }

                break;

            case "accentColor":
                string accentColorName = colors.Keys.ElementAt(accentColorID);
                accentColorText.text = accentColorName.ToLower();
                if (ColorUtility.TryParseHtmlString(colors.Values.ElementAt(mainColorID), out Color accentColor))
                {
                    rend.materials[1].SetColor("_BaseColor", accentColor);
                }
                break;
        }
    }
}
