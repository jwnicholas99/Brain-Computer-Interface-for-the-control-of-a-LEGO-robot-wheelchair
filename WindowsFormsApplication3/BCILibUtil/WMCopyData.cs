using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

namespace BCILib.Util
{
	/// <summary>
	/// Summary description for WMCopyData.
	/// </summary>
	public class WMCopyData
	{
		delegate bool EnumWndCallBack(IntPtr hwnd, int lparam);

		[DllImport("User32.dll")]
		static extern bool EnumWindows(EnumWndCallBack cb, int lparam);

		[DllImport("User32.dll")]
		public static extern int GetProp(IntPtr hwnd, string param);

		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hwnd, uint msgID, 
			int wparam, int lparam);

		[DllImport("User32.dll")] 
		public static extern bool SetProp(IntPtr hwnd, string prop, int val);
		[DllImport("User32.dll")] 
		public static extern IntPtr RemoveProp(IntPtr hwnd, string prop);

		public const int WM_COPYDATA = 0x004A; // only for SendMessage
		private string WM_PROPERTY;

		private ArrayList cli_windows = new ArrayList();
		private int serv_window;

        public IntPtr ServerWindow
        {
            set
            {
                serv_window = value.ToInt32();
            }

            get
            {
                return new IntPtr(serv_window);
            }
        }

		/// <summary>
		/// WMCopyData: class hold functions to send end client window messages
		/// </summary>
		/// <param name="wm_prop"> a string specify the property name.</param>
		public WMCopyData(string wm_prop, IntPtr swnd) {
			WM_PROPERTY = wm_prop;
			serv_window = swnd.ToInt32();
		}

		public string Property {
			get {
				return WM_PROPERTY;
			}
		}

		private bool CheckClientWnd(IntPtr hwnd, int lparam) {
			int pv = GetProp(hwnd, WM_PROPERTY);
			if (pv > 0) {
				//Console.WriteLine("Find client window!");
				cli_windows.Add(hwnd);
			}
			return true;
		}

		public int GetAllGUIWindows() {
            return GetAllGUIWindows(true);
        }

        public IntPtr[] CliWnds
        {
            get
            {
                return (IntPtr[])cli_windows.ToArray(typeof(IntPtr));
            }
        }

        public int GetAllGUIWindows(bool DoEnum) {
            if (DoEnum)
            {
                cli_windows.Clear();
                EnumWindows(new EnumWndCallBack(CheckClientWnd), 0);
                //Console.WriteLine("Clients found: {0}", cli_windows.Count);
            }
			return cli_windows.Count;
		}

		private Copy_Data ctrData = new Copy_Data();

		public bool SendClient(int cmd, int msgdata, float fv) {
            IntPtr clw = CliWnd;
            if (clw == IntPtr.Zero) return false;

			// construct message sent to clients
			ctrData.wdata = ((uint) cmd & 0xff) | (((uint)msgdata & 0xff) << 16);
			ctrData.sz = 4;
			GCHandle gch_f = GCHandle.Alloc(fv, GCHandleType.Pinned);
			ctrData.pdata = gch_f.AddrOfPinnedObject();

			GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);

			bool rst = true;
			try {
				SendMessage(clw, WM_COPYDATA, serv_window, 
					gch_data.AddrOfPinnedObject().ToInt32());
			} catch (Exception e) {
				Console.WriteLine("Exception = {0}", e);
				rst = false;
			}

			gch_f.Free();
			gch_data.Free();

