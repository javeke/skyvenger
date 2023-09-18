using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public static class DataManager
    {
        private const string DataFile = "saved.json";
        public static bool SaveJsonData(string data)
        {
            if (FileManager.WriteSync(DataFile, data))
            {
                Debug.Log("Data Saved!");
                return true;
            }
            else
            {
                Debug.Log("Unable To Save Data");
                return false;
            }
        }

        public static string LoadJsonData()
        {
            if(!FileManager.FileExistsSync(DataFile) && !FileManager.CreateFileSync(DataFile)) {
                Debug.Log("Failed To Create New File");
                return null;
            }

            if(FileManager.ReadSync(DataFile, out string data))
            {
                return data;
            }
            else
            {
                Debug.Log("Unable To Load Saved Data");
                return null;
            }
        }
    }
}