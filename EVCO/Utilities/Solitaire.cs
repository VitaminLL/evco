using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace EVCO
{
	public class Solitaire
	{
		public Solitaire ()
		{
		}

		public static int Run(int[] values)
		{
			StringBuilder s = new StringBuilder ();
			foreach (int n in values) {
				s.Append (n.ToString () + " ");
			}

			Process p = new Process ();
			p.StartInfo.Arguments = s.ToString ().Trim();
			p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
			//p.StartInfo.FileName = "macMsolitare.exe";
			p.StartInfo.FileName = "./linuxLiteMsolitare.exe";
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.UseShellExecute = false;
			p.Start();

			string lastLine = String.Empty;
			while (!p.StandardOutput.EndOfStream)
				lastLine = p.StandardOutput.ReadLine ();

			p.WaitForExit();
			p.Close ();
			p.Dispose ();

			try {
				return Convert.ToInt32 (lastLine);
			}
			catch {
				return -1;
			}
		}
	}
}

