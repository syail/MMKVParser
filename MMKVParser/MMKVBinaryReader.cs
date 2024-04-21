using System.Text;

namespace MMKVParser
{
    internal class MMKVBinaryReader(Stream input) : BinaryReader(input)
    {
        public long ReadVarint()
        {
            long result = 0;
            int shift = 0;
            byte b;
            do
            {
                b = ReadByte();
                result |= ((long)b & 0x7F) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);
            return result;
        }

        /// <summary>
        /// Reads a proto-buf field
        /// 
        /// Format: | Field Length (VarInt) | Field Value |
        /// </summary>
        /// <returns></returns>
        public byte[] ReadProtoField()
        {
            long length = ReadVarint();
            return ReadBytes((int)length);
        }

        public string ReadUTF8StringValue()
        {
            return Encoding.UTF8.GetString(ReadProtoField());
        }
    }
}