			return rst;
		}

        private IntPtr CliWnd
        {
            get
            {
                IntPtr clw = IntPtr.Zero;
                if (cli_windows.Count > 0)
                {
                    clw = (IntPtr)cli_windows[0];
                    while (GetProp(clw, WM_PROPERTY) == 0)
                    {
                        cli_windows.Remove(clw);
                        if (cli_windows.Count < 1)
                        {
                            return IntPtr.Zero;
                        }
                        clw = (IntPtr)cli_windows[0];
                    }
                }

                if (clw == IntPtr.Zero) {
                    if (GetAllGUIWindows() > 0) {
                        clw = (IntPtr)cli_windows[0];
                    }
                }

                return clw;
            }
        }

		public bool SendClient(int cmd, int msgdata) {
            IntPtr clw = CliWnd;
            if (clw == IntPtr.Zero) return false;

			// construct message sent to clients
			ctrData.wdata = ((uint) cmd & 0xff) | (((uint)msgdata & 0xff) << 16);
			ctrData.sz = 0;
			ctrData.pdata = IntPtr.Zero;

			GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);

			bool rst = true;
			try {
				SendMessage(clw, WM_COPYDATA, serv_window, 
					gch_data.AddrOfPinnedObject().ToInt32());
			} catch (Exception e) {
				Console.WriteLine("Exception = {0}", e);
				rst = false;
			}

			gch_data.Free();

			return rst;
		}

		private const uint DATATYPE_INT = 1;
		private const uint DATATYPE_DOUBLE = 2;
		private const uint DATATYPE_STRING = 3;

		// variables length of double values
		public bool SendClient(int cmd, params double[] vals) {
            IntPtr clw = CliWnd;
            if (clw == IntPtr.Zero) return false;

            // check if window exists
            

			// construct message sent to clients
			ctrData.wdata = (uint) cmd & 0xff;
			ctrData.sz = 0;
			ctrData.pdata = IntPtr.Zero;

			bool rst = true;
			int nv = vals.Length;
			if (nv == 0) {
				GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);
				try {
					SendMessage(clw, WM_COPYDATA, serv_window, 
						gch_data.AddrOfPinnedObject().ToInt32());
				} catch (Exception e) {
					Console.WriteLine("Exception = {0}", e);
					rst = false;
				}
				gch_data.Free();
			} else {
				ctrData.wdata |= DATATYPE_DOUBLE << 16;
				GCHandle gch_f = GCHandle.Alloc(vals, GCHandleType.Pinned);
				ctrData.sz = Marshal.SizeOf(typeof(double)) * nv;
				ctrData.pdata = gch_f.AddrOfPinnedObject();
				GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);

                int rv = 0;
				try {
					rv = SendMessage(clw, WM_COPYDATA, serv_window, 
						gch_data.AddrOfPinnedObject().ToInt32());
				} catch (Exception e) {
					Console.WriteLine("Exception = {0}", e);
					rst = false;
				}

				gch_f.Free();
				gch_data.Free();
			}

			return rst;
		}

		/// <summary>
		/// Send command with string data
		/// </summary>
		/// <param name="cmd">command</param>
		/// <param name="strdata">string data</param>
		/// <returns>true indicates sucess</returns>
		public bool SendClient(int cmd, string strdata) {
            IntPtr clw = CliWnd;
            if (clw == IntPtr.Zero) return false;

			// construct message sent to clients
			ctrData.wdata = (uint) cmd & 0xffff;

			bool rst = true;
			ctrData.wdata |= DATATYPE_STRING << 16;

			int len = strdata.Length;
			byte[] str_bytes = null;
			if (len == 4) { // conflict with float value
				str_bytes = new byte[len + 1];
				System.Text.ASCIIEncoding.ASCII.GetBytes(strdata, 0, len, str_bytes, 0);
				str_bytes[len] = 0;
				len++;
			} else {
				str_bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strdata);
			}

			GCHandle gch_str = GCHandle.Alloc(str_bytes, GCHandleType.Pinned);
			ctrData.sz = Marshal.SizeOf(typeof(byte)) * str_bytes.Length;
			ctrData.pdata = gch_str.AddrOfPinnedObject();
			GCHandle gch_data = GCHandle.Alloc(ctrData, GCHandleType.Pinned);

			try {
				SendMessage(clw, WM_COPYDATA, serv_window, gch_data.AddrOfPinnedObject().ToInt32());
			} catch (Exception e) {
				Console.WriteLine("Exception = {0}", e);
                // remove the window that are not working
                cli_windows.Remove(clw);
				rst = false;
			}

			gch_str.Free();
			gch_data.Free();

			return rst;
		}

        //public bool SendCmdString(string fmt, params object[] args)
        //{
        //    return SendClient(BCILib.App.GameCommand.SendCmdString, string.Format(fmt, args));
        //}

		public static ctrl_msg TranslateMessage(System.Windows.Forms.Message m)
		{
			Copy_Data mdata = (Copy_Data) m.GetLParam(typeof(Copy_Data));
			ctrl_msg cmsg = new ctrl_msg();

			cmsg.cmd = (int) (mdata.wdata & 0xffff);
			cmsg.msg = (int) (mdata.wdata >> 16);
			cmsg.fv = 0;
			cmsg.dva = null;

			if (mdata.sz == 4) {// old use
				float[] fdata = new float[1];
				Marshal.Copy(mdata.pdata, fdata, 0, 1);
				cmsg.fv = fdata[0];
			} else if (mdata.sz > 0 && cmsg.msg == DATATYPE_DOUBLE) { // new use
				int nv = mdata.sz / Marshal.SizeOf(typeof (double));
				cmsg.dva = new double[nv];
				Marshal.Copy(mdata.pdata, cmsg.dva, 0, nv);
			} else if (mdata.sz > 0 && cmsg.msg == DATATYPE_STRING) {
				byte[] buf = new byte[mdata.sz];
				Marshal.Copy(mdata.pdata, buf, 0, mdata.sz);
				int len = mdata.sz;
				if (buf[len - 1] == 0) len--;
				cmsg.strdata = System.Text.Encoding.ASCII.GetString(buf, 0, len);
			}

			return cmsg;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Copy_Data {
		public uint wdata;
		public int sz;
		public IntPtr pdata;
	}

	public struct ctrl_msg {
		public int cmd;
		public int msg;
		public float fv;
		public double[] dva;
		public string strdata;
	}
}
