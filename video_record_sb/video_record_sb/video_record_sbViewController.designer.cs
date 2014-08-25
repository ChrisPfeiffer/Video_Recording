// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace video_record_sb
{
	[Register ("video_record_sbViewController")]
	partial class video_record_sbViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPlay { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRecord { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnPlay != null) {
				btnPlay.Dispose ();
				btnPlay = null;
			}
			if (btnRecord != null) {
				btnRecord.Dispose ();
				btnRecord = null;
			}
		}
	}
}
