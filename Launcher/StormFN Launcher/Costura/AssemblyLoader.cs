using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000032D7 File Offset: 0x000014D7
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032E8 File Offset: 0x000014E8
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003358 File Offset: 0x00001558
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000338C File Offset: 0x0000158C
		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						return memoryStream;
					}
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003410 File Offset: 0x00001610
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullName;
			if (resourceNames.TryGetValue(name, out fullName))
			{
				return AssemblyLoader.LoadStream(fullName);
			}
			return null;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003430 File Offset: 0x00001630
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003458 File Offset: 0x00001658
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003518 File Offset: 0x00001718
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				object obj2 = AssemblyLoader.nullCacheLock;
				lock (obj2)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000035FC File Offset: 0x000017FC
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyLoader()
		{
			AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
			AssemblyLoader.symbolNames.Add("costura", "costura.costura.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("cs.modernwpf.resources", "costura.cs.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.modernwpf.resources", "costura.de.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.modernwpf.resources", "costura.es.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fa.modernwpf.resources", "costura.fa.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.modernwpf.resources", "costura.fr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.modernwpf.resources", "costura.it.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.modernwpf.resources", "costura.ja.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.modernwpf.resources", "costura.ko.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.bcl.asyncinterfaces", "costura.microsoft.bcl.asyncinterfaces.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.windowsapicodepack", "costura.microsoft.windowsapicodepack.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.windowsapicodepack.shell", "costura.microsoft.windowsapicodepack.shell.dll.compressed");
			AssemblyLoader.assemblyNames.Add("modernwpf.controls", "costura.modernwpf.controls.dll.compressed");
			AssemblyLoader.assemblyNames.Add("modernwpf", "costura.modernwpf.dll.compressed");
			AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.modernwpf.resources", "costura.pl.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.modernwpf.resources", "costura.pt-br.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("restsharp", "costura.restsharp.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.modernwpf.resources", "costura.ru.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.buffers", "costura.system.buffers.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.management.automation", "costura.system.management.automation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.memory", "costura.system.memory.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.numerics.vectors", "costura.system.numerics.vectors.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.compilerservices.unsafe", "costura.system.runtime.compilerservices.unsafe.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.encodings.web", "costura.system.text.encodings.web.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.json", "costura.system.text.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks.extensions", "costura.system.threading.tasks.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.valuetuple", "costura.system.valuetuple.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.modernwpf.resources", "costura.tr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.modernwpf.resources", "costura.zh-hans.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.modernwpf.resources", "costura.zh-hant.modernwpf.resources.dll.compressed");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000038C8 File Offset: 0x00001AC8
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x0400002E RID: 46
		private static object nullCacheLock = new object();

		// Token: 0x0400002F RID: 47
		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		// Token: 0x04000030 RID: 48
		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		// Token: 0x04000031 RID: 49
		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		// Token: 0x04000032 RID: 50
		private static int isAttached;
	}
}
