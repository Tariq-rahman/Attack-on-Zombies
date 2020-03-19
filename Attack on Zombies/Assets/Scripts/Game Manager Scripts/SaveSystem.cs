using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveData(GameObject player, GameManager GM)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        Debug.Log("save path is: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player, GM);
        Debug.Log(playerData.vaccines);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }
	
    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;       
            stream.Close();
            return data;

        } else
        {
            Debug.LogError("Savefile not found in " + path);
            return null;
        }
    }
}
