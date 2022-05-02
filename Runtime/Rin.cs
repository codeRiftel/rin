using System.Text;
using System.Globalization;

namespace rin {
    public class Rin {
        private StringBuilder builder;
        private const string what = "\"\\/\b\f\n\r\t";

        public Rin() {
            builder = new StringBuilder();
        }

        public string Esc(string src) {
            var bad = false;
            for (int i = 0; i < what.Length; i++) {
                bad = src.IndexOf(what[i]) >= 0;
                if (bad) break;
            }

            if (!bad) return src;

            builder.Clear();
            for (int i = 0; i < src.Length; i++) {
                var escape = true;
                var target = src[i];
                switch (src[i]) {
                    case '"':
                    case '\\':
                    case '/':
                        break;
                    case '\b':
                        target = 'b';
                        break;
                    case '\f':
                        target = 'f';
                        break;
                    case '\n':
                        target = 'n';
                        break;
                    case '\r':
                        target = 'r';
                        break;
                    case '\t':
                        target = 't';
                        break;
                    default:
                        escape = false;
                        target = src[i];
                        break;
                }

                if (escape) builder.Append('\\');
                builder.Append(target);
            }

            return builder.ToString();
        }

        public string Un(string src) {
            builder.Clear();
            for (int i = 0; i < src.Length; i++) {
                if (src[i] != '\\' || i == src.Length - 1) builder.Append(src[i]); else {
                    i++;
                    switch (src[i]) {
                        case '"':
                            builder.Append('"');
                            break;
                        case '\\':
                            builder.Append('\\');
                            break;
                        case '/':
                            builder.Append('/');
                            break;
                        case 'b':
                            builder.Append('\b');
                            break;
                        case 'f':
                            builder.Append('\f');
                            break;
                        case 'n':
                            builder.Append('\n');
                            break;
                        case 'r':
                            builder.Append('\r');
                            break;
                        case 't':
                            builder.Append('\t');
                            break;
                        default:
                            builder.Append('\\');
                            builder.Append(src[i]);
                            break;
                    }
                }
            }

            return builder.ToString();
        }

        public static int? Int(string num) {
            if (int.TryParse(num, out int res)) return res;
            return null;
        }

        public static uint? Uint(string num) {
            if (uint.TryParse(num, out uint res)) return res;
            return null;
        }

        public static float? Float(string num) {
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
            style |= NumberStyles.AllowLeadingSign;

            if (float.TryParse(num, style, CultureInfo.InvariantCulture, out float r)) return r;
            return null;
        }

        public static double? Double(string num) {
            var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
            style |= NumberStyles.AllowLeadingSign;

            if (double.TryParse(num, style, CultureInfo.InvariantCulture, out double r)) return r;
            return null;
        }

        public static string To(float num) {
            return num.ToString(CultureInfo.InvariantCulture);
        }

        public static string To(double num) {
            return num.ToString(CultureInfo.InvariantCulture);
        }

        public static string To(int num) {
            return num.ToString(CultureInfo.InvariantCulture);
        }

        public static string To(uint num) {
            return num.ToString(CultureInfo.InvariantCulture);
        }

        public static string To(bool b) {
            if (b) return "true"; else return "false";
        }
    }
}
