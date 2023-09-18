using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class SavedData
    {
        public int HighScore = default;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void FromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}