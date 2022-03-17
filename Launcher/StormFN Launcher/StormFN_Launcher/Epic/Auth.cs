using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using RestSharp;

namespace StormFN_Launcher.Epic
{
	// Token: 0x0200000F RID: 15
	internal class Auth
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static string GetDevicecodetoken()
		{
			RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "Basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			restRequest.AddParameter("grant_type", "client_credentials");
			RestRequest restRequest2 = restRequest;
			string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
			{
				':'
			}, 26);
			string result;
			try
			{
				result = array[1].ToString().Split(new char[]
				{
					','
				}, 2)[0].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[0].ToString();
			}
			catch
			{
				MessageBox.Show("Please make sure that you are connected to the internet.");
				Process.GetCurrentProcess().Kill();
				result = "error";
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EB4 File Offset: 0x000010B4
		public static string GetDevicecode(string auth)
		{
			RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/deviceAuthorization");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "Bearer " + auth);
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			RestRequest restRequest2 = restRequest;
			string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
			{
				','
			}, 8);
			string[] array2 = array[3].ToString().Split(new char[]
			{
				'"'
			}, 4)[3].ToString().Split(new char[]
			{
				'"'
			}, 2);
			string[] array3 = array[1].ToString().Split(new char[]
			{
				'"'
			}, 4)[3].ToString().Split(new char[]
			{
				'"'
			}, 2);
			Process.Start(array2[0]);
			string content;
			for (;;)
			{
				RestClient restClient2 = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
				RestRequest restRequest3 = new RestRequest(1);
				restRequest3.AddHeader("Authorization", "Basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
				restRequest3.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				restRequest3.AddParameter("grant_type", "device_code");
				restRequest3.AddParameter("device_code", array3[0].ToString());
				RestRequest restRequest4 = restRequest3;
				content = restClient2.Execute(restRequest4).Content;
				if (content.Contains("access_token"))
				{
					break;
				}
				content.Contains("errors.com.epicgames.not_found");
				Thread.Sleep(150);
			}
			string[] array4 = content.Split(new char[]
			{
				':'
			}, 26);
			return array4[1].ToString().Split(new char[]
			{
				','
			}, 2)[0].ToString().Split(new char[]
			{
				'"'
			}, 2)[1].ToString().Split(new char[]
			{
				'"'
			}, 2)[0].ToString() + "," + array4[16].ToString().Split(new char[]
			{
				','
			}, 2)[0];
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030A4 File Offset: 0x000012A4
		public static string GetToken(string authCode)
		{
			Console.WriteLine("Token wird angefragt");
			RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token");
			RestRequest restRequest = new RestRequest(1);
			restRequest.AddHeader("Authorization", "basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
			restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			restRequest.AddParameter("grant_type", "authorization_code");
			restRequest.AddParameter("code", authCode);
			RestRequest restRequest2 = restRequest;
			string content = restClient.Execute(restRequest2).Content;
			if (content.Contains("access_token"))
			{
				string[] array = content.Split(new char[]
				{
					':'
				}, 26);
				string str = array[17].ToString().Split(new char[]
				{
					','
				}, 2)[0];
				return array[1].ToString().Split(new char[]
				{
					','
				}, 2)[0].ToString().Split(new char[]
				{
					'"'
				}, 2)[1].ToString().Split(new char[]
				{
					'"'
				}, 2)[0].ToString() + "," + str;
			}
			if (content.Contains("It is possible that it was no longer valid"))
			{
				MessageBox.Show("An Error occured, pls try again at a later point.");
				Process.Start("https://www.epicgames.com/id/logout?redirectUrl=https%3A//www.epicgames.com/id/login%3FredirectUrl%3Dhttps%253A%252F%252Fwww.epicgames.com%252Fid%252Fapi%252Fredirect%253FclientId%253D3446cd72694c4a4485d81b77adbb2141%2526responseType%253Dcode");
				return "error";
			}
			MessageBox.Show(content);
			return "error";
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031EC File Offset: 0x000013EC
		public static string GetExchange(string token)
		{
			RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/exchange");
			RestRequest restRequest = new RestRequest(0);
			restRequest.AddHeader("Authorization", "bearer " + token);
			RestRequest restRequest2 = restRequest;
			string content = restClient.Execute(restRequest2).Content;
			Console.WriteLine(content);
			if (content.Contains("errors.com.epicgames.common.oauth.invalid_token"))
			{
				return "error";
			}
			return content.Split(new char[]
			{
				','
			}, 4)[1].ToString().Split(new char[]
			{
				','
			}, 2)[0].ToString().Split(new char[]
			{
				'"'
			}, 2)[1].ToString().Split(new char[]
			{
				'"'
			}, 2)[1].ToString().Split(new char[]
			{
				'"'
			}, 2)[1].ToString().Split(new char[]
			{
				'"'
			}, 2)[0].ToString();
		}
	}
}
