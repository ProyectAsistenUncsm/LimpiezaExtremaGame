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

    public GameObject btnSkin00InUse;
    public GameObject btnSkin01InUse;
    public GameObject btnSkin02InUse;

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

    private static string _path = @"C:\Limpieza Extrema\StoreData.json";


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
        public bool doQuestMetrocentro;
        public bool doQuestCondega;
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
            doQuestMetrocentro = false,
            doQuestCondega = false,
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

        if (playerData.ownSkin00 == true)
        {
            if (playerData.currentSkin != 0)
            {
                btnSkin00Use.SetActive(true);
                btnSkin00InUse.SetActive(false);
            }
            else //Si está la skin seleccionada
            {
                btnSkin00Use.SetActive(false);
                btnSkin00InUse.SetActive(true);
            }
        }


        //Actualizar el estado de los botones en dependencia de si el jugador tiene la skin o no
        if (playerData.ownSkin01 == true) //Boton de comprar desaparece y sale el de usar si lo tiene
        {
            if (playerData.currentSkin != 1)
            {
                btnSkin01Buy.SetActive(false);
                btnSkin01Use.SetActive(true);
                btnSkin01InUse.SetActive(false);
            }
            else //Si está la skin seleccionada
            {
                btnSkin01Buy.SetActive(false);
                btnSkin01Use.SetActive(false);
                btnSkin01InUse.SetActive(true);
            }
        }
        else
        {
            btnSkin01InUse.SetActive(false);
            btnSkin01Use.SetActive(false);
            btnSkin01Buy.SetActive(true);
        }

        if (playerData.ownSkin02 == true) //Boton de comprar desaparece y sale el de usar si lo tiene
        {
            if (playerData.currentSkin != 2)
            {
                btnSkin02Buy.SetActive(false);
                btnSkin02Use.SetActive(true);
                btnSkin02InUse.SetActive(false);
            }
            else //Si está la skin seleccionada
            {
                btnSkin02Buy.SetActive(false);
                btnSkin02Use.SetActive(false);
                btnSkin02InUse.SetActive(true);
            }
        }
        else
        {
            btnSkin02Buy.SetActive(true);
            btnSkin02InUse.SetActive(false);
            btnSkin02Use.SetActive(false);
        }


    }

    public void SkinButton01Buy()
    {

        int precioSkin01 = 750;
        if (playerData.ownSkin01 != true)
        {
            if (playerData.playerMoney >= precioSkin01)
            {
                playerData.playerMoney -= 750;
                playerData.ownSkin01 = true;

                // btnSkin01.SetActive(false);

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

    public void SkinButton02Buy()
    {
        int precioSkin02 = 750;
        if (playerData.ownSkin02 != true)
        {
            if (playerData.playerMoney >= precioSkin02)
            {
                playerData.playerMoney -= 750;
                playerData.ownSkin02 = true;

                //btnSkin02.SetActive(false);

                //btnSkin02Use.SetActive(true)

                SaveData();
                UpdateUI();
            }
            else
            {
                Debug.Log("Dinero para comprar la Skin con ID 02 insuficiente");
            }
        }
        else
        {
            Debug.Log("Ya tienes esta skin");
        }
    }

    public void SkinButton00Use()
    {
        playerData.currentSkin = 0; // Cambiar la skin actual a la skin 00
        SaveData(); // Guardar los datos después de cambiar la skin
        UpdateUI(); // Actualizar la UI después de cambiar la skin
        Debug.Log("Skin 00 en uso");
    }
    public void SkinButton01Use()
    {
        playerData.currentSkin = 1; // Cambiar la skin actual a la skin 01
        SaveData(); 
        UpdateUI();
        Debug.Log("Skin 01 en uso");
    }

    public void SkinButton02Use()
    {
        playerData.currentSkin = 2; // Cambiar la skin actual a la skin 02
        SaveData();
        UpdateUI();
        Debug.Log("Skin 02 en uso");
    }
}
