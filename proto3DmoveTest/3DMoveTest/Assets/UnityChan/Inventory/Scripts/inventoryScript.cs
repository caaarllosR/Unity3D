using UnityEngine;
using System.Collections;

public class inventoryScript : MonoBehaviour
{

    private Vector2 windowPosition;
    private Vector2 windowSize;
    private Vector2 gridPosition;
    private Vector2 gridSize;

    private Rect useButtonRect;
    private Rect lanternRect;

    private int gridLineValue;

    public int gridIndexValue;
    public int maxIndexGrids;
    public int maxColumnGrid;

    public float timeJoyVertical;
    public float timeJoyHorizontal;
    private float delayButton;

    private bool spawned;
    public bool gridDescOnOff;
    public bool changeGridY;
    public bool changeGridX;
    public bool inventoryOn;

    public string[] gridsDesc;

    private GUIStyle style;
    private GUIStyle styleLife;

    public GUIContent useItemButton;
    private GUIContent lanternGrid;
    public GUIContent[] grids;

    public Texture invetoryWindow;

    private uChanController uChanControl;
    private inventoryAddItem addingNewItem;
    private itemsScript items;


    // Use this for initialization
    void Start()
    {
        spawned = false;

        gridIndexValue = -1;
        gridLineValue = 5;
        maxIndexGrids = grids.Length - 1;
        maxColumnGrid = (grids.Length / 5) - 1;
        style = new GUIStyle();
        style.normal.textColor = Color.white;

        lanternGrid = new GUIContent();

        styleLife = new GUIStyle();
        styleLife.font = (Font)Resources.Load("Fonts/BIONIC");
        styleLife.normal.textColor = Color.green;
        styleLife.alignment = TextAnchor.LowerRight;
        windowPosition = new Vector2(0, 0);

        addingNewItem = GetComponent<inventoryAddItem>();
        items = GetComponent<itemsScript>();
        uChanControl = GetComponent<uChanController>();

    }


    // Update is called once per frame
    void Update()
    {
        windowSize = new Vector2(Screen.width, 5000);
        useButtonRect = new Rect(Screen.width / 1.18f, Screen.height / 1.35f, Screen.width / 8f, Screen.height / 8f);
        lanternRect = new Rect(Screen.width / 14.5f, Screen.height / 3f, Screen.width / 6.3f, Screen.height / 4.3f); 
        gridPosition = new Vector2(Screen.width / 1.725f, Screen.height / 12f);
        gridSize = new Vector2(Screen.width / 2.50f, Screen.height / 1.6f);

        if (Input.GetButtonDown("Select"))
        {
            if (inventoryOn)
            {
                inventoryOn = false;
                gridIndexValue = -1;
            }
            else if (!inventoryOn)
            {
                inventoryOn = true;
                changeGridX = false;
                changeGridY = false;
                gridIndexValue = 0;
            }
        }
        selectGrid();
    }


