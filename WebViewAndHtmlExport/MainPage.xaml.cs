using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Forms9Patch;

namespace WebViewAndHtmlExport
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            webView.Source = new HtmlWebViewSource
            {
                Html = @"
<!DOCTYPE html>
<html>
<body>

<h1>Convert to PNG</h1>

<p>This html will be converted to a PNG, PDF, or print.</p>

</body>
</html>
"
            };

            shareButton.Clicked += ShareButton_Clicked;
        }

        async void ShareButton_Clicked(object sender, EventArgs e)
        {
            if (await webView.ToPdfAsync("output.pdf") is ToFileResult pdfResult)
            {
                if (pdfResult.IsError)
                    using (Toast.Create("PDF Failure", pdfResult.Result)) { }
                else
                {
                    var collection = new Forms9Patch.MimeItemCollection();
                    collection.AddBytesFromFile("application/pdf", pdfResult.Result);
                    Forms9Patch.Sharing.Share(collection, shareButton);
                }
            }
        }
    }
}

