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
	[Register ("playController")]
	partial class playController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView movieController { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (movieController != null) {
				movieController.Dispose ();
				movieController = null;
			}
		}
	}
}
