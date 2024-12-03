using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.CIModelResolver
{
    internal class StringBuilderExt
    {
        private byte[] buffer;
        private int length;
        private int max = 4096;
        private int unitBytes = 4096;

        //private StringBuilder sb = new StringBuilder();

        public StringBuilderExt()
        {
            buffer = new byte[max];
            length = 0;
        }

        public char this[int index]
        {
            get
            {
                //return (char)sb[index];
                char c = (char)0x00;
                if (index < length)
                {
                    try
                    {
                        c = (char)buffer[index];
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
                return c;
            }
        }

        public void Append(char[] c, int pos, int len)
        {
            //sb.Append(c, pos, len);
            var temp = new byte[buffer.Length];
            if (buffer.Length < length + len)
            {
                temp = new byte[(((length + len) / unitBytes) + 1) * unitBytes];
            }
            buffer.CopyTo(temp, 0);
            for (int i = 0; i < len; i++)
            {
                temp[length + i] = (byte)c[pos + i];
            }
            length += len;
            buffer = temp;
        }

        public String ToString(int pos, int len)
        {
            //return sb.ToString(pos, len);
            return System.Text.Encoding.Default.GetString(buffer.AsSpan(pos, len));
        }

        public int Length
        {
            get
            {
                //return sb.Length;
                return System.Text.Encoding.Default.GetString(buffer).Length;
            }
            set
            {
                //sb.Length = value;
                length = value;
            }
        }
    }
}
