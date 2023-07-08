using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs
{
    public partial class Rav1e
    {
        public partial class Api
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
            public static extern void rav1e_data_unref(ref Data data);

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

            /// <summary>
            /// Set a configuration parameter using its key and value as string.
            ///
            /// Available keys and values
            /// - `"width"`: width of the frame, default `640`
            /// - `"height"`: height of the frame, default `480`
            /// - `"speed"`: 0-10, default `6`
            /// - `"threads"`: maximum number of threads to be used, default auto
            /// - `"tune"`: `"psnr"` or `"psychovisual"`, default `"psychovisual"`
            /// - `"quantizer"`: 0-255, default `100`
            /// - `"tiles"`: total number of tiles desired (0 denotes auto), default `0`
            /// - `"tile_rows"`: number of tiles horizontally (must be a power of two, overridden by tiles if present), default `0`
            /// - `"tile_cols"`: number of tiles vertically (must be a power of two, overridden by tiles if present), default `0`
            /// - `"min_quantizer"`: minimum allowed base quantizer to use in bitrate mode, default `0`
            /// - `"bitrate"`: target bitrate for the bitrate mode (required for two pass mode), default `0`
            /// - `"key_frame_interval"`: maximum interval between two keyframes, default `240`
            /// - `"min_key_frame_interval"`: minimum interval between two keyframes, default `12`
            /// - `"switch_frame_interval"`: interval between switch frames, default `0`
            /// - `"reservoir_frame_delay"`: number of temporal units over which to distribute the reservoir usage, default `None`
            /// - `"rdo_lookahead_frames"`: number of frames to read ahead for the RDO lookahead computation, default `40`
            /// - `"low_latency"`: flag to enable low latency mode, default `false`
            /// - `"enable_timing_info"`: flag to enable signaling timing info in the bitstream, default `false`
            /// - `"still_picture"`: flag for still picture mode, default `false`
            /// </summary>
            /// <param name="cfg"></param>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <returns></returns>
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

            /// <summary>
            /// Retrieve the first-pass data of a two-pass encode for the frame that was
            /// just encoded. This should be called BEFORE every call to `rav1e_receive_packet()`
            /// (including the very first one), even if no packet was produced by the
            /// last call to `rav1e_receive_packet`, if any (i.e., `RA_ENCODER_STATUS_ENCODED`
            /// was returned). It needs to be called once more after
            /// `RA_ENCODER_STATUS_LIMIT_REACHED` is returned, to retrieve the header that
            /// should be written to the front of the stats file (overwriting the
            /// placeholder header that was emitted at the start of encoding).
            ///
            /// It is still safe to call this function when `rav1e_receive_packet()` returns any
            /// other error. It will return `NULL` instead of returning a duplicate copy
            /// of the previous frame's data.
            ///
            /// Must be freed with `rav1e_data_unref()`.
            /// </summary>
            /// <param name="ctx"></param>
            /// <returns></returns>
            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern ref Data rav1e_twopass_out(IntPtr ctx);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_rc_summary_size(IntPtr ctx);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern RcDataKind rav1e_rc_receive_pass_data(IntPtr ctx, IntPtr data);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_rc_second_pass_data_required(IntPtr ctx);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_rc_send_pass_data(IntPtr ctx);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_twopass_bytes_needed(IntPtr ctx);


            /// <summary>
            /// Provide stats data produced in the first pass of a two-pass encode to the
            /// second pass. On success this returns the number of bytes of that data
            /// which were consumed. When encoding the second pass of a two-pass encode,
            /// this should be called repeatedly in a loop before every call to
            /// `rav1e_receive_packet()` (including the very first one) until no bytes are
            /// consumed, or until `twopass_bytes_needed()` returns 0. Returns -1 on failure.
            /// </summary>
            /// <param name="ctx"></param>
            /// <returns></returns>
            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_twopass_in(IntPtr ctx, IntPtr buf, IntPtr buf_size);

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
            public static extern EncoderStatus rav1e_receive_packet(IntPtr ctx, ref IntPtr pkt);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern void rav1e_packet_unref(IntPtr pkt);

            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern IntPtr rav1e_container_sequence_header(IntPtr ctx);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern void rav1e_frame_fill_plane(IntPtr frame, int plane, IntPtr data, IntPtr data_len, IntPtr stride, int bytewidth);


            [DllImport("Codecs/Dlls/rav1e.dll")]
            public static extern Int32 rav1e_frame_extract_plane();

            [StructLayout(LayoutKind.Sequential)]
            public struct ConfigWrap
            {
                public Config config;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct Config
            {
                public EncoderConfig enc;
                public IntPtr rate_control;
                public IntPtr threads;
                public IntPtr pool;
                public IntPtr slots;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct EncoderConfig
            {
                public UIntPtr width;
                public UIntPtr height;
                public IntPtr sample_aspect_ratio;
                public IntPtr time_base;

                public IntPtr bit_depth;
                public IntPtr chroma_sampling;
                public IntPtr chroma_sample_position;

                /// Pixel value range.
                public IntPtr pixel_range;
                /// Content color description (primaries, transfer characteristics, matrix).
                public IntPtr? color_description;
                /// HDR mastering display parameters.
                public IntPtr? mastering_display;
                /// HDR content light parameters.
                public IntPtr? content_light;

                /// AV1 level index to target (0-31).
                /// If None, allow the encoder to decide.
                /// Currently, rav1e is unable to guarantee that the output bitstream
                /// meets the rate limitations of the specified level.
                public byte? level_idx;

                /// Enable signaling timing info in the bitstream.
                public byte enable_timing_info;

                /// Still picture mode flag.
                public byte still_picture;

                /// Flag to force all frames to be error resilient.
                public byte error_resilient;

                /// Interval between switch frames (0 to disable)
                public ulong switch_frame_interval;

                // encoder configuration
                /// The *minimum* interval between two keyframes
                public ulong min_key_frame_interval;
                /// The *maximum* interval between two keyframes
                public ulong max_key_frame_interval;
                /// The number of temporal units over which to distribute the reservoir
                /// usage.
                public int? reservoir_frame_delay; //maybe also intptr
                /// Flag to enable low latency mode.
                ///
                /// In this mode the frame reordering is disabled.
                public byte low_latency;
                /// The base quantizer to use.
                public IntPtr quantizer;
                /// The minimum allowed base quantizer to use in bitrate mode.
                public byte min_quantizer;
                /// The target bitrate for the bitrate mode.
                public int bitrate;
                /// Metric to tune the quality for.
                public IntPtr tune;
                /// Parameters for grain synthesis.
                public IntPtr? film_grain_params;
                /// Number of tiles horizontally. Must be a power of two.
                ///
                /// Overridden by [`tiles`], if present.
                ///
                /// [`tiles`]: #structfield.tiles
                public IntPtr tile_cols;
                /// Number of tiles vertically. Must be a power of two.
                ///
                /// Overridden by [`tiles`], if present.
                ///
                /// [`tiles`]: #structfield.tiles
                public IntPtr tile_rows;
                /// Total number of tiles desired.
                ///
                /// Encoder will try to optimally split to reach this number of tiles,
                /// rounded up. Overrides [`tile_cols`] and [`tile_rows`].
                ///
                /// [`tile_cols`]: #structfield.tile_cols
                /// [`tile_rows`]: #structfield.tile_rows
                public IntPtr tiles;

                /// Settings which affect the encoding speed vs. quality trade-off.
                public IntPtr speed_settings;
            }
        }
    }
}
