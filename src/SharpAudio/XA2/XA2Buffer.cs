﻿using SharpDX;

namespace SharpAudio.XA2
{
    internal class XA2Buffer : AudioBuffer
    {
        private DataStream _dataStream;
        private SharpDX.XAudio2.AudioBuffer _buffer;

        public SharpDX.XAudio2.AudioBuffer Buffer => _buffer;

        public int SizeInBytes { get; private set; }
        public int TotalSamples => SizeInBytes / Format.BytesPerSample;

        public XA2Buffer()
        {
            _buffer = new SharpDX.XAudio2.AudioBuffer();
        }

        public override unsafe void BufferData<T>(T[] buffer, AudioFormat format)
        {
            int sizeInBytes = sizeof(T) * buffer.Length;

            _dataStream?.Dispose();
            _dataStream = new DataStream(sizeInBytes, true, true);
            _dataStream.WriteRange(buffer, 0, buffer.Length);
            _dataStream.Position = 0;

            _format = format;
            SizeInBytes = sizeInBytes;
            _buffer.AudioDataPointer = _dataStream.PositionPointer;
            _buffer.AudioBytes = SizeInBytes;
        }

        public override void Dispose()
        {
            _dataStream?.Dispose();
         }
    }
}
