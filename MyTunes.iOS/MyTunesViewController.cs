using System.Linq;
using UIKit;

namespace MyTunes
{
	public class MyTunesViewController : UITableViewController
	{
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			TableView.ContentInset = new UIEdgeInsets (20, 0, 0, 0);
		}

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            ////TableView.Source = new ViewControllerSource<string>(TableView) {
            ////	DataSource = new string[] { "One", "Two", "Three" },
            ////};
            var songs = await SongLoader.Load();

            ViewControllerSource<Song> viewSourceController = new ViewControllerSource<Song>(TableView)
            {
                DataSource = songs.ToList(),
                TextProc = (a) => a.Name,
                DetailTextProc = (a) => a.Artist + " - " + a.Album
            };
            TableView.Source = viewSourceController;
        }
	}

}

