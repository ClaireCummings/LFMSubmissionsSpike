using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using LFM.ApplicationServices;
using LFM.ApplicationServices.LandRegistry;
using LFM.Infrastructure.LandRegistry;
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

            var result = submissionService.Submit("LRUser001","BGPassword001",package);
            applicationIdTextBox.Text = result.Command.ApplicationId;

            MessageBox.Show(string.Format("Package Submitted{0}ApplicationId: {1}", Environment.NewLine ,result.ToString()), "Success", MessageBoxButton.OK);
        }

        private void getStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var queryInvoker = new QueryInvoker(new SubmissionService(new Lrap1SubmissionRepository()));
            var result = queryInvoker.Query<Lrap1StatusQuery,Lrap1StatusQueryResult>(new Lrap1StatusQuery() {ApplicationId = applicationIdTextBox.Text});
            MessageBox.Show("Application Status: " + result.ResponseType.ToString(), "Status", MessageBoxButton.OK);
        }
    }
}
