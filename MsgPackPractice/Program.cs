using MsgPackPractice.Converter;
using MsgPackPractice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MsgPackPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Root();

            SetData(out root);

            // XML
            //{
            //    // 出力
            //    {
            //        var sw = new System.Diagnostics.Stopwatch();
            //        sw.Start();
            //        ExtensionsClass.Object2XDocument(root).Save("DataElem.xml");
            //        sw.Stop();
            //        Console.WriteLine("■XML出力にかかった時間");
            //        TimeSpan ts = sw.Elapsed;
            //        Console.WriteLine($"　{ts}");
            //    }

            //    // 読込
            //    {
            //        var sw = new System.Diagnostics.Stopwatch();
            //        sw.Start();
            //        XDocument xdoc = XDocument.Load("DataElem.xml");
            //        var xmlRoot = ExtensionsClass.XDocument2Object<Root>(xdoc);
            //        sw.Stop();
            //        Console.WriteLine("■XML読込にかかった時間");
            //        TimeSpan ts = sw.Elapsed;
            //        Console.WriteLine($"　{ts}");
            //    }
            //}

            // XML
            {
                //保存先のファイル名
                string fileName = @"DataElem.xml";
                {
                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    //XmlSerializerオブジェクトを作成
                    //オブジェクトの型を指定する
                    System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Root));
                    //書き込むファイルを開く（UTF-8 BOM無し）
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(
                        fileName, false, new System.Text.UTF8Encoding(false));
                    //シリアル化し、XMLファイルに保存する
                    serializer.Serialize(streamWriter, root);
                    //ファイルを閉じる
                    streamWriter.Close();

                    sw.Stop();
                    Console.WriteLine("■XML 出力にかかった時間");
                    TimeSpan ts = sw.Elapsed;
                    Console.WriteLine(string.Format("  {0}", ts));
                }

                {
                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    //XmlSerializerオブジェクトを作成
                    System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Root));
                    //読み込むファイルを開く
                    System.IO.StreamReader sr = new System.IO.StreamReader(
                        fileName, new System.Text.UTF8Encoding(false));
                    //XMLファイルから読み込み、逆シリアル化する
                    Root obj = (Root)serializer.Deserialize(sr);
                    //ファイルを閉じる
                    sr.Close();

                    sw.Stop();
                    Console.WriteLine("■XML 読込にかかった時間");
                    TimeSpan ts = sw.Elapsed;
                    Console.WriteLine(string.Format("  {0}", ts));
                }
            }

            // MessagePack
            {
                // 出力
                {
                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    System.IO.File.WriteAllBytes("manage.bin", ExtensionsClass.Object2MessagePack(root));
                    sw.Stop();
                    Console.WriteLine("■MessagePack 出力にかかった時間");
                    TimeSpan ts = sw.Elapsed;
                    Console.WriteLine(string.Format("  {0}", ts));
                }
                // 読込
                {
                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    var msgpackRoot = System.IO.File.ReadAllBytes("manage.bin").MessagePack2Object<Root>();
                    sw.Stop();
                    Console.WriteLine("■MessagePack 読込にかかった時間");
                    TimeSpan ts = sw.Elapsed;
                    Console.WriteLine(string.Format("  {0}", ts));
                }
            }
        }

        static void SetData(out Root root)
        {
            root = new Root();

            for (int i = 0; i < 25; ++i)
            {
                var pack = new Pack();
                pack.PackNo = i;
                root.PackList.Add(pack);

                for (int PieceNo = 0; PieceNo < 4; ++PieceNo)
                {
                    var Piece = new Piece();
                    Piece.PieceNo = PieceNo + 1;
                    pack.PieceList.Add(Piece);

                    for (int dataNo = 0; dataNo < 7500; ++dataNo)
                    {
                        var data = new DataElement();
                        Piece.DataList.Add(data);
                        data.lA = dataNo % 3;
                        data.X1 = dataNo + (7500 * PieceNo);
                        data.Y1 = dataNo + (7500 * PieceNo) + 0.1;
                        data.X2 = dataNo + (7500 * PieceNo) + 0.2;
                        data.Y2 = dataNo + (7500 * PieceNo) + 0.3;
                        data.lS = dataNo % 50;
                    }
                }
            }
        } 
    }
}
