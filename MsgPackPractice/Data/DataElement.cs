using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgPackPractice.Data
{
    [MessagePackObject]
    public class DataElement
    {
        [Key(0)]
        public long lA;
        [Key(1)]
        public double X1;
        [Key(2)]
        public double Y1;
        [Key(3)]
        public double X2;
        [Key(4)]
        public double Y2;
        [Key(5)]
        public long lS;

        public override string ToString()
        {
            return "Axis," + lA + ",X1," + X1 + ",Y1," + Y1 + ",X2," + X2 + ",Y2," + Y2 + ",Status," + lS;
        }
    }

    [MessagePackObject]
    public class Piece {
        [Key(0)]
        public long PieceNo;
        [Key(1)]
        public List<DataElement> DataList = new List<DataElement>();

        public override string ToString()
        {
            string result = "PieceNo," + PieceNo;
            foreach(var elem in DataList)
            {
                result += elem + Environment.NewLine;
            }
            return result;
        }
    }

    [MessagePackObject]
    public class Pack {
        [Key(0)]
        public long PackNo;
        [Key(1)]
        public List<Piece> PieceList = new List<Piece>();

        public override string ToString()
        {
            string result = "PackNo," + PackNo;
            foreach(var Piece in PieceList)
            {
                result += Piece + Environment.NewLine;
            }
            return result;
        }
    }

    [MessagePackObject]
    public class Root {
        [Key(0)]
        public List<Pack> PackList = new List<Pack>();

        public override string ToString()
        {
            string result = "";
            foreach(var elem in PackList)
            {
                result += elem + Environment.NewLine;
            }
            return result;
        }
    }
}