    void selectGrid()
    {
        if (inventoryOn)
        {
            if (Input.GetAxisRaw("Vertical") <= -0.5f)
            {
                changeGridX = false;
                timeJoyHorizontal = 0;

                if (!changeGridY)
                {
                    if (gridIndexValue / 5 < maxColumnGrid)
                    {
                        gridIndexValue += 5;
                        changeGridY = true;
                    }
                    else
                    {
                        gridIndexValue -= maxIndexGrids - 4;
                        changeGridY = true;
                    }
                }
                else
                {
                    timeJoyVertical += Time.deltaTime;
                }

                if (timeJoyVertical >= 0.2f && changeGridY)
                {
                    changeGridY = false;
                    timeJoyVertical = 0;
                }
            }
            else if (Input.GetAxisRaw("Vertical") >= 0.5f)
            {
                changeGridX = false;
                timeJoyHorizontal = 0;

                if (!changeGridY)
                {
                    if (gridIndexValue / 5 == 0)
                    {
                        gridIndexValue += maxIndexGrids - 4;
                        changeGridY = true;
                    }
                    else if (gridIndexValue / 5 <= maxColumnGrid)
                    {
                        gridIndexValue -= 5;
                        changeGridY = true;
                    }
                }
                else
                {
                    timeJoyVertical += Time.deltaTime;
                }

                if (timeJoyVertical >= 0.2f && changeGridY)
                {
                    changeGridY = false;
                    timeJoyVertical = 0;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") >= 0.5f)
            {
                changeGridY = false;
                timeJoyVertical = 0;

                if (!changeGridX)
                {
                    if (gridIndexValue < maxIndexGrids)
                    {
                        gridIndexValue += 1;
                        changeGridX = true;
                    }
                    else
                    {
                        gridIndexValue = 0;
                        changeGridX = true;
                    }
                }
                else
                {
                    timeJoyHorizontal += Time.deltaTime;
                }

                if (timeJoyHorizontal >= 0.2f && changeGridX)
                {
                    changeGridX = false;
                    timeJoyHorizontal = 0;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") <= -0.5f)
            {
                changeGridY = false;
                timeJoyVertical = 0;

                if (!changeGridX)
                {
                    if (gridIndexValue == 0)
                    {
                        gridIndexValue = maxIndexGrids;
                        changeGridX = true;
                    }
                    else if (gridIndexValue <= maxIndexGrids)
                    {
                        gridIndexValue -= 1;
                        changeGridX = true;
                    }
                }
                else
                {
                    timeJoyHorizontal += Time.deltaTime;
                }

                if (timeJoyHorizontal >= 0.2f && changeGridX)
                {
                    changeGridX = false;
                    timeJoyHorizontal = 0;
                }
            }
            if (Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
            {
                changeGridY = false;
                changeGridX = false;
                timeJoyVertical = 0;
                timeJoyHorizontal = 0;
            }
        }
    }


    private void resetButton()
    {
        if (spawned)
        {
            delayButton -= Time.deltaTime;

            if (Input.GetButton("Submit"))
            {
                spawned = true;
            }
            else if (delayButton < 0)
            {
                delayButton = 0;
                spawned = false;
            }
        }
    }


    void OnGUI()
    {
        if (inventoryOn)
        {
            GUI.BeginGroup(new Rect(windowPosition.x, windowPosition.y, windowSize.x, windowSize.y), invetoryWindow);
            GUI.EndGroup();
            gridIndexValue = GUI.SelectionGrid(new Rect(gridPosition.x, gridPosition.y, gridSize.x, gridSize.y), gridIndexValue, grids, gridLineValue);
            styleLife.fontSize = (int)(Screen.width / 35.05f);
            GUI.Label(new Rect(Screen.width / 2.51f, Screen.height / 4.3f, styleLife.fontSize, styleLife.fontSize), uChanControl.Life.ToString(), styleLife);
            GUI.Label(new Rect(Screen.width / 2f, Screen.height / 4.3f, styleLife.fontSize, styleLife.fontSize), uChanControl.LifeMax.ToString() , styleLife);
            GUI.Label(new Rect(Screen.width / 2.075f, Screen.height / 53f, styleLife.fontSize, styleLife.fontSize), uChanControl.Hunger.ToString(), styleLife);
            GUI.Label(new Rect(Screen.width / 2.075f, Screen.height / 7.5f, styleLife.fontSize, styleLife.fontSize), uChanControl.Thirst.ToString(), styleLife);

            if (uChanControl.LanternCatch > 0)
            {
                lanternGrid.image = items.lanternTexture;
            }
     
            GUI.Button(new Rect(lanternRect.x, lanternRect.y, lanternRect.width, lanternRect.height), lanternGrid);

            if (gridIndexValue >= 0 && !gridDescOnOff)
            {
                gridDescOnOff = true;
            }
            if (gridIndexValue != -1)
            {
                if (grids[gridIndexValue].image.name != "gridIcon")
                {
                    style.fontSize = (int)(Screen.width / 30.05f);
                    GUI.Label(new Rect(Screen.width / 30.05f, Screen.height / 1.35f, 200, 40), "Nome: " + grids[gridIndexValue].tooltip + "   qtd: " + grids[gridIndexValue].text, style);
                    GUI.Label(new Rect(Screen.width / 30.05f, Screen.height / 1.15f, 100, 100), gridsDesc[gridIndexValue], style);

                    if (GUI.Button(new Rect(useButtonRect.x, useButtonRect.y, useButtonRect.width, useButtonRect.height), useItemButton) || (Input.GetButton("Submit") && !spawned))
                    {
                        delayButton = 0.2f; //controla delay por segundo emq ue se pode apertar botão
                        spawned = true; //Enquanto for verdadeiro botão não volta a funcionar
                        addingNewItem.gridIndexValue = gridIndexValue;
                        addingNewItem.itemGridName = grids[gridIndexValue].tooltip;
                        items.useitem(addingNewItem.itemGridName);
                    }
                }
            }
            resetButton(); //permite apertar o botão novamente
        }
    }
}
