using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class inventoryAddItem : MonoBehaviour
{

    public inventoryScript theInventory;
    public itemsScript items;
    private uChanController uChanControl;

    public Texture blankIcon;
    public Texture theNewItem;

    public int gridIndexValue;
    public int indexNewItem;
    public int splitID;

    public string[] splitItemImageID;
    public string[] splitItemName;
    public string[] splitItemAmount;
    public string[] splitItemDesc;

    public string itemGridName;
    public string path;
    public string itemImageID;
    public string itemName;
    public string itemAmount;
    public string itemDesc;
    public int saveLife;
    public int saveHunger;
    public int saveThirst;
    public int saveLantern;

    // Use this for initialization
    void Start()
    {
        path = "GUI_Icons/gridIcon.png";
        theInventory = GetComponent<inventoryScript>();
        items = GetComponent<itemsScript>();
        uChanControl = GetComponent<uChanController>();

        gridIndexValue = 0;
        indexNewItem = -1;

        loadInventory();
    }


    public void saveInventory() //funcao utilizada ao trocar de cena para salvar dados do inventario
    {
        saveLife = uChanControl.Life;
        saveHunger = uChanControl.Hunger;
        saveThirst = uChanControl.Thirst;
        saveLantern = uChanControl.LanternCatch;

        for (int i = 0; i < theInventory.grids.Length; i++)
        {
            if (theInventory.grids[i].image.name != "gridIcon")
            {
                if (items.itemPlayerAmount[int.Parse(theInventory.grids[i].image.name)] > 0)
                {
                    itemImageID += theInventory.grids[i].image.name + "-";
                    itemName += theInventory.grids[i].tooltip + "-";
                    itemAmount += theInventory.grids[i].text + "-";
                    itemDesc += theInventory.gridsDesc[i] + "-";
                }
            }
        }
        PlayerPrefs.SetString("itemImageID", itemImageID);
        PlayerPrefs.SetString("itemName", itemName);
        PlayerPrefs.SetString("itemAmount", itemAmount);
        PlayerPrefs.SetString("itemDesc", itemDesc);
        PlayerPrefs.SetInt("saveLife", saveLife);
        PlayerPrefs.SetInt("saveHunger", saveHunger);
        PlayerPrefs.SetInt("saveThirst", saveThirst);
        PlayerPrefs.SetInt("saveLantern", saveLantern);
    }


    public void loadInventory() //funcao utilizada no carregamento do jogo ou de nova cena para recuperar dados salvos no inventario
    {
        itemImageID = PlayerPrefs.GetString("itemImageID");

        if (itemImageID != "")
        {
            itemName = PlayerPrefs.GetString("itemName");
            itemAmount = PlayerPrefs.GetString("itemAmount");
            itemDesc = PlayerPrefs.GetString("itemDesc");

            uChanControl.Life = PlayerPrefs.GetInt("saveLife");
            uChanControl.Hunger = PlayerPrefs.GetInt("saveHunger");
            uChanControl.Thirst = PlayerPrefs.GetInt("saveThirst");
            uChanControl.LanternCatch = PlayerPrefs.GetInt("saveLantern");
            splitItemImageID = itemImageID.Split('-');
            splitItemName = itemName.Split('-');
            splitItemAmount = itemAmount.Split('-');
            splitItemDesc = itemDesc.Split('-');

            for (int i = 0; i < splitItemImageID.Length; i++)
            {
                if (splitItemImageID[i] != "")
                {
                    splitID = int.Parse(splitItemImageID[i]);
                    theInventory.grids[i].image = (Texture)items.itemTexture[splitID];
                    theInventory.grids[i].tooltip = splitItemName[i];
                    theInventory.grids[i].text = splitItemAmount[i];
                    theInventory.gridsDesc[i] = splitItemDesc[i];
                }
            }
            PlayerPrefs.DeleteAll();
        }
    }


    public void newItem()
    {
        if (indexNewItem > -1)
        {
            if (gridIndexValue < theInventory.grids.Length)
            {
                theNewItem = items.itemTexture[indexNewItem];

                if (theInventory.grids[gridIndexValue].image.name == theNewItem.name)
                {
                    theInventory.grids[gridIndexValue].text = items.itemPlayerAmount[indexNewItem].ToString();
                    gridIndexValue = 0;
                    indexNewItem = -1;
                }
                else if (theInventory.grids[gridIndexValue].image.name == blankIcon.name)
                {
                    theInventory.grids[gridIndexValue].image = theNewItem;
                    theInventory.grids[gridIndexValue].text = items.itemPlayerAmount[indexNewItem].ToString();
                    theInventory.grids[gridIndexValue].tooltip = items.itemsName[indexNewItem];
                    theInventory.gridsDesc[gridIndexValue] = items.itemDesc[indexNewItem];
                    gridIndexValue = 0;
                    indexNewItem = -1;
                }
                else if (theInventory.grids[gridIndexValue].image.name != blankIcon.name)
                {
                    gridIndexValue += 1;
                }
            }
        }
    }

    public static Texture2D LoadImg(string filePath)
    {
        Texture2D texture = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); 
        }
        return texture;
    }
}