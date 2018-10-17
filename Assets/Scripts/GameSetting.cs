using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BigPlane {
    /// <summary>
    /// 游戏数据配置
    /// </summary>
    public static class GameSetting {

        static Dictionary<string, string> setting;

        const string settingFileName = "Texts/GameSetting";

        static GameSetting() {
            setting = new Dictionary<string, string>();
            TextAsset text = Resources.Load<TextAsset>(settingFileName);
            setting = SimpleCsv.OpenCsvAsKV(text);
        }

        public static string GetSetting(string key) {
            if (setting.ContainsKey(key))
                return setting[key];
            else
                return string.Empty;
        }
    }

}
