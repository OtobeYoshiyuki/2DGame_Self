using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OtobeGame;

namespace OtobeLib
{
    //二次元配列のインデックスを扱う構造体
    public struct TwoDimensions
    {
        //縦の情報
        public int col;

        //横の情報
        public int raw;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">縦の情報</param>
        /// <param name="y">横の情報</param>
        public TwoDimensions(int x, int y)
        {
            //縦の情報を設定する
            col = x;
            //横の情報を設定する
            raw = y;
        }
    }

    /// <summary>
    /// ファイルの入力、出力を管理するクラス
    /// </summary>
    public class FileManager
    {
        //探していたものが見つからなかったとき
        public const int FIND_ERROR = -1;

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

        /// <summary>
        /// 対象の文字列のインデックスを検索する
        /// </summary>
        /// <param name="csvDatas">対象のCsvデータ</param>
        /// <param name="inputData">検索する文字列</param>
        /// <returns>文字列が入っているインデックス</returns>
        public TwoDimensions Find_InputData_Index(List<List<string>> csvDatas,string inputData)
        {
            for (int i = 0; i < csvDatas.Count; i++)
            {
                for (int j = 0; j < csvDatas[i].Count; j++)
                {
                    //探していたものが見つかった場合は、インデックスを返す
                    if (csvDatas[i][j] == inputData) return new TwoDimensions(i, j);
                }
            }

            //見つからなかったときは-1を返す
            return new TwoDimensions(FIND_ERROR, FIND_ERROR);
        }

        /// <summary>
        /// 使用するデータのみを抽出する
        /// </summary>
        /// <param name="before">変換前のファイルの情報</param>
        /// <param name="first">開始のインデックス</param>
        /// <param name="finish">終了のインデックス</param>
        /// <returns>変換後のファイルの情報</returns>
        public List<List<string>> Use_InputData(List<List<string>> before, TwoDimensions first, TwoDimensions finish)
        {
            //変換するコンテナを宣言する
            List<List<string>> convertList = new List<List<string>>();

            for (int i = first.col; i <= finish.col; i++)
            {
                //一時的に格納するコンテナを宣言する
                List<string> convert = new List<string>();

                for (int j = GameMain.NULL; j <= before[i].Count; j++)
                {
                    //コンテナに情報を格納する
                    convert.Add(before[i][j]);
                }
                //１行ずつコンテナに格納する
                convertList.Add(convert);
            }
            //変換後のファイルの情報を返す
            return convertList;
        }
    }

}

