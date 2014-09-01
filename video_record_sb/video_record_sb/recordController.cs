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
	partial class recordController : UIViewController
	{
		Boolean weAreRecording;
		AVCaptureMovieFileOutput output;
		AVCaptureDevice device;
		AVCaptureDevice audioDevice;

		AVCaptureDeviceInput input;
		AVCaptureDeviceInput audioInput;
		AVCaptureSession session;

		AVCaptureVideoPreviewLayer previewlayer;



		public recordController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			weAreRecording = false;
			lblError.Hidden = true;

			btnStartRecording.SetTitle("Start Recording", UIControlState.Normal);

			//Set up session
			session = new AVCaptureSession ();


			//Set up inputs and add them to the session
			//this will only work if using a physical device!

			Console.WriteLine ("getting device inputs");
			try{
				//add video capture device
				device = AVCaptureDevice.DefaultDeviceWithMediaType (AVMediaType.Video);
				input = AVCaptureDeviceInput.FromDevice (device);
				session.AddInput (input);

				//add audio capture device
				audioDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Audio);
				audioInput = AVCaptureDeviceInput.FromDevice(audioDevice);
				session.AddInput(audioInput);
			
			}
			catch(Exception ex){
				//show the label error.  This will always show when running in simulator instead of physical device.
				lblError.Hidden = false;
				return;
			}




			//Set up preview layer (shows what the input device sees)
			Console.WriteLine ("setting up preview layer");
			previewlayer = new AVCaptureVideoPreviewLayer (session);
			previewlayer.Frame = this.View.Bounds;
		
			//this code makes UI controls sit on top of the preview layer!  Allows you to just place the controls in interface builder
			UIView cameraView = new UIView ();
			cameraView = new UIView ();
			cameraView.Layer.AddSublayer (previewlayer);
			this.View.AddSubview (cameraView);
			this.View.SendSubviewToBack (cameraView);

			Console.WriteLine ("Configuring output");
			output = new AVCaptureMovieFileOutput ();

			long totalSeconds = 10000;
			Int32 preferredTimeScale = 30;
			CMTime maxDuration = new CMTime (totalSeconds, preferredTimeScale);
			output.MinFreeDiskSpaceLimit = 1024 * 1024;
			output.MaxRecordedDuration = maxDuration;

			if (session.CanAddOutput (output)) {
				session.AddOutput (output);
			}

			session.SessionPreset = AVCaptureSession.PresetMedium;

			Console.WriteLine ("About to start running session");

			session.StartRunning ();

			//toggle recording button was pushed.
			btnStartRecording.TouchUpInside += startStopPushed;


			//Console.ReadLine ();

		}


		void startStopPushed(object sender, EventArgs ea)
		{

			if (!weAreRecording) {

				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var library = System.IO.Path.Combine (documents, "..", "Library");
				var urlpath = System.IO.Path.Combine (library, "sweetMovieFilm.mov");

				NSUrl url = new NSUrl (urlpath, false);

				NSFileManager manager = new NSFileManager ();
				NSError error = new NSError ();

				if (manager.FileExists (urlpath)) {
					Console.WriteLine ("Deleting File");
					manager.Remove (urlpath, out error);
					Console.WriteLine ("Deleted File");
				}

				AVCaptureFileOutputRecordingDelegate avDel= new AVCaptureFileOutputRecordingDelegate ();
				output.StartRecordingToOutputFile(url, avDel);
				Console.WriteLine (urlpath);
				weAreRecording = true;

				btnStartRecording.SetTitle("Stop Recording", UIControlState.Normal);
			}
			//we were already recording.  Stop recording
			else {

				output.StopRecording ();

				Console.WriteLine ("stopped recording");

				weAreRecording = false;

				btnStartRecording.SetTitle("Start Recording", UIControlState.Normal);

			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			session.StopRunning ();
			this.btnStartRecording.TouchUpInside -= startStopPushed;

			foreach (var view in this.View.Subviews) {
			
				view.RemoveFromSuperview ();
			}


			base.ViewWillDisappear (animated);
		}

		protected override void Dispose(bool disposing)
		{
			Console.WriteLine (String.Format ("{0} controller disposed - {1}", this.GetType (), this.GetHashCode ()));

			base.Dispose (disposing);
		}
	}
}
