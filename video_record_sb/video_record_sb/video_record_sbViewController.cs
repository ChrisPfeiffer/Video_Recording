using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreVideo;
using MonoTouch.CoreMedia;
using MonoTouch.AVFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreFoundation;
using System.Runtime.InteropServices;

namespace video_record_sb
{
	public partial class video_record_sbViewController : UIViewController
	{

		public video_record_sbViewController (IntPtr handle) : base (handle)
		{

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}



		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		protected override void Dispose(bool disposing)
		{
			Console.WriteLine (String.Format ("{0} controller disposed - {1}", this.GetType (), this.GetHashCode ()));

			base.Dispose (disposing);
		}

		#endregion
	}
}

