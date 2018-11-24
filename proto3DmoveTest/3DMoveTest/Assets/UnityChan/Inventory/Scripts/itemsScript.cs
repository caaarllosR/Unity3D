using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemsScript : MonoBehaviour
{
    public string[] itemsName;
    public string[] itemDesc;
    public int[] itemPlayerAmount;
    private int i;
    private string path;
    public Texture lanternTexture;
    public Texture[] itemTexture;
    public Dictionary<string, int> itemList = new Dictionary<string, int>();
    public uChanController uChanControl;

    // Use this for initialization
    void Start()
    {
        itemList.Add("Cura", 0);
        path = "Item_Icons/";
        i = 0;

        itemsName = new string[] {"Cura", "bla", "blabla", "blablabla" };
        itemDesc = new string[] {"Recupera vida.", "blablablabla", "blablablabla", "blablablabla" };
        itemPlayerAmount = new int[] { 0, 0, 0, 0 };
        itemTexture = new Texture[itemsName.Length];

        uChanControl = GetComponent<uChanController>();

        foreach (string itemName in itemsName)
        {
            itemTexture[i] = Resources.Load<Texture>(path.Insert(path.Length, itemName));
            i++;
        }

        lanternTexture = Resources.Load<Texture>("Lantern_Icon/lantern");
    }

    private void cure(int cure) //Metodo de itens de cura, (nome do item que vai curar, quantidade de vida a ser curada
    {
        if (uChanControl.Life <= (uChanControl.LifeMax - cure))
        {
            uChanControl.Life += cure;
        }
        else
        {
            uChanControl.Life = uChanControl.LifeMax;
        }
    }


    public void useitem(string itemName) //Metodo que verifica item que esta sendo usado e qual a sua utilidade
    {
        if (itemName == "Cura")
        {
            cure(2);
        }
    }

}
