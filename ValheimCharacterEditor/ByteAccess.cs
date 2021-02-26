using System.IO;

namespace ValheimCharacterEditor
{
    class ByteAccess
    {
        private BinaryWriter writer;
        private BinaryReader reader;
        private MemoryStream stream = new MemoryStream();

        public ByteAccess(byte[] data)
        {
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
            stream.Write(data,0,data.Length);
            stream.Position = 0;
        }
        public ByteAccess()
        {
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
        }

        public long Length()
        {
            return stream.Length;
        }

        public int ReadInt32()
        {
            return reader.ReadInt32();
        }
        public long ReadInt64()
        {
            return reader.ReadInt64();
        }
        public bool ReadBoolean()
        {
            return reader.ReadBoolean();
        }
        public float ReadSingle()
        {
            return reader.ReadSingle();
        }
        public string ReadString()
        {
            return reader.ReadString();
        }
        public ValheimEngine.Vector3 ReadVector3()
        {
            var vector3 = new ValheimEngine.Vector3
            {
                X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()
            };
            return vector3;
        }
        public byte[] ReadBytes()
        {
            int length = reader.ReadInt32();
            return reader.ReadBytes(length);
        }

        public void Write(int data)
        {
            writer.Write(data);
        }
        public void Write(long data)
        {
            writer.Write(data);
        }
        public void Write(bool data)
        {
            writer.Write(data);
        }
        public void Write(float data)
        {
            writer.Write(data);
        }
        public void Write(string data)
        {
            writer.Write(data);
        }
        public void Write(ValheimEngine.Vector3 data)
        {
            writer.Write(data.X);
            writer.Write(data.Y);
            writer.Write(data.Z);
        }
        public void Write(byte[] data)
        {
            writer.Write(data.Length);
            writer.Write(data);
        }
        public byte[] ToArray()
        {
            writer.Flush();
            stream.Flush();
            return stream.ToArray();
        }
        public void Clear()
        {
            writer.Flush();
            stream.SetLength(0);
            stream.Position = 0;
        }
    }
}
