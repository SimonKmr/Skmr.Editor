using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    public class Rav1e
    {
        /// <summary>
        /// This returns the version of the loaded library, regardless
        /// of which version the library user was built against.
        /// </summary>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_version_short();

        /// <summary>
        /// This returns the version of the loaded library, regardless
        /// of which version the library user was built against.
        /// </summary>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_version_full();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_data_unref(Data data);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_config_default();

        /// <summary>
        /// Setup a second pass rate control using the provided summary
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="data"></param>
        /// <param name="size_t"></param>
        /// <returns>
        /// `0` on success 
        /// `> 0` if the buffer has to be larger 
        /// `< 0` on failure
        /// </returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_set_rc_summary(IntPtr cfg, IntPtr data, UInt64 size_t);

        /// <summary>
        /// Request to emit pass data
        /// </summary>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_config_set_emit_data(IntPtr cfg, Int32 data);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_config_set_sample_aspect_ratio(IntPtr cfg, Rational sample_aspect_ratio);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_config_set_time_base(IntPtr cfg, Rational time_base);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_set_pixel_format(IntPtr cfg, Byte bit_depth);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_set_color_description(IntPtr cfg, Int32 data);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_set_content_light(IntPtr cfg, Int16 max_content_light_level, Int16 max_frame_average_light_level);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_set_mastering_display(IntPtr cfg, IntPtr primaries, string white_point, UInt32 max_luminance, UInt32 min_luminance);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_config_unref(IntPtr cfg);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_parse(IntPtr cfg, IntPtr key, IntPtr value);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_parse(IntPtr cfg, IntPtr key, Int32 value);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_parse_int(IntPtr cfg, IntPtr key, Int32 value);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_context_new(IntPtr cfg);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_context_unref(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_frame_new(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_frame_unref(IntPtr frame);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_set_type(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_set_opaque(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_add_t35_metadata(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_twopass_out(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_rc_summary_size(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_rc_receive_pass_data(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_rc_second_pass_data_required();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_rc_send_pass_data(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_twopass_bytes_needed(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_twopass_in(IntPtr ctx);

        /// <summary>
        /// Send the frame for encoding
        ///
        /// The function increases the frame internal reference count and it can be passed multiple
        /// times to different `rav1e_send_frame()` with a caveat:
        ///
        /// The opaque data, if present, will be moved from the `Frame` to the `Context`
        /// and returned by `rav1e_receive_packet` in the `Packet` `opaque` field or
        /// the destructor will be called on `rav1e_context_unref` if the frame is
        /// still pending in the encoder.
        /// </summary>
        /// <returns>
        /// - `0` on success,
        /// - `> 0` if the input queue is full
        /// - `< 0` on unrecoverable failure
        /// </returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern EncoderStatus rav1e_send_frame(IntPtr ctx, Frame frame);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern EncoderStatus rav1e_last_status();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_status_to_str(EncoderStatus status);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_receive_packet();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_packet_unref(IntPtr pkt);

        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_container_sequence_header();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_fill_plane_internal();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_fill_plane();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_extract_plane();


        [StructLayout(LayoutKind.Sequential)]
        public struct Data
        {
            public IntPtr data;
            public UInt64 size_t;
        }

        public struct FrameOpaque
        {
            public IntPtr data;
            public IntPtr cb;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Frame
        {
            public IntPtr fi;
            public IntPtr frame_type;
            public IntPtr opaque;
            public IntPtr t35_metadata;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct ChromaSampling
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MatrixCoefficients
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ChromaticityPoint
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FrameTypeOverride
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FrameOpaqueCb
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rational
        {
            /// Numerator.
            public UInt64 num;
            /// Denominator.
            public UInt64 den;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct EncoderConfig
        {
            public uint width; 
            public uint height; 
            public Rational sample_aspect_ratio;
            public Rational time_base;

            public uint bit_depth;
            public ChromaSampling chroma_sampling;
            public ChromaSamplePosition chroma_sample_position;
            public PixelRange pixel_range;
            public ColorDescription? color_description;
            public MasteringDisplay? mastering_display;
            public ContentLight? content_light;

            public byte? level_idx;
            public byte enable_timing_info;
            public byte still_picture;
            public byte error_resilient;

            public ulong switch_frame_interval;
            public ulong min_key_frame_interval;
            public ulong max_key_frame_interval;

            public int? reservoir_frame_delay;
            public byte low_latency;
            public uint quantizer;
            public byte min_quantizer;
            public int bitrate;

            public Tune tune;
            public GrainTableSegment? film_grain_params;

            public uint tiles_cols;
            public uint tiles_rows;
            public uint tiles;
            public SpeedSettings speed_setting;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GrainTableSegment
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorDescription
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Tune
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MasteringDisplay
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ContentLight
        {

        }

        public struct SpeedSettings
        {
            public byte multiref;
            public byte fast_deblock;
        }

        public static IntPtr GetConfigPointer(EncoderConfig config)
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(EncoderConfig)));
            Marshal.StructureToPtr(config, ptr, true);
            return ptr;
        }

        public enum EncoderStatus : int
        {
            Success = 0,
            NeedMoreData,
            EnoughData,
            LimitReached,
            Encoded,
            Failure = -1,
            NotReady = -2,
        }

        public enum PixelRange
        {
            Limited,
            Full
        }

        public enum ChromaSamplePosition
        {
            Unknown,
            Vertical,
            Colocated,
        }

        public enum ColorPrimaries : int
        {
            /// BT.709
            BT709 = 1,
            /// Unspecified, must be signaled or inferred outside of the bitstream
            Unspecified,
            /// BT.470 System M (historical)
            BT470M = 4,
            /// BT.470 System B, G (historical)
            BT470BG,
            /// BT.601-7 525 (SMPTE 170 M)
            BT601,
            /// SMPTE 240M (historical)
            SMPTE240,
            /// Generic film
            GenericFilm,
            /// BT.2020, BT.2100
            BT2020,
            /// SMPTE 248 (CIE 1921 XYZ)
            XYZ,
            /// SMPTE RP 431-2
            SMPTE431,
            /// SMPTE EG 432-1
            SMPTE432,
            /// EBU Tech. 3213-E
            EBU3213 = 22,
        }
    }
}
