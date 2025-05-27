using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.RestService;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


public class StoreData : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField inputName;         // InputField para el nombre del jugador
    public Text txtMoney;               // Text para mostrar el dinero del jugador

    public GameObject btnSkin00Use;
    public GameObject btnSkin01Use;
    public GameObject btnSkin02Use;

    public GameObject btnSkin01Buy;
    public GameObject btnSkin02Buy;
    // Start is called before the first frame update
    private void Awake()
    {
        //Intentar cargar los datos del jugador
        if (File.Exists(_path))
        {
            LoadData();
            UpdateUI(); // Actualizar la UI con los datos cargados

            //Load GameData.json
            Debug.Log("Se ha encontrado un archivo de guardado, se ha cargado de forma exitosa.");
        }
        else
        {
            //InicializarDatos
            InizializeData();
            //Save GameData.json
            SaveData();
            UpdateUI(); // Actualizar la UI con los datos cargados
            Debug.Log("No existe un archivo de guardado, se ha creado uno nuevo.");
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private static string _path = @"C:\Limpeiza Extrema\StoreData.json";


    // Clase para agrupar los datos del jugador
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public float playerMoney;
        public int playerxp;
        public int currentSkin;
        public bool ownSkin00;
        public bool ownSkin01;
        public bool ownSkin02;
        public bool ownSkin03;
    }

    private PlayerData playerData;



    //Metodo para inicializar datos 
    private void InizializeData()
    {
        playerData = new PlayerData()
        {
            playerName = "John Doe",
            playerMoney = 0f,
            playerxp = 0,
            currentSkin = 0,
            ownSkin00 = true,
            ownSkin01 = false,
            ownSkin02 = false,
            ownSkin03 = false
        };
    }



    //Metodo Save StoreData
    private void SaveData()
    {
        //Serializar los datos  a JSON
        string jsonData = JsonUtility.ToJson(playerData, true);

        //Verificar si el directorio existe, si no, crearlo
        string directory = Path.GetDirectoryName(_path);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        //Guardar el archivo JSON
        File.WriteAllText(_path, jsonData);
        Debug.Log("Datos guardados en: " + _path);
    }

    //Metodo Load StoreData
    private void LoadData()
    {
        //Leer el archivo JSON y deserealizar los datos
        string jsonData = File.ReadAllText(_path);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

        Debug.Log("Datos cargados desde: " + _path);
    }

    //Metodo UpdateUI
    public void UpdateUI()
    {
        if (inputName == null) Debug.LogError("inputName no asignado!");
        if (txtMoney == null) Debug.LogError("txtMoney no asignado!");
        if (playerData == null) Debug.LogError("playerData no asignado!");

        if (playerData == null) return;
        if (inputName != null) inputName.text = playerData.playerName;
        if (txtMoney != null) txtMoney.text = playerData.playerMoney.ToString();

        // Actualizar la interfaz de usuario con los datos cargados
        inputName.text = playerData.playerName;
        txtMoney.text = playerData.playerMoney.ToString();
        //txtMoney2.text = playerData.playerMoney.ToString();

        //Actualizar el estado de los botones en dependencia de si el jugador tiene la skin o no
        if (playerData.ownSkin01 == true) //Boton de comprar desaparece y sale el de usar si lo tiene
        {
            if (playerData.currentSkin != 1)
            {
                btnSkin01Buy.SetActive(false);
                btnSkin01Use.SetActive(true);
            }
            else
            {

            }
            
        }
        else
        {
            btnSkin01Buy.SetActive(true);
            
        }

        if (playerData.ownSkin02 == true) //Boton de comprar desaparece y sale el de usar si lo tiene
        {
            btnSkin02Buy.SetActive(false);
            btnSkin02Use.SetActive(true); 
        }
        else
        {
            btnSkin02Buy.SetActive(true);
        }


    }

    public void SkinButton00 ()
    {
        Debug.Log("Funciona bien el boton 00 hp");
    }

    public void SkinButton01()
    {

        int precioSkin01 = 750;
        if (playerData.ownSkin01 != true)
        {
            if(playerData.playerMoney>=precioSkin01)
            {
                playerData.playerMoney -= 750;
                playerData.ownSkin01 = true;

                btnSkin01.SetActive(false);

                //btnSkin01Use.SetActive(true)

                SaveData();
                UpdateUI();
            }
            else
            {
                Debug.Log("Dinero para comprar la Skin con ID 01 insuficiente");
            }
        }
        else
        {
            Debug.Log("Ya tienes esta skin");
        }
            
    }

    public void SkinButton02()
    {
        int precioSkin02 = 750;
        if (playerData.ownSkin02 != true)
        {
            if (playerData.playerMoney >= precioSkin02)
            {
                playerData.playerMoney -= 750;
                playerData.ownSkin02 = true;

                btnSkin02.SetActive(false);

                //btnSkin02Use.SetActive(true)

                SaveData();
                UpdateUI();
            }
            else
            {
                Debug.Log("Dinero para comprar la Skin con ID 01 insuficiente");
            }
        }
        else
        {
            Debug.Log("Ya tienes esta skin");
        }
    }

    // SET VALUE

    /*
     PARA ACTUALIZAR LOS DATOS SERIA ALGO COMO

        playerData.PlayerName = inputName.text;
        playerData.PlayerMoney = 1500f;
        playerData.CurrentSkin = 2;
        playerData.OwnSKinDefault = true;
        playerData.OwnSKin01 = true;
        playerData.OwnSKin02 = false;
        playerData.OwnSKin03 = true;

     // Guardar los datos nuevos
        SaveData();

    // Actualizar la UI
        UpdateUI();
    */
}
