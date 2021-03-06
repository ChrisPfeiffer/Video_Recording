using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

using MonoTouch.CoreVideo;
using MonoTouch.CoreMedia;
using MonoTouch.AVFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreFoundation;
using System.Runtime.InteropServices;


namespace video_record_sb
{
	partial class playController : UIViewController
	{
		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;

		public playController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			//largely taken from the xamarin website videoplayer tutorial

		

			//build the path to the location where the movie was saved
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var library = System.IO.Path.Combine (documents, "..", "Library");
			var urlpath = System.IO.Path.Combine (library, "sweetMovieFilm.mov");

			NSUrl url = new NSUrl (urlpath, false);

			_asset = AVAsset.FromUrl(url);
			_playerItem = new AVPlayerItem (_asset);
			_player = new AVPlayer (_playerItem);

			_playerLayer = AVPlayerLayer.FromPlayer (_player);
			_playerLayer.Frame = View.Frame;
			View.Layer.AddSublayer (_playerLayer);

			//this code makes UI controls sit on top of the movie layer.  Allows you to just place the controls in interface builder
			UIView cameraView = new UIView ();
			this.View.AddSubview (cameraView);
			this.View.SendSubviewToBack (cameraView);
			cameraView.Layer.AddSublayer (_playerLayer);

			_player.Play ();

			this.btnDonePlaying.TouchUpInside += stopPlaying;


		}

		private void stopPlaying(Object sender, EventArgs ea)
		{
			_player.Pause ();
			_playerLayer.RemoveFromSuperLayer ();

			//remove subviews
			foreach (var view in this.View.Subviews) {

				view.RemoveFromSuperview ();
			}

			this.btnDonePlaying.TouchUpInside -= stopPlaying;

		}
	}
}
