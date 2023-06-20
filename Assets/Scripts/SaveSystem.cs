using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem {

        public static void SavePlayer(GameMaster gm) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + gm.scene.name + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gm);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string levelName) {

        string path = Application.persistentDataPath + "/" + levelName + ".fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
