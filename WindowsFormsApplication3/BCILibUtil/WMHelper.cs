using System;
using System.Windows.Forms;
using System.IO;
using BCILib.App;
using System.Runtime.InteropServices;


namespace BCILib.Util
{
    public delegate void WMCopyData_Process(ctrl_msg cmd);

    /// <summary>
	/// Summary description for WMHelper.
	/// </summary>
	public class WMHelper
	{
        static DummyWnd _dummyWnd = new DummyWnd();

        static WMCopyData _copyData = null;
        const string ID_BrainpalGameCtrl = "BrainpalGameCtrl";

        static WMHelper()
        {
            _copyData = new WMCopyData(ID_BrainpalGameCtrl, IntPtr.Zero);
        }

        public static void Register(IntPtr hwnd, string prop)
        {
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidDataException("Window not been initialized.");
            }
            _dummyWnd.Activate(hwnd, prop);

            _copyData.ServerWindow = hwnd;
        }

        public static void Deregister()
        {
            _dummyWnd.Deactivate();
        }

        public static void SetRecvCmdHandler(WMCopyData_Process proc)
        {
            _dummyWnd.OnWMCopyData = proc;
        }

        public static void ReportEventMsg(string msg)
        {
            _copyData.SendClient(GameCommand.CMD_SENDMESSAGE, msg);
        }

        public static void SendAckowledgement()
        {
            _copyData.SendClient(GameCommand.StartGame, 1);
        }

        public static string GetXMLQuestions()
        {
            string xml_fn = "questions.xml";
            string wpath = Directory.GetCurrentDirectory();

            if (!File.Exists(xml_fn)) {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.RestoreDirectory = true;
                dlg.FileName = xml_fn;
                if (dlg.ShowDialog() != DialogResult.OK) {
                    return null;
                }
                xml_fn = dlg.FileName;
            }

            StreamReader sr = new StreamReader(xml_fn);
            string xml_data = sr.ReadToEnd();
            sr.Close();
            return xml_data;
        }

        public static void LogStatisticsData(string fmt, params object[] args)
        {
            _copyData.SendClient(GameCommand.CMD_SENDGAMEDAT, string.Format(fmt, args));
        }

        public static void TimerStart()
        {
            _copyData.SendClient(GameCommand.Timer_Start);
        }

        public static void TimerPause()
        {
            _copyData.SendClient(GameCommand.Timer_Pause);
        }

        public static void TimerResume()
        {
            _copyData.SendClient(GameCommand.Timer_Resume);
        }

        public static void TimerStop()
        {
            _copyData.SendClient(GameCommand.Timer_Stop);
        }

        public static TimeSpan TimerGetElaspedTime()
        {
            // how to implement?
            IntPtr[] cwnds = _copyData.CliWnds;
            if (cwnds == null || cwnds.Length == 0) {
                _copyData.GetAllGUIWindows();
                cwnds = _copyData.CliWnds;
                if (cwnds == null || cwnds.Length == 0) {
                    return new TimeSpan(0);
                }
            }

            Copy_Data ctrData = new Copy_Data();

            ctrData.wdata = GameCommand.Timer_Elased;
            ctrData.sz = 0;
            ctrData.pdata = IntPtr.Zero;

            GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);

            int elapsed = 0;
            try {
                elapsed = WMCopyData.SendMessage(cwnds[0], WMCopyData.WM_COPYDATA, 0,
                    gch_data.AddrOfPinnedObject().ToInt32());
            }
            catch (Exception e) {
                Console.WriteLine("Exception = {0}", e);
            }

            gch_data.Free();

            TimeSpan ts = new TimeSpan(elapsed * TimeSpan.TicksPerMillisecond);
            return ts;
        }

        public static void SetGameLevel(int gl)
        {
            _copyData.SendClient(GameCommand.Game_Level, gl);
        }
    }

    internal class DummyWnd: NativeWindow
    {
		string _wmprop;

		public void Activate(IntPtr hwnd, string prop) {
			_wmprop = prop;
			WMCopyData.SetProp(hwnd, prop, 1);
            ReleaseHandle();
			AssignHandle(hwnd);
		}

		public WMCopyData_Process OnWMCopyData;

		protected override void WndProc(ref Message m) {
			if (m.Msg == WMCopyData.WM_COPYDATA) {
                //_copyData.SetClientWnd(m.WParam);

                ctrl_msg msg = WMCopyData.TranslateMessage(m);

				if (OnWMCopyData != null) {
					OnWMCopyData(msg);
                    m.Result = new IntPtr(2);
				}
                else m.Result = new IntPtr(1);
			} else {
				base.WndProc(ref m);
			}
		}

		public void Deactivate() {
            WMCopyData.RemoveProp(this.Handle, _wmprop);
			ReleaseHandle();
		}

        ~DummyWnd()
        {
			Deactivate();
		}
	}
}
