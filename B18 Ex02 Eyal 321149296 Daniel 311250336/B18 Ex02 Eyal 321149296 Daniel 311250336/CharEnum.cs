using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public class CharEnum
    {
        public enum eCharsToAddOrPrint : byte
        {
            Empty = (byte)' ',
            VerticalSeperator = (byte)'|',
            HorizontalSeperator = (byte)'=',
            CaptialLetterToInt = (byte)'A',
            SmallLetterToInt = (byte)'a',
            MoveToSymbol = (byte)'>',
        }
    }
}
