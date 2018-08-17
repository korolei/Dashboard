using System;
using System.IO;

namespace Dashboard.Common.Utilities
{
    public static class StreamHelper
    {
        private const int DEFAULT_CHUNK_SIZE = 64 * 1024;

        public static byte[] ReadAllBytes(Stream stream)
        {
            if (!stream.CanSeek)
                return ToReadableMemoryStream(stream).ToArray();
            else
            {
                byte[] byteArray = new byte[stream.Length];
                int currPos = 0;
                int readSize = 0;

                stream.Position = 0;

                do
                {
                    int toRead = Math.Min(DEFAULT_CHUNK_SIZE, byteArray.Length - currPos);

                    readSize = stream.Read(byteArray, currPos, toRead);
                    currPos += readSize;
                } while (readSize != 0);

                return byteArray;
            }
        }

        public static MemoryStream ToReadableMemoryStream(Stream stream)
        {
            return ToReadableMemoryStream(stream, DEFAULT_CHUNK_SIZE);
        }

        public static MemoryStream ToReadableMemoryStream(Stream stream, int readSize)
        {
            MemoryStream ostream = null;

            try
            {
                ostream = new MemoryStream();

                if (stream != null)
                    WriteToStream(stream, ostream, readSize);

                ostream.Position = 0;
            }
            catch (Exception)
            {
                if (ostream != null)
                {
                    ostream.Dispose();
                    ostream = null;
                }

                throw;
            }

            return ostream;
        }

        public static Stream ToSeekableStream(Stream stream)
        {
            if (stream.CanSeek)
                return stream;

            MemoryStream convertedStream = null;
            MemoryStream memStream = stream as MemoryStream;

            if (memStream != null)
                convertedStream = new MemoryStream(memStream.GetBuffer());
            else
            {
                try
                {
                    convertedStream = new MemoryStream();

                    WriteToStream(stream, convertedStream, DEFAULT_CHUNK_SIZE);
                    convertedStream.Position = 0;
                }
                catch (Exception)
                {
                    if (convertedStream != null)
                    {
                        convertedStream.Dispose();
                        convertedStream = null;
                    }

                    throw;
                }
            }

            return convertedStream;
        }

        public static void WriteToStream(Stream input, Stream output)
        {
            WriteToStream(input, output, DEFAULT_CHUNK_SIZE);
        }

        public static void WriteToStream(Stream input, Stream output, int chunkSize)
        {
            byte[] buffer = new byte[chunkSize];
            int bytesRead = -1;

            if (!input.CanRead)
                throw new InvalidOperationException("Can not read from input stream!");
            else if (!output.CanWrite)
                throw new InvalidOperationException("Can not write to output stream!");

            do
            {
                bytesRead = input.Read(buffer, 0, chunkSize);
                output.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0);
        }
    }
}
