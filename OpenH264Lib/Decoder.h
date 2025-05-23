// OpenH264Lib.h

#pragma once
#include <codec_api.h>
#include <cstddef>

using namespace System;

namespace OpenH264Lib {

	public ref class Decoder
	{
	private:
		ISVCDecoder* decoder;

	private:
		typedef int(__stdcall *WelsCreateDecoderFunc)(ISVCDecoder** ppDecoder);
		WelsCreateDecoderFunc CreateDecoderFunc;
		typedef void(__stdcall *WelsDestroyDecoderFunc)(ISVCDecoder* ppDecoder);
		WelsDestroyDecoderFunc DestroyDecoderFunc;

	private:
		~Decoder(); // デストラクタ
		!Decoder(); // ファイナライザ
	public:
		Decoder(String ^dllName);

	private:
		int Setup();

	public:
		///<summary>Decode h264 frame data to Bitmap.</summary>
		///<returns>Bitmap. Might be null if frame data is incomplete.</returns>
		byte* Decode(array<Byte> ^frame, int length);
		byte* Decode(unsigned char *frame, int length);

	private:
		static byte* YUV420PtoRGB(byte* yplane, byte* uplane, byte* vplane, int width, int height, int stride);
	};
}
