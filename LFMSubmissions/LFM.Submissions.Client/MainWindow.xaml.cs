using System.Collections.Generic;
using System.IO;
using System.Windows;
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
                new Lrap1Attachment() {Payload = File.ReadAllText("./TestXMLData/Attachment Service Test 1 XmlRequest.xml")},
                new Lrap1Attachment() {Payload = File.ReadAllText("./TestXMLData/Attachment Service Test 1 XmlRequest.xml")}
            };

            var package = new Lrap1Package()
            {
                Payload = File.ReadAllText("./TestXMLData/eDRS Test 4 XmlRequest.xml"),
                Attachments = attachments
            };

            submissionService.Submit("LRUser001","BGPassword001",package);
            MessageBox.Show("Package Submitted", "Success",MessageBoxButton.OK);
        }

      
    }
}
