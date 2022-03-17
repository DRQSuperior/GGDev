using System;
using System.Text.Json.Serialization;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000E RID: 14
	public class Exchange
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002D94 File Offset: 0x00000F94
		[JsonPropertyName("expiresInSeconds")]
		public int ExpiresInSeconds { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D9D File Offset: 0x00000F9D
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002DA5 File Offset: 0x00000FA5
		[JsonPropertyName("code")]
		public string Code { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DAE File Offset: 0x00000FAE
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002DB6 File Offset: 0x00000FB6
		[JsonPropertyName("creatingClientId")]
		public string CreatingClientId { get; set; }
	}
}
