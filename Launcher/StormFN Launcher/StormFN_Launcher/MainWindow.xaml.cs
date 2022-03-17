using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using StormFN_Launcher.Epic;
using StormFN_Launcher.Utilities;

namespace StormFN_Launcher
{
	// Token: 0x02000004 RID: 4
	public partial class MainWindow : Window
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002165 File Offset: 0x00000365
		public static string foldername
		{
			get
			{
				return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\";
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002190 File Offset: 0x00000390
		private void Give_AntiCheat_Love()
		{
			using (PowerShell powerShell = PowerShell.Create())
			{
				string str = "system*";
				powerShell.AddScript("Set-Service 'BEService' -StartupType Disabled" + str);
				powerShell.AddScript("Set-Service 'EasyAntiCheat' -StartupType Disabled" + str);
				Config_file.Default.AC_bypass = true;
				Config_file.Default.Save();
				foreach (PSObject psobject in powerShell.Invoke())
				{
					if (psobject != null)
					{
						this.msg("An Error occured \n" + psobject.Properties["Status"].Value.ToString() + " - ");
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002264 File Offset: 0x00000464
		private void Save_settings(object sender, EventArgs e)
		{
			Config_file.Default.Path = this.FN_Path.Text;
			Config_file.Default.Save();
			Application.Current.Shutdown();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002290 File Offset: 0x00000490
		private void MainWindow_Load(object sender, EventArgs e)
		{
			this.DeleteCms();
			if (this.FN_Path.Text == "Fortnite Path" || this.FN_Path.Text == "" || string.IsNullOrEmpty(this.FN_Path.Text))
			{
				TextBox fn_Path = this.FN_Path;
				MainWindow.Installation installation = MainWindow.GetEpicInstallLocations().FirstOrDefault((MainWindow.Installation i) => i.AppName == "Fortnite");
				fn_Path.Text = ((installation != null) ? installation.InstallLocation : null);
			}
			else
			{
				this.FN_Path.Text = Config_file.Default.Path;
			}
			if (!Config_file.Default.AC_bypass)
			{
				this.Give_AntiCheat_Love();
			}
			if (Config_file.Default.Show)
			{
				Config_file.Default.Show = false;
				Config_file.Default.Save();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002370 File Offset: 0x00000570
		public void ShowLi(bool yesorno)
		{
			if (!yesorno)
			{
				this.Logged_in_as.Visibility = Visibility.Hidden;
				this.DisplayName.Visibility = Visibility.Hidden;
				this.LoginButton.Content = "Login";
				return;
			}
			this.Logged_in_as.Visibility = Visibility.Visible;
			this.DisplayName.Visibility = Visibility.Visible;
			this.LoginButton.Content = "Launch";
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D1 File Offset: 0x000005D1
		public void msg(string text)
		{
			MessageBox.Show(text.ToString(), "GHYBRID Launcher");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E4 File Offset: 0x000005E4
		private void Login_click(object sender, RoutedEventArgs e)
		{
			if (!(this.LoginButton.Content.ToString() == "Login"))
			{
				if (this.LoginButton.Content.ToString() == "Launch")
				{
					Config_file.Default.Path = this.FN_Path.Text;
					Config_file.Default.Save();
					this.exchange = Auth.GetExchange(this.token);
					string text = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
					string text2 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
					string text3 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");
					if (!File.Exists(text))
					{
						this.msg("\"" + text + "\" wasn't found, stop deleting game files!");
						this.ShowLi(false);
						return;
					}
					if (!File.Exists(text2))
					{
						this.msg("\"" + text2 + "\" wasn't found, stop deleting game files!");
						this.ShowLi(false);
						return;
					}
					if (!File.Exists(text3))
					{
						this.msg("\"" + text3 + "\" wasn't found, stop deleting game files!");
						this.ShowLi(false);
						return;
					}
					Config_file.Default.Path = this.FN_Path.Text;
					Config_file.Default.Save();
					this.exchange = Auth.GetExchange(this.token);
					string arguments = "-AUTH_LOGIN=unused -AUTH_PASSWORD=" + this.exchange + " -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ - skippatchcheck";
					Process process = new Process
					{
						StartInfo = new ProcessStartInfo(text, arguments)
						{
							UseShellExecute = false,
							RedirectStandardOutput = false,
							CreateNoWindow = true
						}
					};
					Process process2 = new Process();
					process2.StartInfo.FileName = text3;
					process2.Start();
					foreach (object obj in process2.Threads)
					{
						ProcessThread processThread = (ProcessThread)obj;
						Win32.SuspendThread(Win32.OpenThread(2, false, processThread.Id));
					}
					Process process3 = new Process();
					process3.StartInfo.FileName = text2;
					process3.StartInfo.Arguments = "-epiclocale=en -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ";
					process3.Start();
					foreach (object obj2 in process3.Threads)
					{
						ProcessThread processThread2 = (ProcessThread)obj2;
						Win32.SuspendThread(Win32.OpenThread(2, false, processThread2.Id));
					}
					process.Start();
					Thread.Sleep(2000);
					base.Hide();
					Thread.Sleep(6000);
					try
					{
						File.Delete(MainWindow.tempPath + "/Injector.exe");
					}
					catch
					{
					}
					try
					{
						this.webClient.DownloadFile("https://cdn.discordapp.com/attachments/823233042788122685/828311722036690984/Injector.exe", MainWindow.tempPath + "/Injector.exe");
					}
					catch
					{
						MessageBox.Show("Please Run Launcher as Administrator and turn off Antivirus.");
						this.ShowLi(false);
						return;
					}
					process.WaitForInputIdle();
					new Process
					{
						StartInfo = 
						{
							Arguments = string.Format("\"{0}\" \"{1}\"", process.Id, MainWindow.ssldllpath),
							CreateNoWindow = true,
							UseShellExecute = false,
							FileName = MainWindow.tempPath + "/Injector.exe"
						}
					}.Start();
					process.WaitForInputIdle();
					new Process
					{
						StartInfo = 
						{
							Arguments = string.Format("\"{0}\" \"{1}\"", process.Id, MainWindow.consoledllpath),
							CreateNoWindow = true,
							UseShellExecute = false,
							FileName = MainWindow.tempPath + "/Injector.exe"
						}
					}.Start();
					process.WaitForExit();
					try
					{
						process2.Close();
						process3.Close();
					}
					catch
					{
					}
					base.Show();
					this.ShowLi(false);
					this.LoginButton.Content = "Login";
				}
				return;
			}
			Config_file.Default.Path = this.FN_Path.Text;
			Config_file.Default.Save();
			string devicecode = Auth.GetDevicecode(Auth.GetDevicecodetoken());
			string[] array = devicecode.Split(new char[]
			{
				','
			}, 2);
			if (devicecode.Contains("error"))
			{
				return;
			}
			this.username = array[1];
			this.DisplayName.Content = (string)(array[1] ?? "");
			this.ShowLi(true);
			this.token = array[0];
			this.LoginButton.Content = "Launch";
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028C8 File Offset: 0x00000AC8
		public static List<MainWindow.Installation> GetEpicInstallLocations()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");
			if (!Directory.Exists(Path.GetDirectoryName(path)) || !File.Exists(path))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<MainWindow.EpicInstallLocations>(File.ReadAllText(path)).InstallationList;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002910 File Offset: 0x00000B10
		private void Select_fn_path_button_Click(object sender, EventArgs e)
		{
			string text = this.FN_Path.Text;
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};
			if (commonOpenFileDialog.ShowDialog() == 1)
			{
				this.FN_Path.Text = commonOpenFileDialog.FileName;
				Config_file.Default.Path = this.FN_Path.Text;
				Config_file.Default.Save();
				return;
			}
			this.FN_Path.Text = text;
			Config_file.Default.Path = this.FN_Path.Text;
			Config_file.Default.Save();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000299C File Offset: 0x00000B9C
		private void DeleteCms()
		{
			try
			{
				string path = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\PersistentDownloadDir";
				string path2 = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\webcache";
				Directory.Delete(path, true);
				Directory.Delete(path2, true);
			}
			catch
			{
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029F4 File Offset: 0x00000BF4
		private void Console_Checked(object sender, RoutedEventArgs e)
		{
			MainWindow.disableConsole = false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000029FC File Offset: 0x00000BFC
		private void Console_UnChecked(object sender, RoutedEventArgs e)
		{
			MainWindow.disableConsole = true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A04 File Offset: 0x00000C04
		private void Custom_Name_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!(this.Custom_Name.Text == ""))
			{
				this.msg("Sorry, but GHYBRID will support Custom Names in there next update.");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000020BC File Offset: 0x000002BC
		private void Custom_Name_MouseDown(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x04000003 RID: 3
		public static bool disableConsole = false;

		// Token: 0x04000004 RID: 4
		private string token;

		// Token: 0x04000005 RID: 5
		private string exchange;

		// Token: 0x04000006 RID: 6
		private string username;

		// Token: 0x04000007 RID: 7
		public static string tempPath = Path.GetTempPath();

		// Token: 0x04000008 RID: 8
		private readonly WebClient webClient = new WebClient();

		// Token: 0x04000009 RID: 9
		public static string ssldllpath = MainWindow.foldername + "SSL.dll";

		// Token: 0x0400000A RID: 10
		public static string consoledllpath = MainWindow.foldername + "Sodium.dll";

		// Token: 0x02000005 RID: 5
		public class EpicInstallLocations
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002BCC File Offset: 0x00000DCC
			// (set) Token: 0x06000025 RID: 37 RVA: 0x00002BD4 File Offset: 0x00000DD4
			[JsonProperty("InstallationList")]
			public List<MainWindow.Installation> InstallationList { get; set; }
		}

		// Token: 0x02000006 RID: 6
		public class Installation
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000027 RID: 39 RVA: 0x00002BE5 File Offset: 0x00000DE5
			// (set) Token: 0x06000028 RID: 40 RVA: 0x00002BED File Offset: 0x00000DED
			[JsonProperty("InstallLocation")]
			public string InstallLocation { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BF6 File Offset: 0x00000DF6
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002BFE File Offset: 0x00000DFE
			[JsonProperty("AppName")]
			public string AppName { get; set; }
		}
	}
}
