using System;
using System.Text.Json.Serialization;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000D RID: 13
	public class devicecode
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002D0C File Offset: 0x00000F0C
		[JsonPropertyName("user_code")]
		public int user_code { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D15 File Offset: 0x00000F15
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002D1D File Offset: 0x00000F1D
		[JsonPropertyName("device_code")]
		public string device_code { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D26 File Offset: 0x00000F26
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002D2E File Offset: 0x00000F2E
		[JsonPropertyName("verification_uri")]
		public string verification_uri { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D37 File Offset: 0x00000F37
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002D3F File Offset: 0x00000F3F
		[JsonPropertyName("verification_uri_complete")]
		public string verification_uri_complete { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002D50 File Offset: 0x00000F50
		[JsonPropertyName("prompt")]
		public string prompt { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D59 File Offset: 0x00000F59
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002D61 File Offset: 0x00000F61
		[JsonPropertyName("expires_in")]
		public string expires_in { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D6A File Offset: 0x00000F6A
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002D72 File Offset: 0x00000F72
		[JsonPropertyName("interval")]
		public string interval { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D7B File Offset: 0x00000F7B
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002D83 File Offset: 0x00000F83
		[JsonPropertyName("client_id")]
		public string client_id { get; set; }
	}
}
