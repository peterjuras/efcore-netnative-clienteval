using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Microsoft.EntityFrameworkCore;

namespace EmptyQueryNativeTest
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            using (Context context = new Context())
            {
                context.Database.Migrate();

                context.Models.RemoveRange(context.Models.ToArray());
                context.Children.RemoveRange(context.Children.ToArray());

                for (int i = 0; i < 100; i++)
                {
                    Model model = new Model
                    {
                        SomeString = $"Model {i/10}"
                    };
                    model.AddChild(new Child { SomeString = "Something"} );
                    model.AddChild(new Child { SomeString = "Something"} );

                    context.Models.Add(model);
                }

                context.SaveChanges();
            }
        }

        private void SimpleFetchAll(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (Context context = new Context())
            {
                context.GetModels(null);
            }
            stopwatch.Stop();
            Debug.WriteLine($"Fetching all took {stopwatch.ElapsedMilliseconds}ms");
        }

        private void SearchWithResults(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (Context context = new Context())
            {
                context.GetModels("5");
            }
            stopwatch.Stop();
            Debug.WriteLine($"Search with results took {stopwatch.ElapsedMilliseconds}ms");
        }

        private void SearchWithoutResults(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (Context context = new Context())
            {
                context.GetModels("ASDFKJASDFADSFJ");
            }
            stopwatch.Stop();
            Debug.WriteLine($"Search without results took {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
