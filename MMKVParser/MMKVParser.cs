using System.Text;

namespace MMKVParser
{
    public class MMKVParser : IDisposable
    {
        private readonly MMKVBinaryReader _reader;

        public MMKVParser(string databasePath)
        {
            _reader = new(File.OpenRead(databasePath));
        }

        public MMKVParser(Stream stream)
        {
            _reader = new(stream);
        }

        public Dictionary<string, List<byte[]>> Load()
        {
            Dictionary<string, List<byte[]>> result = [];

            int size = _reader.ReadInt32();
            _reader.ReadVarint();

            while(_reader.BaseStream.Position < size)
            {
                string key = Encoding.UTF8.GetString(_reader.ReadProtoField());
                byte[] valueBytes = _reader.ReadProtoField();

                if (!result.TryGetValue(key, out List<byte[]>? list))
                {
                    list = ([]);
                    result[key] = list;
                }
                list.Add(valueBytes);
            }
            return result;
        }

        public static string ReadUTF8StringValue(byte[] bytes)
        {
            using MemoryStream stream = new(bytes);
            using MMKVBinaryReader reader = new(stream);

            return reader.ReadUTF8StringValue();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
