﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NowMine
{
    class SearchPanel
    {
        //List<MusicPiece> resultsList;
        YouTubeProvider youtubeProvider = new YouTubeProvider();
        StackPanel stackPanel;
        TextBox textBox;
        QueuePanel queuePanel;

        public SearchPanel(StackPanel stackPanel, TextBox textBox, QueuePanel queuePanel)
        {
            this.stackPanel = stackPanel;
            this.textBox = textBox;
            this.queuePanel = queuePanel;
        }

        public void search()
        {
            if (textBox.Text == "")
            {
                return;
            }
            List<MusicPiece> list;
            list = getSearchList(textBox.Text);
            populateSearchBoard(list);
        }

        private void populateSearchBoard(List<MusicPiece> results)
        {
            stackPanel.Children.Clear();
            foreach (MusicPiece result in results)
            {
                result.MouseDoubleClick += SearchResult_MouseDoubleClick;
                stackPanel.Children.Add(result);
            }
        }

        private void SearchResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var musicPiece = (MusicPiece)sender;
            var queueMusicPiece = musicPiece.copy();
            queuePanel.addToQueue(queueMusicPiece);
        }

        public List<MusicPiece> getSearchList(String searchWord)
        {
            List<MusicPiece> list;
            List<YouTubeInfo> infoList = youtubeProvider.LoadVideosKey(searchWord);
            //this.resultsList = infoToResults(infoList);
            list = infoToResults(infoList);
            return list;
            //PopulateSearchBoard(this.resultsList);
        }

        private List<MusicPiece> infoToResults(List<YouTubeInfo> infoList)
        {
            List<MusicPiece> list = new List<MusicPiece>();
            foreach (YouTubeInfo info in infoList)
            {
                MusicPiece result = new MusicPiece(info);
                list.Add(result);
            }
            return list;
        }

        //private void PopulateSearchBoard(List<MusicPiece> results)
        //{
        //    MainWindow.searchBoard.Children.Clear();
        //    for (int i = 0; i < infos.Count; i++)
        //    {
        //        ListObject control = new ListObject { Info = infos[i] };
        //        //int angleMutiplier = i % 2 == 0 ? 1 : -1;
        //        //control.RenderTransform = new RotateTransform { Angle = GetRandom(30, angleMutiplier) };
        //        //control.SetValue(Canvas.LeftProperty, GetRandomDist(dragCanvas.ActualWidth - 150.0));
        //        //control.SetValue(Canvas.TopProperty, GetRandomDist(dragCanvas.ActualHeight - 150.0));
        //        //control.SelectedEvent += control_SelectedEvent;
        //        searchPanel.Children.Add(control);
        //    }
        //}

        //private void txtSearchBar_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        if (txtSearchBar.Text != string.Empty)
        //        {
        //            List<YouTubeInfo> infos = YouTubeProvider.LoadVideosKey(txtSearchBar.Text);
        //            PopulateSearchList(infos);
        //        }
        //        else
        //        {
        //            MessageBox.Show("you need to enter a search word", "Error",
        //                MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
    }
}
