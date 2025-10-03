using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public enum GenericControls
{
    None,
    North,
    East,
    South,
    West, 
    Right,
    Left,
    Up,
    Down,
    Forward, 
    Backward,
    Grab
}


public struct ControlSet
{
    // Attacking
    public KeyCode North;
    public KeyCode East;
    public KeyCode West;
    public KeyCode South;
    public KeyCode Grab;

    // Movement
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;

}

public class ControlSettings : MonoBehaviour
{
    public static ControlSet Player_1_Keybinds = new ControlSet();
    public static ControlSet Player_2_Keybinds = new ControlSet();

    string configFile;
    string defaultData = "[Player_1_Keybinds]" +
        "\nNorth = 5 " +
        "\nSouth = T " +
        "\nEast = R " +
        "\nWest = Y " +
        "\nGrab = E " +
        "\n\nUp = W " +
        "\nDown = S " +
        "\nLeft = A " +
        "\nRight = D " +
        "\n\n[Player_2_Keybinds]" +
        "\nNorth = I " +
        "\nSouth = K " +
        "\nEast = L " +
        "\nWest = J " +
        "\nGrab = U " +
        "\n\nUp = UpArrow " +
        "\nDown = DownArrow " +
        "\nLeft = LeftArrow " +
        "\nRight = RightArrow ";

    // Start is called before the first frame update
    void Awake()
    {
        configFile = Application.persistentDataPath + "/Config.ini";
        GenerateConfig();

        Player_1_Keybinds = InitializeControlSet(ConfigParser.GetSectionData(configFile, nameof(Player_1_Keybinds)));
        Player_2_Keybinds = InitializeControlSet(ConfigParser.GetSectionData(configFile, nameof(Player_2_Keybinds)));

        Debug.Log(Player_1_Keybinds.Left + " " + Player_2_Keybinds.Left.ToString());

        Destroy(gameObject);

    }

    void GenerateConfig()
    {
        if (!File.Exists(configFile))
        {
            FileStream stream = new FileStream(configFile, FileMode.Create, FileAccess.ReadWrite);
            byte[] buffer = Encoding.ASCII.GetBytes(defaultData);
            stream.Write(buffer, 0, buffer.Length);

            stream.Flush();
            stream.Close();
        }
    }

    ControlSet InitializeControlSet(HashSet<KeyValuePair<string, string>> keyBindings)
    {
        ControlSet set = new ControlSet();

        foreach (KeyValuePair<string, string> keyBinding in keyBindings)
        {
            KeyCode parsedValue;

            if(Enum.TryParse(keyBinding.Value, out parsedValue))
            {
                switch (keyBinding.Key)
                {
                    case "North" :
                        set.North = parsedValue;
                        break;
                    case "South":
                        set.South = parsedValue;
                        break;
                    case "East":
                        set.East = parsedValue;
                        break;
                    case "West":
                        set.West = parsedValue;
                        break;
                    case "Grab":
                        set.Grab = parsedValue;
                        break;
                    case "Up":
                        set.Up = parsedValue;
                        break;
                    case "Down":
                        set.Down = parsedValue;
                        break;
                    case "Left":
                        set.Left = parsedValue;
                        break;
                    case "Right":
                        set.Right = parsedValue;
                        break;
                }
            }
        }
        return set;
    }

}
