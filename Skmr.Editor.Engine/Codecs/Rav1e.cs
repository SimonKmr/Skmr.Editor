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

        #region config
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_config_default();

        /// <summary>
        /// Setup a second pass rate control using the provided summary
        /// </summary>
        /// <returns>
        ///`0` on success
        ///`&gt; 0` if the buffer has to be larger 
        ///`&lt; 0` on failure
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
        public static extern Int32 rav1e_config_parse(IntPtr cfg, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_config_parse_int(IntPtr cfg, [MarshalAs(UnmanagedType.LPStr)] string key, Int32 value);
        #endregion

        #region context
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_context_new(IntPtr cfg);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_context_unref(IntPtr ctx);
        #endregion

        #region frame
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_frame_new(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_frame_unref(IntPtr frame);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_set_type(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_set_opaque(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_frame_add_t35_metadata(IntPtr frame, byte country_code, byte country_code_extension_byte, IntPtr data, IntPtr size_t);
        #endregion

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
        /// still pending in the encoder.
        /// </summary>
        /// <returns>
        /// - `0` on success,
        /// - `&gt; 0` if the input queue is full
        /// - `&lt; 0` on unrecoverable failure
        /// </returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern EncoderStatus rav1e_send_frame(IntPtr ctx, IntPtr frame);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern EncoderStatus rav1e_last_status();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_status_to_str(EncoderStatus status);

        /// <summary>
        /// Receive encoded data
        /// </summary>
        /// <returns></returns>
        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern Int32 rav1e_receive_packet();


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_packet_unref(IntPtr pkt);

        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern IntPtr rav1e_container_sequence_header(IntPtr ctx);


        [DllImport("Codecs/Dlls/rav1e.dll")]
        public static extern void rav1e_frame_fill_plane(IntPtr frame, int plane, IntPtr data, IntPtr data_len, IntPtr stride, int bytewidth);


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
        public struct Rational
        {
            /// Numerator.
            public UInt64 num;
            /// Denominator.
            public UInt64 den;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Packet
        {
            public IntPtr data;
            public IntPtr len;
            public ulong input_frameno;
            public FrameType frame_type;
            public IntPtr opaque;
            public IntPtr rec;
            public IntPtr source;
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

        public enum FrameType
        {
            KEY,
            INTER,
            INTRA_ONLY,
            SWITCH,
        }
    }
}
