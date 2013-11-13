using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using LFM.LandRegistry.SubmissionsService;
using LFM.MessageService;

namespace LFM.Submissions.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void submitLRAP1Button_Click(object sender, RoutedEventArgs e)
        {
            var submissionService = new Lrap1SubmissionService(new MessageSender(App.Bus));
            
            var attachments = new List<Lrap1Attachment>
            {
                new Lrap1Attachment() {Payload = File.ReadAllText(@"C:\GIT\LFMSubmissionsSpike\LFMSubmissions\LFM.Submissions.Client\TestXML\Attachment Service Test 8 XmlRequest.xml")},
                new Lrap1Attachment() {Payload = File.ReadAllText(@"C:\GIT\LFMSubmissionsSpike\LFMSubmissions\LFM.Submissions.Client\TestXML\Attachment Service Test 8 XmlRequest.xml")}
            };

            var package = new Lrap1Package()
            {
                Payload = File.ReadAllText(@"C:\GIT\LFMSubmissionsSpike\LFMSubmissions\LFM.Submissions.Client\TestXML\eDRS Test 4 XmlRequest.xml"),
                Attachments = attachments
            };

            submissionService.Submit("username","password",package);
            MessageBox.Show("Package Submitted", "Success",MessageBoxButton.OK);
        }

      
    }
}
