﻿using System;
using System.Collections.Generic;
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
using Awesomium;
using Awesomium.Windows.Controls;
using Awesomium.Core;

namespace NowMine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SearchPanel searchPanel;
        QueuePanel queuePanel;
        WebPanel webPanel;

        public MainWindow()
        {
            InitializeComponent();
            webPanel = new WebPanel(webPlayer);
            queuePanel = new QueuePanel(queueBoard, webPanel);
            searchPanel = new SearchPanel(searchBoard, txtSearch, queuePanel);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            searchPanel.search();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchPanel.search();
            }
        }

        private void webPlayer_DocumentReady(object sender, DocumentReadyEventArgs e)
        {
            BindMethods(webPlayer);
            webPlayer.ExecuteJavascript("myMethod('chujufsto')");
        }

        private void BindMethods (IWebView _webView)
        {
            JSValue result = webPlayer.CreateGlobalJavascriptObject("app");
            if (result.IsObject)
            {
                JSObject appObject = result;
                appObject.Bind("sayHello", sayHello);
            }
        }

        private JSValue sayHello(object sender, JavascriptMethodEventArgs e)
        {
            return "Hello!";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            webPlayer.ExecuteJavascript("myMethod('chujufsto')");


        }
    }
}
