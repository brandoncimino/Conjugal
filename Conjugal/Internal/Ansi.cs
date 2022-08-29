using System.Diagnostics.CodeAnalysis;

namespace FowlFever.Conjugal.Internal;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
internal static class Ansi {
    private const string _fg        = "3";
    private const string _bg        = "4";
    private const string _bright_fg = "9";
    private const string _bright_bg = "10";
    private const string csi        = "\x1b[";

    public static class Reset {
        public const string All = $"{csi}m";
        public const string Fg  = $"{csi}39m";
        public const string Bg  = $"{csi}49m";
    }

    #region Colors

    public static class Black {
        private const string Base = "0m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Gray {
        public const string Fg = Black.Bright.Fg;
        public const string Bg = Black.Bright.Bg;

        public static class Dark {
            public const string Fg = Black.Fg;
            public const string Bg = Black.Bg;
        }
    }

    public static class Red {
        private const string Base = "1m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Green {
        private const string Base = "2m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Yellow {
        private const string Base = "3m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Blue {
        private const string Base = "4m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Magenta {
        private const string Base = "5m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class Cyan {
        private const string Base = "6m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    public static class White {
        private const string Base = "7m";
        public const  string Fg   = $"{csi}{_fg}{Base}";
        public const  string Bg   = $"{csi}{_bg}{Base}";

        public static class Bright {
            public const string Fg = $"{csi}{_bright_fg}{Base}";
            public const string Bg = $"{csi}{_bright_bg}{Base}";
        }
    }

    #endregion

    #region Effects

    public static class Bold {
        public const string On  = $"{csi}1m";
        public const string Off = $"{csi}22m";
    }

    #endregion
}