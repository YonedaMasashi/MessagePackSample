using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgPackPractice.Data
{
    public class DataElement
    {
        public long lA;
        public double X1;
        public double Y1;
        public double X2;
        public double Y2;
        public long lS;

        public override string ToString()
        {
            return "Axis," + lA + ",X1," + X1 + ",Y1," + Y1 + ",X2," + X2 + ",Y2," + Y2 + ",Status," + lS;
        }
    }

    public class Piece
    {
        public long PieceNo;
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

    public class Pack
    {
        public long PackNo;
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

    public class Root
    {
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
