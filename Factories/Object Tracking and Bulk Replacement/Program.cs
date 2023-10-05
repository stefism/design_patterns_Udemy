using System.Text;

namespace DesignPatterns
{
    public interface ITheme
    {
        string TextColor { get; }
        string BackgroundColor { get; }
    }

    class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BackgroundColor => "white";

    }

    class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BackgroundColor => "dark gray";

    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in themes)
                {
                    if(reference.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light").AppendLine(" theme");
                    }
                }

                return sb.ToString();
            }
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> themes = new();

        private ITheme CreateThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var r = new Ref<ITheme>(CreateThemeImpl(dark));
            themes.Add(new WeakReference<Ref<ITheme>>(r));
            //themes.Add(new(r)); - Съкратен запис на горното.

            return r;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach(var weakReference in themes)
            {
                if(weakReference.TryGetTarget(out var reference))
                {
                    reference.Value = CreateThemeImpl(dark);
                }
            }
        }
    }

    public class Ref<T> where T : class
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            var factory = new TrackingThemeFactory();
            factory.CreateTheme(false);
            factory.CreateTheme(true);
            Console.WriteLine(factory.Info);

            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.Value.BackgroundColor);
            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.Value.BackgroundColor);

        }
    }
}