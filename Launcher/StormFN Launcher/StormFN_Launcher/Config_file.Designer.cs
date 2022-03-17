using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StormFN_Launcher
{
	// Token: 0x02000003 RID: 3
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
	internal sealed partial class Config_file : ApplicationSettingsBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020BE File Offset: 0x000002BE
		public static Config_file Default
		{
			get
			{
				return Config_file.defaultInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020C5 File Offset: 0x000002C5
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020D7 File Offset: 0x000002D7
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Path
		{
			get
			{
				return (string)this["Path"];
			}
			set
			{
				this["Path"] = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020E5 File Offset: 0x000002E5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020F7 File Offset: 0x000002F7
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool Show
		{
			get
			{
				return (bool)this["Show"];
			}
			set
			{
				this["Show"] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000211C File Offset: 0x0000031C
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool AC_bypass
		{
			get
			{
				return (bool)this["AC_bypass"];
			}
			set
			{
				this["AC_bypass"] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000212F File Offset: 0x0000032F
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002141 File Offset: 0x00000341
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Email
		{
			get
			{
				return (string)this["Email"];
			}
			set
			{
				this["Email"] = value;
			}
		}

		// Token: 0x04000002 RID: 2
		private static Config_file defaultInstance = (Config_file)SettingsBase.Synchronized(new Config_file());
	}
}
