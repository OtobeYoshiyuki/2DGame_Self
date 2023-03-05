using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// ファイルの入力、出力を管理するクラス
/// </summary>
public class FileManager
{
    //読み込んだデータを扱うコンテナ
    private List<List<string>> m_inputData = null;

    //出力するデータを扱うコンテナ
    private List<List<string>> m_outputData = null;

    /// <summary>
    /// 指定したCsvファイルを読み込む
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>読み込んだ情報</returns>
    public List<List<string>> InputDataCsv(string fileName)
    {
        //読み込んだデータを格納するコンテナを生成する
        m_inputData = new List<List<string>>();

        //Resourceにある指定のパスのCSVファイルを格納
        TextAsset csvFile = Resources.Load(fileName) as TextAsset;

        //TextAssetをStringReaderに変換
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            //１行ずつ読む
            string line = reader.ReadLine();

            //読みこんだDataをリストにAddする
            string[] lineData = line.Split(',');

            //string[]からList<string>へ変換する
            List<string> convert = new List<string>();

            foreach (string str in lineData)
            {
                convert.Add(str);
            }

            //読み込んだ情報を返す
            m_inputData.Add(convert);
        }

        return m_inputData;
    }
}
