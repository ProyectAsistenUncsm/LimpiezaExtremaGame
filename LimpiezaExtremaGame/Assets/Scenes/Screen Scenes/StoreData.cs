using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Experimental.RestService;
using UnityEngine.UI;
using System;


public class StoreData : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField inputName;         // InputField para el nombre del jugador
    public Text txtMoney;               // Text para mostrar el dinero del jugador
    public Text txtCurrentSkin;         // Text para mostrar la skin actual
    public Text txtDefaultSkin;         // Text para mostrar si la skin default está disponible
    public Text txtSkin01;              // Text para mostrar si la skin 01 está disponible
    public Text txtSkin02;              // Text para mostrar si la skin 02 está disponible
    public Text txtSkin03;              // Text para mostrar si la skin 03 está disponible

    // Start is called before the first frame update
    private void Awake()
    {
        //Intentar cargar los datos del jugador
        if (File.Exists(_path))
        {
            LoadData();

            //Load GameData.json
            Debug.Log("Se ha encontrado un archivo de guardado, se ha cargado de forma exitosa.");
        }
        else
        {
            //InicializarDatos
            InizializeData();
            //Save GameData.json
            SaveData();

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
    private void UpdateUI()
    {
        // Actualizar la interfaz de usuario con los datos cargados
        inputName.text = playerData.playerName;
        txtMoney.text = $"Money: {playerData.playerMoney}";
        txtCurrentSkin.text = $"Current Skin: {playerData.currentSkin}";
        txtDefaultSkin.text = $"Own Default: {playerData.ownSkin00}";
        txtSkin01.text = $"Own Skin 01: {playerData.ownSkin01}";
        txtSkin02.text = $"Own Skin 02: {playerData.ownSkin02}";
        txtSkin03.text = $"Own Skin 03: {playerData.ownSkin03}";
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
