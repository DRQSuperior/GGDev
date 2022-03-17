using System;
using System.Runtime.InteropServices;

namespace StormFN_Launcher.Utilities
{
	// Token: 0x0200000B RID: 11
	public static class Win32
	{
		// Token: 0x06000042 RID: 66
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32.dll")]
		public static extern int SuspendThread(IntPtr hThread);

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32.dll")]
		public static extern int ResumeThread(IntPtr hThread);

		// Token: 0x06000045 RID: 69
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool AllocConsole();

		// Token: 0x06000046 RID: 70
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCtrlHandler(Win32.HandlerRoutine HandlerRoutine, bool Add);

		// Token: 0x06000047 RID: 71
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x06000048 RID: 72
		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x06000049 RID: 73
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x0600004A RID: 74
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x0600004B RID: 75
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

		// Token: 0x0400001B RID: 27
		public const int PROCESS_CREATE_THREAD = 2;

		// Token: 0x0400001C RID: 28
		public const int PROCESS_VM_OPERATION = 8;

		// Token: 0x0400001D RID: 29
		public const int PROCESS_VM_WRITE = 32;

		// Token: 0x0400001E RID: 30
		public const int PROCESS_VM_READ = 16;

		// Token: 0x0400001F RID: 31
		public const int PROCESS_QUERY_INFORMATION = 1024;

		// Token: 0x04000020 RID: 32
		public const uint PAGE_READWRITE = 4U;

		// Token: 0x04000021 RID: 33
		public const uint MEM_COMMIT = 4096U;

		// Token: 0x04000022 RID: 34
		public const uint MEM_RESERVE = 8192U;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x0600004E RID: 78
		public delegate bool HandlerRoutine(int dwCtrlType);
	}
}
