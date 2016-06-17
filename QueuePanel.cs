﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NowMine
{
    class QueuePanel
    {
        public List<MusicPiece> queue { get; }
        public List<MusicPiece> history { get; }
        StackPanel stackPanel;
        WebPanel webPanel;

        public QueuePanel(StackPanel stackPanel, WebPanel webPanel)
        {
            this.stackPanel = stackPanel;
            this.webPanel = webPanel;
            queue = new List<MusicPiece>();
            history = new List<MusicPiece>();
        }

        public void addToQueue(MusicPiece musicPiece)
        {
            if (queue.Count == 0)
            {
                musicPiece.nowPlayingVisual();
                webPanel.playNow(musicPiece.Info.Id);
            }
            queue.Add(musicPiece);
            populateQueueBoard();
        }

        public void populateQueueBoard()
        {
            stackPanel.Children.Clear();
            foreach (MusicPiece result in queue)
            {
                result.MouseDoubleClick += Queue_DoubleClick;
                stackPanel.Children.Add(result);
            }
        }


        private void Queue_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var musicPiece = (MusicPiece)sender;
            deleteFromQueue(musicPiece);
            musicPiece.nowPlayingVisual();
            toHistory(nowPlaying());
            queue.Insert(0, musicPiece);
            webPanel.playNow(musicPiece.Info.Id);
            populateQueueBoard();
        }

        public void deleteFromQueue(MusicPiece musicPiece)
        {
            if (queue.Contains(musicPiece))
            {
                queue.Remove(musicPiece);
            }
        }

        public void toHistory(MusicPiece musicPiece)
        {
            deleteFromQueue(musicPiece);
            musicPiece.historyVisual();
            history.Add(musicPiece);
        }

        public MusicPiece getNextVideo()
        {
            //deleteFromQueue(nowPlaying());
            //return nowPlaying();
            if (queue.Count > 2)
            {
                return queue.ElementAt(1);
            }
            return null;
        }

        public bool playNext()
        {
            MusicPiece nextVideo = getNextVideo();
            nextVideo.nowPlayingVisual();
            toHistory(nowPlaying());
            webPanel.playNow(nextVideo.Info.Id);
            //deleteFromQueue(nowPlaying());
            return true;
        }

        public MusicPiece nowPlaying()
        {
            return queue.ElementAt(0);
        }
    }
}
