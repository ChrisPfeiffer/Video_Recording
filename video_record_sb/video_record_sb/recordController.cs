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
		public recordController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			weAreRecording = false;
			lblError.Hidden = true;

			btnStartRecording.SetTitle("Start Recording", UIControlState.Normal);

			//assign eventhandler to record toggler


			//Set up session
			AVCaptureSession session = new AVCaptureSession ();


			//Set up inputs and add them to the session
			//this will only work if using a physical device!
			AVCaptureDevice device;
			AVCaptureDeviceInput input;
			try{
			device = AVCaptureDevice.DefaultDeviceWithMediaType (AVMediaType.Video);
			input = AVCaptureDeviceInput.FromDevice (device);
				session.AddInput (input);}
			catch(Exception ex){
				lblError.Hidden = false;
				return;
			}


			//Set up preview layer (shows what the input device sees)
			AVCaptureVideoPreviewLayer previewlayer = new AVCaptureVideoPreviewLayer (session);
			UIView myview = this.View;
			previewlayer.Frame = myview.Bounds;
			this.View.Layer.AddSublayer (previewlayer);
			//this code makes UI controls sit on top of the preview layer!  Allows you to just place the controls in interface builder
			UIView cameraView = new UIView ();
			this.View.AddSubview (cameraView);
			this.View.SendSubviewToBack (cameraView);
			cameraView.Layer.AddSublayer (previewlayer);

			//add output
			//AVCaptureVideoDataOutput output = new AVCaptureVideoDataOutput ();
			output = new AVCaptureMovieFileOutput ();

			long totalSeconds = 60;
			Int32 preferredTimeScale = 30;
			CMTime maxDuration = new CMTime (totalSeconds, preferredTimeScale);
			output.MinFreeDiskSpaceLimit = 1024 * 1024;
			output.MaxRecordedDuration = maxDuration;

			if (session.CanAddOutput (output)) {
				session.AddOutput (output);
			}

			session.SessionPreset = AVCaptureSession.PresetMedium;

			session.StartRunning ();

			//toggle recording button was pushed.
			btnStartRecording.TouchUpInside += startStopPushed;


			Console.ReadLine ();

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
					manager.Remove (urlpath, out error);
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
	}
}
