using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

namespace BigPlane {

    /// <summary>
    /// 从文件中来的数据记录
    /// </summary>
    public interface IRecord {

        int id { get; }

        bool isEmpty();

        void InitFromLine(string s);
        
    }
    /// <summary>
    /// 逗号分隔的行列表,会忽略//开头的行,字符串中间的//往后的内容也会被忽略
    /// </summary>
    public static class SimpleCsv {

        const string pattern = @"[\r\n]|//+.*[\r\n]";

        static SimpleCsv(){}

        /// <summary>
        /// 根据一个res名字载入资源
        /// </summary>
        /// <returns></returns>
        public static string[] OpenCsv(string path) {
            TextAsset text = Resources.Load<TextAsset>(path);
            
            if (text != null)
                return OpenCsv(text);
            else
                throw new UnityException($"读取文件错误:{path}");
        }

        /// <summary>
        /// 获取csv的所有数据
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] OpenCsv(TextAsset text) {
            Regex reg = new Regex(pattern);
            string[] lines = reg.Split(text.text);
            return lines.Where((x) => !string.IsNullOrEmpty(x)).ToArray<string>();
        }


        public static Dictionary<int, T> OpenCsvAs<T>(string path) where T : IRecord, new() {
            TextAsset text = Resources.Load<TextAsset>(path);

            if (text != null)
                return OpenCsvAs<T>(text);
            else
                throw new UnityException($"读取文件错误:{path}");
        }


        public static Dictionary<int,T> OpenCsvAs<T>(TextAsset text) where T:IRecord,new() {

            Dictionary<int, T> dict = new Dictionary<int, T>();
            string[] lines = OpenCsv(text);

            if (lines.Length == 0)
                throw new UnityException($"{text.name}没有数据");

            for (int i = 0; i < lines.Length; i++) {
                T t = new T();
                t.InitFromLine(lines[i]);
                dict.Add(t.id, t);
            }
            return dict;
        }


        public static Dictionary<string, string> OpenCsvAsKV(TextAsset text) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] lines = OpenCsv(text);
            if(lines.Length==0)
                throw new UnityException($"{text.name}没有数据");
            string[] cells;
            for (int i = 0; i < lines.Length; i++) {
                cells = lines[i].Split(',');
                dict.Add(cells[0], cells[1]);
            }
            return dict;
        }

    }
}