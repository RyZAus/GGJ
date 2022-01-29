using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

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
    private Dictionary<string, string> playersDic =
        new Dictionary<string, string>()
        {
            {"Player 1", "0" },
            {"Player 2", "1" }
        };
    private int playerID;
    private int mainColorID;
    private int accentColorID;
    
    [SerializeField] private Text playerText;
    [SerializeField] private Text mainColorText;
    [SerializeField] private Text accentColorText;
    
    private void Awake()
    {
        mainColorID = PlayerPrefs.GetInt("playerPref", 0);
        mainColorID = PlayerPrefs.GetInt("mainBody", 0);
        accentColorID = PlayerPrefs.GetInt("accents", 0);
    }

    private void Start()
    {
        SetItem("player");
        SetItem("mainColor");
        SetItem("accentColor");
    }
    
    public void SelectPlayer(bool isForward)
    {
        if (isForward)
        {
            if (playerID >= playersDic.Count - 1)
            {
                playerID = 0;
            }
            else
            {
                playerID++;
            }
        }
        else
        {
            if (playerID <= 0)
            {
                playerID = playersDic.Count - 1;
            }
            else
            {
                playerID--;
            }
        }

        PlayerPrefs.SetInt("player", playerID);
        SetItem("player");
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
            if (mainColorID <= 0)
            {
                mainColorID = colors.Count - 1;
            }
            else
            {
                mainColorID--;
            }
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
            if (accentColorID <= 0)
            {
                accentColorID = colors.Count - 1;
            }
            else
            {
                accentColorID--;
            }
        }

        PlayerPrefs.SetInt("accentColor", accentColorID);
        SetItem("accentColor");

    }


    private void SetItem(string type)
    {
        switch (type)
        {
            case "player":
                string playerName = playersDic.Keys.ElementAt(playerID);
                playerText.text = playerName.ToLower();

                break;
            
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
